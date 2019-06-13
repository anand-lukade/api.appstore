using api.appstore.Models;
using System;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Http;

namespace api.appstore.Controllers
{
    public partial class AppController
    {                        
        [Route("Webpages")]
        public IHttpActionResult PostWebpages()
        {
            try
            {
                using (MususAppEntities entity = new MususAppEntities())
                {
                    WebPageUrl master = null;
                    var httpRequest = HttpContext.Current.Request;

                    if (httpRequest.Params["id"] != null)
                    {
                        var id = Guid.Parse(httpRequest.Params["id"]);
                        master = entity.WebPageUrls.FirstOrDefault(x => x.Id == id);
                        if (master != null)
                        {
                            UploadAttachments(master);
                        }
                    }
                    else
                    {
                        master = new WebPageUrl();
                        master.IsDeleted = false;
                        master.DeletedTime = null;
                        master.CreatedTime = DateTime.UtcNow;
                        master.Id = Guid.NewGuid();
                        UploadAttachments(master);
                        entity.WebPageUrls.Add(master);
                    }                   
                    entity.SaveChanges();
                    return Ok(master);
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);

            }
        }
        //[Route("Webpages")]
        //public IHttpActionResult PutWebpages(WebPageUrl master)
        //{
        //    try
        //    {
        //        using (MususAppEntities entity = new MususAppEntities())
        //        {
        //            var app = entity.WebPageUrls.FirstOrDefault(x => x.Id == master.Id);
        //            app.Description = master.Description;
        //            app.Documents = master.Documents;
        //            app.Title = master.Title;
        //            app.WebPageUrl1 = master.WebPageUrl1;
        //            entity.SaveChanges();
        //            return Ok("web page app changed successfully");
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        return BadRequest(ex.Message);

        //    }
        //}
        [Route("Webpages/{id}")]
        public IHttpActionResult DeleteWebpages(Guid id)
        {
            try
            {
                using (MususAppEntities entity = new MususAppEntities())
                {
                    var app = entity.WebPageUrls.FirstOrDefault(x => x.Id == id);
                    app.IsDeleted = true;
                    app.DeletedTime = DateTime.Now;
                    entity.SaveChanges();
                    return Ok("web page deleted successfully");
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);

            }
        }
        private void UploadAttachments(WebPageUrl master)
        {
            var httpRequest = HttpContext.Current.Request;
            master.Title = httpRequest.Params["title"];
            master.Description = httpRequest.Params["description"];
            master.WebPageUrl1 = httpRequest.Params["webPageUrl"];
            if (httpRequest.Files.Count > 0)
            {
                string path = "~/UploadBuckets/";
                string serverpath = HttpContext.Current.Server.MapPath(path);
                if (!Directory.Exists(serverpath))
                {
                    Directory.CreateDirectory(serverpath);
                }
                foreach (string file in httpRequest.Files)
                {
                    var postedFile = httpRequest.Files[file];
                    if (postedFile != null && postedFile.ContentLength > 0)
                    {
                        string str_uploadpath = HttpContext.Current.Server.MapPath("/UploadBuckets/");
                        var filePath = str_uploadpath + master.Id + "_" + Path.GetFileName(postedFile.FileName);
                        postedFile.SaveAs(filePath);
                        var serverAddress = ConfigurationManager.AppSettings["webStore"] + "UploadBuckets/" + master.Id + "_" + Path.GetFileName(postedFile.FileName);
                        switch (file)
                        {
                            case "Documents":
                                master.Documents += serverAddress + ";";
                                break;
                        }                        
                    }
                }
                if (master.Documents != null && master.Documents.Length > 0)
                {
                    master.Documents = master.Documents.Substring(0, master.Documents.Length - 1);
                }
            }
        }
    }
}
