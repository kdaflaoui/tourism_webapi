using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using TourismDataAccess;
using TourismService.Authentification;

namespace TourismService.Controllers
{
    [RoutePrefix("api/museums")]
    [RequireHttps]
    public class MuseumsController : ApiController
    {
        [Route("")]
        public HttpResponseMessage GetMuseums()
        {
            try
            {
                using (var db = new dbTourismEventAppEntities())
                {
                    var response = Request.CreateResponse(HttpStatusCode.OK, db.Museum.ToList());
                    return response;
                }

            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex.Message);
            }

        }
        [Route("{id:int}", Name = "GetMuseumByID")]
        public HttpResponseMessage GetCategories(int? id)
        {
            using (var db = new dbTourismEventAppEntities())
            {
                var entity = db.Museum.FirstOrDefault(cat => cat.MuseumID == id);
                if (entity != null)
                {
                    return Request.CreateResponse(HttpStatusCode.OK, entity);
                }
                else
                {
                    return Request.CreateErrorResponse(HttpStatusCode.NotFound, "The Museum with id = " + id + " is not found");
                }

            }
        }

        [HttpPost]
        [Route("")]
        public HttpResponseMessage Post([FromBody] Museum museum)
        {
            try
            {
                using (var db = new dbTourismEventAppEntities())
                {
                    var entity = db.Museum.FirstOrDefault(cat => cat.MuseumID == museum.MuseumID);
                    if (entity == null)
                    {
                        db.Museum.Add(museum);
                        db.SaveChanges();
                        var response = Request.CreateResponse(HttpStatusCode.Created, museum);
                        response.Headers.Location = new Uri(Url.Link("GetMuseumByID", new { id = museum.MuseumID }));
                        return response;
                    }
                    else
                    {
                        return Request.CreateErrorResponse(HttpStatusCode.Conflict, "The Museum name is already exist");
                    }

                }
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex.Message);
            }
        }

        [HttpPut]
        [Route("{id:int}")]
        public HttpResponseMessage Update(int? id, [FromBody] Museum Museum)
        {
            try
            {
                using (var db = new dbTourismEventAppEntities())
                {
                    var entity = db.Museum.FirstOrDefault(cat => cat.MuseumID == id);
                    if (entity != null)
                    {
                        db.Entry(Museum).State = System.Data.Entity.EntityState.Modified;
                        db.SaveChanges();
                        return Request.CreateResponse(HttpStatusCode.OK, entity);
                    }
                    else
                    {
                        return Request.CreateErrorResponse(HttpStatusCode.NotFound, "The Museum with id = " + id + " is not found");
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
        public HttpResponseMessage Update(int? id)
        {
            using (var db = new dbTourismEventAppEntities())
            {
                var entity = db.Museum.FirstOrDefault(cat => cat.MuseumID == id);
                if (entity != null)
                {
                    db.Entry(entity).State = System.Data.Entity.EntityState.Deleted;
                    db.SaveChanges();
                    return Request.CreateResponse(HttpStatusCode.OK, entity);
                }
                else
                {
                    return Request.CreateErrorResponse(HttpStatusCode.NotFound, "The Museum with id = " + id + " is not found");
                }

            }
        }
    }
}

