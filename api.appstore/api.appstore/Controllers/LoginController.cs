using api.appstore.Models;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Configuration;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Web.Http;

namespace api.appstore.Controllers
{
    public class LoginController : ApiController
    {
        [Route("Login")]
        public IHttpActionResult Login(LoginRequest loginRequest)
        {
            try
            {
                if(loginRequest.Username==null || loginRequest.Password==null)
                {
                    return BadRequest("Username/Password is mandatory");
                }
                using (appStoreEntities entity = new appStoreEntities())
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
