using api.appstore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;

namespace api.appstore.Controllers
{
    [Authorize]
    public partial class AppController : ApiController
    {
        [Route("HostedApps")]
        public IHttpActionResult GetAllApps(int page=1,int size=10)
        {
            try
            {
                using (appStoreEntities entity = new appStoreEntities())
                {
                    List<AppMaster> apps = entity.AppMasters.OrderBy(x => x.Documents).Skip(page-1).Take(10).ToList();
                    return Ok(apps);                    
                }                    
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [Route("HostedApps/{appId}")]
        public IHttpActionResult GetApp(string appId)
        {
            try
            {
                using (appStoreEntities entity = new appStoreEntities())
                {
                    var app = entity.AppMasters.FirstOrDefault(x => x.Id.ToString() == appId);
                    return Ok(app);
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [Route("ThirdPartyApps")]
        public IHttpActionResult GetThirdPartyApps(int page = 1, int size = 10)
        {
            try
            {
                using (appStoreEntities entity = new appStoreEntities())
                {
                    List<ThirdParty> apps = entity.ThirdParties.OrderBy(x => x.Documents).Skip(page - 1).Take(10).ToList();
                    return Ok(apps);
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [Route("ThirdPartyApps/{appId}")]
        public IHttpActionResult GetThirdPartyApp(string appId)
        {
            try
            {
                using (appStoreEntities entity = new appStoreEntities())
                {
                    var app = entity.ThirdParties.FirstOrDefault(x => x.Id.ToString() == appId);
                    return Ok(app);
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [Route("Webpages")]
        public IHttpActionResult GetWebPages(int page = 1, int size = 10)
        {
            try
            {
                using (appStoreEntities entity = new appStoreEntities())
                {
                    List<WebPageUrl> apps = entity.WebPageUrls.OrderBy(x=>x.Documents).Skip(page - 1).Take(10).ToList();
                    return Ok(apps);
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [Route("Webpages/{appId}")]
        public IHttpActionResult GetWebpageApp(string appId)
        {
            try
            {
                using (appStoreEntities entity = new appStoreEntities())
                {
                    var app = entity.WebPageUrls.FirstOrDefault(x => x.Id.ToString() == appId);
                    return Ok(app);
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [Route("Documents")]
        public IHttpActionResult GetDocuments(int page = 1, int size = 10)
        {
            try
            {
                using (appStoreEntities entity = new appStoreEntities())
                {                    
                    List<DocumentMaster> apps = entity.DocumentMasters.OrderBy(x=>x.Documents).Skip(page - 1).Take(10).ToList();
                    return Ok(apps);
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [Route("Documents/{appId}")]
        public IHttpActionResult GetDocument(string appId)
        {
            try
            {
                using (appStoreEntities entity = new appStoreEntities())
                {
                    var app = entity.DocumentMasters.FirstOrDefault(x => x.Id.ToString() == appId);
                    return Ok(app);
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
