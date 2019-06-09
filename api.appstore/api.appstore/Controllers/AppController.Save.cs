using api.appstore.Models;
using System;
using System.Linq;
using System.Web.Http;

namespace api.appstore.Controllers
{
    public partial class AppController
    {
        [Route("HostedApps")]
        public IHttpActionResult PostAppMaster(AppMaster master)
        {
            try
            {
                using (MususAppEntities entity = new MususAppEntities())
                {
                    master.IsDeleted = false;
                    master.DeletedTime = null;
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
        
        [Route("ThirdPartyApps")]
        public IHttpActionResult PostThirdPartyApp(ThirdParty master)
        {
            try
            {
                using (MususAppEntities entity = new MususAppEntities())
                {
                    master.IsDeleted = false;
                    master.DeletedTime = null;
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

        [Route("Webpages")]
        public IHttpActionResult PostWebpages(WebPageUrl master)
        {
            try
            {
                using (MususAppEntities entity = new MususAppEntities())
                {
                    master.IsDeleted = false;
                    master.DeletedTime = null;                    
                    entity.WebPageUrls.Add(master);
                    entity.SaveChanges();
                    return Ok("web page app added successfully");
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);

            }
        }
        [Route("Webpages")]
        public IHttpActionResult PutWebpages(WebPageUrl master)
        {
            try
            {
                using (MususAppEntities entity = new MususAppEntities())
                {
                    var app = entity.WebPageUrls.FirstOrDefault(x => x.Id == master.Id);
                    app.Description = master.Description;
                    app.Documents = master.Documents;
                    app.Title = master.Title;
                    app.WebPageUrl1 = master.WebPageUrl1;
                    entity.SaveChanges();
                    return Ok("web page app changed successfully");
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);

            }
        }
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
