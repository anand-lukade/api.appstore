using api.appstore.Models;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Configuration;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Net.Http;
using System.Security.Claims;
using System.Text;
using System.Web.Http;
using System.Web.Http.Cors;

namespace api.appstore.Controllers
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class LoginController : ApiController
    {

        [HttpPost]
        [Route("QRLogin")]
        public IHttpActionResult QRLogin(QRRequest request)
        {
            try
            {
                if (string.IsNullOrEmpty(request.QrCode))
                {
                    return BadRequest("QR code is required");
                }
                var qrCodeID = request.QrCode.Split('?');
                string[] stringSeparators = new string[] { "Num" };
                var splitCode = qrCodeID[1].Split(stringSeparators, StringSplitOptions.None);
                var qrId = splitCode[0].Split('=')[1];
                var model = new QRLoginUserModel
                {
                    Id = qrId
                };
                var apiresult = new CommonModels().
                    Post<QRLoginUserModel>("Personnel_InfoClass", model);
                apiresult.Wait();
                if (apiresult.Result.IsSuccessStatusCode)
                {
                    var readTask = apiresult.Result.Content.ReadAsAsync<QRLoginUserModel>();
                    readTask.Wait();
                    var info = readTask.Result;
                    if (info.IsActive)
                    {
                        string token = CreateToken(info.FirstName + "." + info.LastName);
                        return Ok(new UserDetails()
                        {                            
                            Firstname = info.FirstName,
                            Lastname = info.LastName,
                            IsAdmin = false,
                            Token = token,
                            TokenValidity = DateTime.UtcNow.AddDays(Convert.ToInt16(ConfigurationManager.AppSettings["jwtValidity"])),
                            IsActive = true
                        });
                    }
                    else
                    {
                        return BadRequest("User is inactive");
                    }
                }
                else
                {
                    return BadRequest("Saleforce server unable to validate");
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [Route("Login")]
        public IHttpActionResult Login(LoginRequest loginRequest)
        {
            try
            {
                if(loginRequest.Username==null || loginRequest.Password==null)
                {
                    return BadRequest("Username/Password is mandatory");
                }
                using (MususAppEntities entity = new MususAppEntities())
                {
                    var user = entity.UserInfoes.FirstOrDefault(
                        x => x.UserName == loginRequest.Username &&
                        x.Password == loginRequest.Password &&
                        x.IsActive == true);
                    if(user!=null)
                    {
                        string token = CreateToken(user.UserName);
                        return Ok(new UserDetails()
                        {
                            Firstname = user.FirstName,
                            Lastname = user.LastName,
                            IsActive = user.IsActive,
                            TokenValidity = DateTime.UtcNow.AddDays(Convert.ToInt16(ConfigurationManager.AppSettings["jwtValidity"])),
                            Token = token,
                            IsAdmin = true
                        });
                    }
                    else
                    {
                        return BadRequest("invalid username/password");
                    }
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        private string CreateToken(string username)
        {
            string sec = ConfigurationManager.AppSettings["jwtKey"];

            //Set issued at date
            DateTime issuedAt = DateTime.UtcNow;
            //set the time when it expires
            DateTime expires = DateTime.UtcNow.AddDays(int.Parse(ConfigurationManager.AppSettings["jwtValidity"]));

            //http://stackoverflow.com/questions/18223868/how-to-encrypt-jwt-security-token
            var tokenHandler = new JwtSecurityTokenHandler();

            //create a identity and add claims to the user which we want to log in
            ClaimsIdentity claimsIdentity = new ClaimsIdentity(new[]
            {
                new Claim(ClaimTypes.Name, username)
            });

            

            var now = DateTime.UtcNow;
            var securityKey = new Microsoft.IdentityModel.Tokens.SymmetricSecurityKey(System.Text.Encoding.Default.GetBytes(sec));
            var signingCredentials = new Microsoft.IdentityModel.Tokens.SigningCredentials(securityKey, Microsoft.IdentityModel.Tokens.SecurityAlgorithms.HmacSha256Signature);


            //create the jwt
            var token =
                (JwtSecurityToken)
                    tokenHandler.CreateJwtSecurityToken(issuer: ConfigurationManager.AppSettings["jwtIssuer"], 
                    audience: ConfigurationManager.AppSettings["jwtIssuer"],
                        subject: claimsIdentity, notBefore: issuedAt, expires: expires, signingCredentials: signingCredentials);
            var tokenString = tokenHandler.WriteToken(token);

            return tokenString;
        }
    }
}
