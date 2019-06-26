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
                    var httpRequest = HttpContext.Current.Request;
                    ThirdParty master = null;
                    if (httpRequest.Params["id"]!=null)
                    {
                        var id = Guid.Parse(httpRequest.Params["id"]);
                        master = entity.ThirdParties.FirstOrDefault(x => x.Id == id);
                        if (master != null)
                        {
                            UploadAttachments(master);
                        }
                        else
                        {
                            return BadRequest("Thirdparty app does not exists");
                        }
                    }                                       
                    else
                    {
                        master = new ThirdParty();
                        master.CreatedTime = DateTime.UtcNow;
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
                
                foreach (string file in httpRequest.Files)
                {
                    var postedFile = httpRequest.Files[file];
                    if (postedFile != null && postedFile.ContentLength > 0)
                    {
                        string serverAddress = Process(postedFile);
                        switch (file)
                        {
                            case "Documents":
                                if (master.Documents != null)
                                {
                                    master.Documents += ";" + serverAddress;
                                }
                                else
                                {
                                    master.Documents = serverAddress;
                                }
                                break;
                        }                        
                    }
                }                
            }
        }        
    }
}
