using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Cors;
using TourismDataAccess;
using TourismService.Authentification;

namespace TourismService.Controllers
{
    [RoutePrefix("api/categories")]
    //[EnableCorsAttribute("*","*","*")]
    public class CategoriesController : ApiController
    {
        [Route("")]
        public HttpResponseMessage GetCategories()
        {
            try
            {
                using (var db = new dbTourismEventAppEntities())
                {
                    var response = Request.CreateResponse(HttpStatusCode.OK, db.Category.ToList());
                    return response;
                }

            }catch(Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex.Message);
            }

        }
        [Route("{id:int}", Name ="GetCategoryByID")]
        public HttpResponseMessage GetCategories(int? id)
        {
             using(var db = new dbTourismEventAppEntities())
            {
                var entity = db.Category.FirstOrDefault(cat => cat.CategoryID == id);
                if(entity != null)
                {
                   return Request.CreateResponse(HttpStatusCode.OK, entity);
                }
                else
                {
                    return Request.CreateErrorResponse(HttpStatusCode.NotFound, "The catagory with id = " + id + " is not found");
                }
                
            }
        }

        [HttpPost]
        [Route("")]
        public HttpResponseMessage Post([FromBody] Category category)
        {
            try
            {
                using (var db = new dbTourismEventAppEntities())
                {
                    var entity = db.Category.FirstOrDefault(cat => cat.CategoryID == category.CategoryID);
                    if(entity == null)
                    {
                        db.Category.Add(category);
                        db.SaveChanges();
                        var response = Request.CreateResponse(HttpStatusCode.Created, category);
                        response.Headers.Location = new Uri(Url.Link("GetCategoryByID", new  { id = category.CategoryID }));
                        return response;
                    }
                    else
                    {
                        return Request.CreateErrorResponse(HttpStatusCode.Conflict, "The category name is already exist");
                    }

                }
            }catch(Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex.Message);
            }
        }

        [HttpPut]
        [Route("")]
        public HttpResponseMessage Update([FromBody] Category category)
        {
            try
            {
                using (var db = new dbTourismEventAppEntities())
                {
                    var entity = db.Category.FirstOrDefault(cat => cat.CategoryID == category.CategoryID);
                    if (entity != null)
                    {

                        //db.Category.Attach(category);
                        //db.Entry(category).State = System.Data.Entity.EntityState.Modified;
                        entity.CategoryName = category.CategoryName;
                        entity.Description = category.Description;
                        entity.IsActive = category.IsActive;
                        entity.IsDeleted = category.IsDeleted;
                        entity.IsFeatured = category.IsFeatured;

                        db.SaveChanges();
                        return Request.CreateResponse(HttpStatusCode.OK, entity);
                    }
                    else
                    {
                        return Request.CreateErrorResponse(HttpStatusCode.NotFound, "The catagory with id = " + category.CategoryName + " is not found");
                    }
                }
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex.Message);
            }

        }

        [HttpDelete]
        [Route("{id:int}")]
        public HttpResponseMessage Delete(int? id)
        {
            using (var db = new dbTourismEventAppEntities())
            {
                var entity = db.Category.FirstOrDefault(cat => cat.CategoryID == id);
                if (entity != null)
                {
                    db.Entry(entity).State = System.Data.Entity.EntityState.Deleted;
                    db.SaveChanges();
                    return Request.CreateResponse(HttpStatusCode.OK, entity);
                }
                else
                {
                    return Request.CreateErrorResponse(HttpStatusCode.NotFound, "The catagory with id = " + id + " is not found");
                }

            }
        }

        //[Route("{category:alpha}")]
        //public HttpResponseMessage GetCategoriesByName(string category)
        //{
        //    using (var db = new dbTourismEventAppEntities())
        //    {
        //        var events = db.Category.Where(cat => cat.CategoryName.Equals("Event")).Include(cat=> cat.Event).ToList();
        //        if (events != null)
        //        {
        //            return Request.CreateResponse(HttpStatusCode.OK, events);
        //        }
        //        else
        //        {
        //            return Request.CreateErrorResponse(HttpStatusCode.NotFound, "The catagory with id is not found");
        //        }

        //    }
        //}
    }
}
