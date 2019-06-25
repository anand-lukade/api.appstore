using api.appstore.Models;
using Microsoft.Azure.Storage;
using Microsoft.Azure.Storage.Blob;
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
                foreach (string file in httpRequest.Files)
                {
                    var postedFile = httpRequest.Files[file];
                    if (postedFile != null && postedFile.ContentLength > 0)
                    {
                        string serverAddress = Process(postedFile, master.Id);
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
                                if (master.ScreenShots != null)
                                {
                                    master.ScreenShots += ";" + Process(postedFile, master.Id,true);
                                }
                                else
                                {
                                    master.ScreenShots = Process(postedFile, master.Id, true); 
                                }                                    
                                break;

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
        private string Process(HttpPostedFile file, Guid id, bool flag=false)
        {
            CloudStorageAccount storageAccount = CloudStorageAccount.Parse(ConfigurationManager.AppSettings["storageConnectionKey"]);
            CloudBlobClient blobClient = storageAccount.CreateCloudBlobClient();            
            CloudBlobContainer container = blobClient.GetContainerReference(ConfigurationManager.AppSettings["containerName"]);        
            CloudBlockBlob blockBlob = container.GetBlockBlobReference(DateTime.Now.ToString("hhmmss")+"_"+file.FileName);
            //if(flag)
            //{
            //    //blockBlob = container.GetBlockBlobReference("BTJG3912.JPG");
            //    //blockBlob.Properties.ContentType = file.ContentType;
            //    //blockBlob.Properties.ContentMD5 = null;
            //    //blockBlob.SetProperties();
            //}            
            blockBlob.UploadFromStream(file.InputStream);
            return blockBlob.Uri.AbsoluteUri.ToString();
        }

        [Route("HostedApps/{appId}")]
        public IHttpActionResult PutDownloadCountMaster(Guid appId)
        {
            try
            {
                using (MususAppEntities entity = new MususAppEntities())
                {

                    var appMaster = entity.AppMasters.FirstOrDefault(x => x.Id == appId);
                    if(appMaster!=null)
                    {
                        if(appMaster.Download == null)
                        {
                            appMaster.Download = 1;
                        }
                        else
                        {
                            appMaster.Download += 1;
                        }                        
                    }
                    entity.SaveChanges();
                    return Ok(appMaster);
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }
        [Authorize]
        [Route("Ratings/{rating}/{appId}")]
        public IHttpActionResult Put(int rating, Guid appId)
        {
            try
            {
                using (MususAppEntities entity = new MususAppEntities())
                {                    
                    entity.Ratings.Add(new Rating()
                    {
                        AppId = appId,
                        Point = rating,
                        Username= RequestContext.Principal.Identity.Name
                    });
                    entity.SaveChanges();
                    return Ok();
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }
    }
}
