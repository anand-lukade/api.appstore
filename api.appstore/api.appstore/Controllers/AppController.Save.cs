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
                using (appStoreEntities entity = new appStoreEntities())
                {

                    if (master.Id != null)
                    {

                        var app = entity.AppMasters.FirstOrDefault(x => x.Id == master.Id);
                        app = master;
                        entity.SaveChanges();
                        return Ok("hosted app updated successfully");
                    }
                    else
                    {
                        entity.AppMasters.Add(master);
                        entity.SaveChanges();
                        return Ok("app updated successfully");
                    }
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
                using (appStoreEntities entity = new appStoreEntities())
                {
                    if (master.Id != null)
                    {
                        var app = entity.ThirdParties.FirstOrDefault(x => x.Id == master.Id);
                        app = master;
                        entity.SaveChanges();
                        return Ok("third party app changed successfully");
                    }
                    else
                    {
                        entity.ThirdParties.Add(master);
                        entity.SaveChanges();
                        return Ok("third party app added successfully");
                    }
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
                using (appStoreEntities entity = new appStoreEntities())
                {
                    if (master.Id != null)
                    {

                        var app = entity.WebPageUrls.FirstOrDefault(x => x.Id == master.Id);
                        app = master;
                        entity.SaveChanges();
                        return Ok("web page app changed successfully");

                    }
                    else
                    {
                        entity.WebPageUrls.Add(master);
                        entity.SaveChanges();
                        return Ok("web page app added successfully");
                    }
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
                using (appStoreEntities entity = new appStoreEntities())
                {
                    if (master.Id != null)
                    {
                        var app = entity.DocumentMasters.FirstOrDefault(x => x.Id == master.Id);
                        app = master;
                        entity.SaveChanges();
                        return Ok("web page app changed successfully");
                    }
                    else
                    {
                        entity.DocumentMasters.Add(master);
                        entity.SaveChanges();
                        return Ok("web page app added successfully");
                    }
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
