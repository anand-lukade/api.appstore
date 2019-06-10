using api.appstore.Models;
using System;
using System.Linq;
using System.Web.Http;

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
    }
}
