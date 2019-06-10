﻿using api.appstore.Models;
using System;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.IO;

namespace api.appstore.Controllers
{
    public partial class AppController
    {                
        [Route("ThirdPartyApps")]
        public IHttpActionResult PostThirdPartyApp(ThirdParty master)
        {
            try
            {
                using (MususAppEntities entity = new MususAppEntities())
                {
                    master.IsDeleted = false;
                    master.DeletedTime = null;
                    UploadAttachments(master);
                    entity.ThirdParties.Add(master);
                    entity.SaveChanges();
                    return Ok("third party app added successfully");
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }
        [Route("ThirdPartyApps")]
        public IHttpActionResult PutThirdPartyApp(ThirdParty master)
        {
            try
            {
                using (MususAppEntities entity = new MususAppEntities())
                {
                    var app = entity.ThirdParties.FirstOrDefault(x => x.Id == master.Id);
                    app.CategoryId = master.CategoryId;
                    app.Description = master.Description;
                    app.Documents = master.Documents;
                    app.ThirdPartyAppUrl = master.ThirdPartyAppUrl;
                    app.Title = master.Title;
                    app.Version = master.Version;                   
                    entity.SaveChanges();
                    return Ok("third party app changed successfully");
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }
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
                        switch (file)
                        {                            
                            case "Documents":
                                master.Documents += filePath + ";";
                                break;
                        }
                    }
                }
            }
        }
    }
}
