using api.appstore.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Http;

namespace api.appstore.Controllers
{
    public partial class AppController
    {
        [Route("HostedApps")]
        public IHttpActionResult PostAppMaster()
        {
            try
            {                
                using (MususAppEntities entity = new MususAppEntities())
                {
                    AppMaster master = null;
                    var httpRequest = HttpContext.Current.Request;
                    if (httpRequest.Params["id"] != null)
                    {
                        var id = Guid.Parse(httpRequest.Params["id"]);
                        master = entity.AppMasters.FirstOrDefault(x => x.Id == id);
                        if (master != null)
                        {
                            UploadAttachments(master);
                        }
                        else
                        {
                            BadRequest("Application not exists");
                        }
                    }
                    else
                    {
                        master = new AppMaster();
                        master.Id = Guid.NewGuid();
                        master.CreatedTime = DateTime.UtcNow;
                        master.IsDeleted = false;
                        master.DeletedTime = null;
                        UploadAttachments(master);                        
                        entity.AppMasters.Add(master);
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
        [Route("HostedApps")]
        public IHttpActionResult DeleteAppMaster(AppMaster master)
        {
            try
            {
                using (MususAppEntities entity = new MususAppEntities())
                {
                    var app = entity.AppMasters.FirstOrDefault(x => x.Id == master.Id);
                    app.IsDeleted = true;
                    app.DeletedTime = DateTime.Now;
                    entity.SaveChanges();
                    return Ok("hosted app updated successfully");
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }

        private void UploadAttachments(AppMaster master)
        {                      
            var httpRequest = HttpContext.Current.Request;
            master.CategoryId = Convert.ToInt16(httpRequest.Params["categoryId"]);
            master.Title = httpRequest.Params["title"];            
            master.Description = httpRequest.Params["description"];
            master.Version= httpRequest.Params["version"];
            master.IphonePackageName = httpRequest.Params["iphonePackageName"];
            master.IpadPackageName = httpRequest.Params["ipadPackageName"];
            master.Published = httpRequest.Params["published"]!=null? 
                Convert.ToBoolean(httpRequest.Params["published"]) :false;
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
                        var filePath = str_uploadpath + master.Id+"_"+ Path.GetFileName(postedFile.FileName);
                        var serverAddress= ConfigurationManager.AppSettings["webStore"]+"UploadBuckets/" + master.Id + "_" + Path.GetFileName(postedFile.FileName);
                        postedFile.SaveAs(filePath);
                        switch (file)
                        {
                            case "AndriodSmartPhoneBuild":
                                master.AndriodSmartPhoneBuild = serverAddress;
                                break;
                            case "Icon":
                                master.Icon = serverAddress;
                                break;
                            case "AndriodTabletBuild":
                                master.AndriodTabletBuild = serverAddress;
                                break;

                            case "IphoneBuild":
                                master.IphoneBuild = serverAddress;
                                break;

                            case "IpadBuild":
                                master.IpadBuild = serverAddress;
                                break;

                            case "ScreenShots":
                                master.ScreenShots += serverAddress + ";";
                                break;

                            case "Documents":
                                master.Documents += serverAddress + ";";                               
                                break;
                        }
                    }
                }
                if (master.Documents!=null && master.Documents.Length > 0)
                {
                    master.Documents = master.Documents.Substring(0, master.Documents.Length - 1);
                }
                if (master.ScreenShots != null && master.ScreenShots.Length > 0)
                {
                    master.ScreenShots = master.ScreenShots.Substring(0, master.ScreenShots.Length - 1);
                }
            }                                 
        }
    }
}
