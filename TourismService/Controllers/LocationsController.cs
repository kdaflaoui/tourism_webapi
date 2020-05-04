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
    [RoutePrefix("api/locations")]
    [RequireHttps]
    public class LocationsController : ApiController
    {
        [Route("")]
        public HttpResponseMessage GetLocations()
        {
            try
            {
                using (var db = new dbTourismEventAppEntities())
                {
                    var response = Request.CreateResponse(HttpStatusCode.OK, db.Location.ToList());
                    return response;
                }

            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex.Message);
            }

        }
        [Route("{id:int}", Name = "GetLocationByID")]
        public HttpResponseMessage GetLocation(int? id)
        {
            using (var db = new dbTourismEventAppEntities())
            {
                var entity = db.Location.FirstOrDefault(cat => cat.LocationID == id);
                if (entity != null)
                {
                    return Request.CreateResponse(HttpStatusCode.OK, entity);
                }
                else
                {
                    return Request.CreateErrorResponse(HttpStatusCode.NotFound, "The location with id = " + id + " is not found");
                }

            }
        }

        [HttpPost]
        [Route("")]
        public HttpResponseMessage Post([FromBody] Location location)
        {
            try
            {
                using (var db = new dbTourismEventAppEntities())
                {
                    var entity = db.Location.FirstOrDefault(cat => cat.LocationID == location.LocationID);
                    if (entity == null)
                    {
                        db.Location.Add(location);
                        db.SaveChanges();
                        var response = Request.CreateResponse(HttpStatusCode.Created, location);
                        response.Headers.Location = new Uri(Url.Link("GetLocationByID", new { id = location.LocationID }));
                        return response;
                    }
                    else
                    {
                        return Request.CreateErrorResponse(HttpStatusCode.Conflict, "The Location name is already exist");
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
        public HttpResponseMessage Update(int? id, [FromBody] Location Location)
        {
            try
            {
                using (var db = new dbTourismEventAppEntities())
                {
                    var entity = db.Location.FirstOrDefault(cat => cat.LocationID == id);
                    if (entity != null)
                    {
                        db.Entry(Location).State = System.Data.Entity.EntityState.Modified;
                        db.SaveChanges();
                        return Request.CreateResponse(HttpStatusCode.OK, entity);
                    }
                    else
                    {
                        return Request.CreateErrorResponse(HttpStatusCode.NotFound, "The location with id = " + id + " is not found");
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
                var entity = db.Location.FirstOrDefault(cat => cat.LocationID == id);
                if (entity != null)
                {
                    db.Entry(entity).State = System.Data.Entity.EntityState.Deleted;
                    db.SaveChanges();
                    return Request.CreateResponse(HttpStatusCode.OK, entity);
                }
                else
                {
                    return Request.CreateErrorResponse(HttpStatusCode.NotFound, "The location with id = " + id + " is not found");
                }

            }
        }
    }
}
