﻿using api.appstore.Models;
using System;
using System.Collections.Generic;
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
                    AppMaster master = new AppMaster();
                    master.Id = Guid.NewGuid();
                    master.IsDeleted = false;
                    master.CreatedTime = DateTime.UtcNow;
                    master.DeletedTime = null;
                    UploadAttachments(master);
                    entity.AppMasters.Add(master);
                    entity.SaveChanges();
                    return Ok("app updated successfully");
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
            
        }
        [Route("HostedApps")]
        public IHttpActionResult PutAppMaster(AppMaster master)
        {
            try
            {
                using (MususAppEntities entity = new MususAppEntities())
                {
                    var app = entity.AppMasters.FirstOrDefault(x => x.Id == master.Id);
                    app.AndriodSmartPhoneBuild = master.AndriodSmartPhoneBuild;
                    app.AndriodTabletBuild = master.AndriodTabletBuild;
                    app.CategoryId = master.CategoryId;
                    app.Description = master.Description;
                    app.Documents = master.Documents;
                    app.Icon = master.Icon;
                    app.IpadBuild = master.IpadBuild;
                    app.IpadPackageName = master.IpadPackageName;
                    app.IphoneBuild = master.IphoneBuild;
                    app.IphonePackageName = master.IphonePackageName;
                    app.Published = master.Published;
                    app.ScreenShots = master.ScreenShots;
                    app.Title = master.Title;
                    app.Version = master.Version;                                      
                    entity.SaveChanges();
                    return Ok("hosted app updated successfully");
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
            master.Title = httpRequest.Params["title"];
            master.Description = httpRequest.Params["description"];
            master.Version= httpRequest.Params["version"];
            master.IphonePackageName = httpRequest.Params["iphonePackageName"];
            master.IpadPackageName = httpRequest.Params["ipadPackageName"];
            master.Published = Convert.ToBoolean(httpRequest.Params["published"]);
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
                        postedFile.SaveAs(filePath);
                        switch (file)
                        {
                            case "AndriodSmartPhoneBuild":
                                master.AndriodSmartPhoneBuild = filePath;
                                break;

                            case "AndriodTabletBuild":
                                master.AndriodTabletBuild = filePath;
                                break;

                            case "IphoneBuild":
                                master.IphoneBuild = filePath;
                                break;

                            case "IpadBuild":
                                master.IpadBuild = filePath;
                                break;

                            case "ScreenShots":
                                master.ScreenShots += filePath+";";
                                break;

                            case "Documents":
                                master.Documents += filePath +";";                               
                                break;
                        }
                    }
                }
            }                                 
        }
    }
}
