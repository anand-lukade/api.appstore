using api.appstore.Models;
using System;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.IO;

namespace api.appstore.Controllers
{
    public partial class AppController
    {                                
        [Route("Documents")]
        public IHttpActionResult PostDocuments(DocumentMaster master)
        {
            try
            {
                using (MususAppEntities entity = new MususAppEntities())
                {
                    master.DeletedTime = null;
                    master.IsDeleted = false;
                    UploadAttachments(master);
                    entity.DocumentMasters.Add(master);
                    entity.SaveChanges();
                    return Ok("web page app added successfully");           
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [Route("Documents")]
        public IHttpActionResult PutDocuments(DocumentMaster master)
        {
            try
            {

                using (MususAppEntities entity = new MususAppEntities())
                {                    
                    var app = entity.DocumentMasters.FirstOrDefault(x => x.Id == master.Id);                    
                    app.Description = master.Description;
                    app.Documents = master.Documents;
                    app.Title = master.Title;
                    entity.SaveChanges();
                    return Ok("web page app changed successfully");
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [Route("Documents/{id}")]
        public IHttpActionResult DeleteDocuments(Guid id)
        {
            try
            {
                using (MususAppEntities entity = new MususAppEntities())
                {
                    var app = entity.DocumentMasters.FirstOrDefault(x => x.Id == id);
                    app.IsDeleted = true;
                    app.DeletedTime = DateTime.Now;
                    entity.SaveChanges();
                    return Ok("web page app deleted successfully");
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        private void UploadAttachments(DocumentMaster master)
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
