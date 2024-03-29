﻿using api.appstore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Http.Cors;

namespace api.appstore.Controllers
{    
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    [Authorize]
    public partial class AppController : ApiController
    {
        [Route("HostedApps")]
        public IHttpActionResult GetAllApps(int page=1,int size=10)
        {
            try
            {
                using (MususAppEntities entity = new MususAppEntities())
                {
                    List<AppMaster> appmasters = entity.AppMasters.OrderBy(x => x.Documents).Skip(page-1).Take(10).ToList();
                    List<AppMasterDto> apps = new List<AppMasterDto>(); 
                    foreach(var master in appmasters)
                    {
                        AppMasterDto dto = new AppMasterDto();
                        MapHostedAppObject(master, dto);
                        apps.Add(dto);
                    }                    
                    return Ok(apps);                    
                }                    
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [Route("HostedApps/{appId}")]
        public IHttpActionResult GetApp(Guid appId)
        {
            try
            {
                using (MususAppEntities entity = new MususAppEntities())
                {                    
                    var appMaster = entity.AppMasters.FirstOrDefault(x => x.Id == appId);
                    AppMasterDto app = new AppMasterDto();
                    var rating = entity.Ratings.Where(x => x.AppId == appMaster.Id);
                    if (rating != null && rating.Count()>0)
                    {                        
                        app.Rating = rating.Average(x=>x.Point);
                    }
                    else
                    {
                        app.Rating = 0;
                    }
                    var useRating = entity.Ratings.FirstOrDefault(x => x.AppId == appMaster.Id 
                    && x.Username.Contains(RequestContext.Principal.Identity.Name));
                    if(useRating!=null)
                    {
                        app.UserRating = useRating.Point;
                    }
                    GetPostComment(entity, appMaster, app, RequestContext.Principal.Identity.Name.ToString());
                    MapHostedAppObject(appMaster, app);
                    return Ok(app);
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        private static void GetPostComment(MususAppEntities entity, AppMaster appMaster, AppMasterDto app, string userName)
        {
            var userPost = entity.Posts.FirstOrDefault(x => x.AppId == appMaster.Id && x.UserName == userName);
            if(userPost!=null)
            {
                var userCR = new CommentReview()
                {
                    Comment=userPost.Txt,
                    CommentDate=userPost.CreateTime,
                    Username=userName,
                    Id=userPost.Id
                };
                var userComment=entity.Comments.FirstOrDefault(x => x.PostId == userPost.Id);
                if (userComment != null)
                {
                    userCR.Review = userComment.Msg;
                    userCR.ReviewDate = userComment.CreateTime;
                    userCR.ReviewUsername = userComment.UserName;
                }
                app.UserComment = userCR;
            }
            if(app.UserComment==null)
            {
                var posts = entity.Posts.Where(x => x.AppId == appMaster.Id).OrderByDescending(x => x.CreateTime);
                if (posts != null)
                {
                    foreach (var post in posts)
                    {
                        var review = entity.Comments.FirstOrDefault(x => x.PostId == post.Id);
                        if (review != null)
                        {
                            app.CommentReviews.Add(new CommentReview()
                            {
                                Id = post.Id,
                                Comment = post.Txt,
                                CommentDate = post.CreateTime,
                                Review = review.Msg,
                                ReviewDate = review.CreateTime,
                                ReviewUsername = review.UserName,
                                Username = post.UserName
                            });
                        }
                        else
                        {
                            app.CommentReviews.Add(new CommentReview()
                            {
                                Id = post.Id,
                                Comment = post.Txt,
                                CommentDate = post.CreateTime
                            });
                        }
                    }
                }
            }
            else
            {
                var posts = entity.Posts.Where(x => x.AppId == appMaster.Id).OrderByDescending(x => x.CreateTime);
                if (posts != null)
                {
                    foreach (var post in posts)
                    {
                        var review = entity.Comments.FirstOrDefault(x => x.PostId == post.Id);
                        if (review != null)
                        {
                            app.CommentReviews.Add(new CommentReview()
                            {
                                Id = post.Id,
                                Comment = post.Txt,
                                CommentDate = post.CreateTime,
                                Review = review.Msg,
                                ReviewDate = review.CreateTime,
                                ReviewUsername = review.UserName,
                                Username = post.UserName
                            });
                        }
                        else
                        {
                            app.CommentReviews.Add(new CommentReview()
                            {
                                Id = post.Id,
                                Comment = post.Txt,
                                CommentDate = post.CreateTime
                            });
                        }
                    }
                }                
            }           
        }
        [Route("Comments/{appId}")]
        public IHttpActionResult PostComment(Guid appId,CommentReview review)
        {
            try
            {
                using (MususAppEntities entity = new MususAppEntities())
                {
                    if(review!=null)
                    {
                        if(review.Id>0)
                        {
                            var post = entity.Posts.FirstOrDefault(x=>x.Id==review.Id);
                            post.Txt = review.Comment;
                            post.CreateTime = DateTime.UtcNow;                            
                            if (post != null && review.Review != null)
                            {
                                entity.SaveChanges();
                                var comment = entity.Comments.FirstOrDefault(x => x.PostId == post.Id);
                                comment.Msg = review.Review;
                                comment.UserName = review.ReviewUsername;
                                comment.CreateTime = DateTime.UtcNow;
                            }
                        }
                        else
                        {
                            var post=entity.Posts.Add(new Post()
                            {
                                AppId = appId,
                                CreateTime = DateTime.UtcNow,
                                Txt=review.Comment,
                                UserName=review.Username
                            });
                            entity.SaveChanges();
                            if (post != null)
                            {                                
                                entity.Comments.Add(new Comment()
                                {
                                    CreateTime = DateTime.UtcNow,
                                    PostId = post.Id,
                                    Msg = review.Review,
                                    UserName = null
                                });
                            }
                        }
                    }
                    entity.SaveChanges();
                    return Ok();
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        private void MapHostedAppObject(AppMaster master, AppMasterDto dto)
        {
            dto.AndriodSmartPhoneBuild = master.AndriodSmartPhoneBuild;
            dto.AndriodTabletBuild = master.AndriodTabletBuild;
            dto.CategoryId = master.CategoryId;
            dto.CreatedTime = master.CreatedTime;
            dto.DeletedTime = master.DeletedTime;
            dto.Description = master.Description;
            dto.Icon = master.Icon;
            dto.Id = master.Id;
            dto.IpadBuild = master.IpadBuild;
            dto.IpadPackageName = master.IpadPackageName;
            dto.IphoneBuild = master.IphoneBuild;
            dto.IphonePackageName = master.IphonePackageName;
            dto.IsDeleted = master.IsDeleted;
            dto.Published = master.Published;
            dto.Title = master.Title;
            dto.Version = master.Version;
            dto.Download = master.Download??0;
            if (master.Documents!=null)
            {
                var documents = master.Documents.Split(new char[] { ';' });   
                foreach(var doc in documents)
                {
                    dto.Documents.Add(doc);
                }
                
            }
            if (master.ScreenShots!=null)
            {
                var screenShots = master.ScreenShots.Split(new char[] { ';' });
                foreach (var doc in screenShots)
                {
                    dto.ScreenShots.Add(doc);
                }                
            }
        }


        [Route("ThirdPartyApps")]
        public IHttpActionResult GetThirdPartyApps(int page = 1, int size = 10)
        {
            try
            {
                using (MususAppEntities entity = new MususAppEntities())
                {
                    List<ThirdParty> appmasters = entity.ThirdParties.OrderBy(x => x.Documents).Skip(page - 1).Take(10).ToList();
                    List<ThirdPartyDto> apps = new List<ThirdPartyDto>();
                    foreach(var app in appmasters)
                    {
                        ThirdPartyDto dto = new ThirdPartyDto();
                        MapThirdPartyApp(app, dto);
                        apps.Add(dto);
                    }
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
                using (MususAppEntities entity = new MususAppEntities())
                {
                    var appMaster = entity.ThirdParties.FirstOrDefault(x => x.Id.ToString() == appId);
                    ThirdPartyDto app = new ThirdPartyDto();
                    var rating = entity.Ratings.Where(x => x.AppId == appMaster.Id);
                    if (rating != null && rating.Count() > 0)
                    {
                        app.Rating = rating.Average(x => x.Point);
                    }
                    else
                    {
                        app.Rating = 0;
                    }
                    MapThirdPartyApp(appMaster, app);
                    return Ok(app);
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        private void MapThirdPartyApp(ThirdParty master, ThirdPartyDto dto)
        {            
            dto.CategoryId = master.CategoryId;
            dto.CreatedTime = master.CreatedTime;
            dto.DeletedTime = master.DeletedTime;
            dto.Description = master.Description;
            
            dto.Id = master.Id;
            dto.IsDeleted = master.IsDeleted;
            dto.ThirdPartyAppUrl = master.ThirdPartyAppUrl;            
            dto.Title = master.Title;
            dto.Version = master.Version;
            dto.Download = master.Download??0;
            if (master.Documents!=null)
            {
                var documents = master.Documents.Split(new char[] { ';' });
                dto.Documents = documents.ToList();
            }            
        }


        [Route("Webpages")]
        public IHttpActionResult GetWebPages(int page = 1, int size = 10)
        {
            try
            {
                using (MususAppEntities entity = new MususAppEntities())
                {
                    List<WebPageUrl> apps = entity.WebPageUrls.OrderBy(x=>x.Documents).Skip(page - 1).Take(10).ToList();
                    List<WebpageUrlDto> dtos = new List<WebpageUrlDto>();
                    foreach (var app in apps)
                    {
                        WebpageUrlDto dto = new WebpageUrlDto();
                        MapWebpageApp(app, dto);
                        dtos.Add(dto);
                    }
                    return Ok(dtos);
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
                using (MususAppEntities entity = new MususAppEntities())
                {
                    var appMaster = entity.WebPageUrls.FirstOrDefault(x => x.Id.ToString() == appId);
                    WebpageUrlDto app = new WebpageUrlDto();
                    var rating = entity.Ratings.Where(x => x.AppId == appMaster.Id);
                    if (rating != null && rating.Count() > 0)
                    {
                        app.Rating = rating.Average(x => x.Point);
                    }
                    else
                    {
                        app.Rating = 0;
                    }
                    MapWebpageApp(appMaster, app);
                    return Ok(app);
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        private void MapWebpageApp(WebPageUrl master, WebpageUrlDto dto)
        {           
            dto.CreatedTime = master.CreatedTime;
            dto.DeletedTime = master.DeletedTime;
            dto.Description = master.Description;
            dto.Id = master.Id;
            dto.IsDeleted = master.IsDeleted;         
            dto.Title = master.Title;
            dto.WebPageUrl = master.WebPageUrl1;
            dto.Download = master.Download??0;
            if (master.Documents!=null)
            {
                var documents = master.Documents.Split(new char[] { ';' });
                dto.Documents = documents.ToList();
            }
        }


        [Route("Documents")]
        public IHttpActionResult GetDocuments(int page = 1, int size = 10)
        {
            try
            {
                using (MususAppEntities entity = new MususAppEntities())
                {                    
                    List<DocumentMaster> masters = entity.DocumentMasters.OrderBy(x=>x.Documents).Skip(page - 1).Take(10).ToList();

                    List<DocumentMasterDto> dtos = new List<DocumentMasterDto>();
                    foreach (var master in masters)
                    {
                        DocumentMasterDto dto = new DocumentMasterDto();
                        MapDocuments(master, dto);
                        dtos.Add(dto);
                    }

                    return Ok(dtos);
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
                using (MususAppEntities entity = new MususAppEntities())
                {
                    var appMaster = entity.DocumentMasters.FirstOrDefault(x => x.Id.ToString() == appId);
                    var rating = entity.Ratings.Where(x => x.AppId == appMaster.Id);
                    DocumentMasterDto app = new DocumentMasterDto();
                    if (rating != null && rating.Count() > 0)
                    {
                        app.Rating = rating.Average(x => x.Point);
                    }
                    else
                    {
                        app.Rating = 0;
                    }
                    MapDocuments(appMaster, app);
                    return Ok(app);
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        private void MapDocuments(DocumentMaster master, DocumentMasterDto dto)
        {
            dto.CreatedTime = master.CreatedTime;
            dto.DeletedTime = master.DeletedTime;
            dto.Description = master.Description;
            dto.Id = master.Id;
            dto.IsDeleted = master.IsDeleted;
            dto.Title = master.Title;
            dto.Download = master.Download??0;
            if (master.Documents!=null)
            {
                var documents = master.Documents.Split(new char[] { ';' });
                dto.Documents = documents.ToList();
            }
        }

        [Route("Categories")]
        public IHttpActionResult GetCategories()
        {
            try
            {
                using (MususAppEntities entity = new MususAppEntities())
                {
                    var app = entity.Categories.ToList();
                    return Ok(app);
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [Route("Categories")]
        public IHttpActionResult PostCategory(Category category)
        {
            try
            {
                using (MususAppEntities entity = new MususAppEntities())
                {
                    entity.Categories.Add(category);
                    entity.SaveChanges();
                    return Ok(category);
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [Route("Categories/{id}")]
        public IHttpActionResult DeleteCategory(int id)
        {
            try
            {
                using (MususAppEntities entity = new MususAppEntities())
                {
                    var cat = entity.Categories.FirstOrDefault(x => x.Id == id);
                    if(cat==null)
                    {
                        return BadRequest();
                    }
                    else
                    {
                        entity.Categories.Remove(cat);
                        entity.SaveChanges();
                        return Ok();
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
