using api.appstore.Models;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using System.Web.Http.Cors;

namespace api.appstore.Controllers
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class UploadController : ApiController
    {
        public HttpResponseMessage Post()
        {
            HttpResponseMessage result = null;
            var httpRequest = HttpContext.Current.Request;
            var a=httpRequest.Params["username"];
            if (httpRequest.Files.Count > 0)
            {
                string path = "~/UploadBuckets/";
                string serverpath = HttpContext.Current.Server.MapPath(path);
                if (!Directory.Exists(serverpath))
                {
                    Directory.CreateDirectory(serverpath);
                }
                var docfiles = new List<string>();
                foreach (string file in httpRequest.Files)
                {                    
                    var postedFile = httpRequest.Files[file];
                    if (postedFile != null && postedFile.ContentLength > 0)
                    {
                        string str_uploadpath = HttpContext.Current.Server.MapPath("/UploadBuckets/");
                        var filePath = str_uploadpath + Path.GetFileName(postedFile.FileName);
                        postedFile.SaveAs(filePath);
                        docfiles.Add(filePath);
                        
                    }                    
                }
                result = Request.CreateResponse(HttpStatusCode.Created, docfiles);
            }
            else
            {
                result = Request.CreateResponse(HttpStatusCode.BadRequest);
            }
            return result;
        }
    }
}
