using api.appstore.Models;
using System;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.IO;
using System.Configuration;

namespace api.appstore.Controllers
{
    public partial class AppController
    {                
        [Route("ThirdPartyApps")]
        public IHttpActionResult PostThirdPartyApp()
        {
            try
            {
                using (MususAppEntities entity = new MususAppEntities())
                {                    
                    ThirdParty master = new ThirdParty();
                    var app = entity.ThirdParties.FirstOrDefault(x => x.Id == master.Id);
                    if (app != null)
                    {
                        UploadAttachments(app);
                    }
                    else
                    {
                        master.IsDeleted = false;
                        master.DeletedTime = null;
                        master.Id = Guid.NewGuid();
                        UploadAttachments(master);
                        entity.ThirdParties.Add(master);
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
        //[Route("ThirdPartyApps")]
        //public IHttpActionResult PutThirdPartyApp(ThirdParty master)
        //{
        //    try
        //    {
        //        using (MususAppEntities entity = new MususAppEntities())
        //        {
        //            var app = entity.ThirdParties.FirstOrDefault(x => x.Id == master.Id);
        //            app.CategoryId = master.CategoryId;
        //            app.Description = master.Description;
        //            app.Documents = master.Documents;
        //            app.ThirdPartyAppUrl = master.ThirdPartyAppUrl;
        //            app.Title = master.Title;
        //            app.Version = master.Version;                   
        //            entity.SaveChanges();
        //            return Ok("third party app changed successfully");
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        return BadRequest(ex.Message);
        //    }

        //}
        [Route("ThirdPartyApps")]
        public IHttpActionResult DeleteThirdPartyApp(Guid id)
        {
            try
            {
                using (MususAppEntities entity = new MususAppEntities())
                {
                    var app = entity.ThirdParties.FirstOrDefault(x => x.Id == id);
                    app.IsDeleted = true;
                    app.DeletedTime = DateTime.Now;
                    entity.SaveChanges();
                    return Ok("third party app changed successfully");
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }


        private void UploadAttachments(ThirdParty master)
        {
            var httpRequest = HttpContext.Current.Request;
            master.CategoryId = Convert.ToInt16(httpRequest.Params["categoryId"]);
            master.Title = httpRequest.Params["title"];
            master.Description = httpRequest.Params["description"];
            master.Version = httpRequest.Params["version"];
            master.ThirdPartyAppUrl = httpRequest.Params["thirdPartyAppUrl"];
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
                        var serverAddress = ConfigurationManager.AppSettings["webStore"] + "UploadBuckets/" + master.Id + "_" + Path.GetFileName(postedFile.FileName);
                        postedFile.SaveAs(filePath);
                        switch (file)
                        {                            
                            case "Documents":
                                master.Documents += serverAddress + ";";
                                break;
                        }
                        if (master.Documents.Length > 0)
                        {
                            master.Documents = master.Documents.Substring(0, master.Documents.Length - 1);
                        }
                    }
                }
            }
        }        
    }
}
