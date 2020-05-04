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
    [RoutePrefix("api/events")]
    [RequireHttps]
    public class EventsController : ApiController
    {
        [Route("")]
        public HttpResponseMessage GetEvents()
        {
            try
            {
                using (var db = new dbTourismEventAppEntities())
                {
                    var response = Request.CreateResponse(HttpStatusCode.OK, db.Event.ToList());
                    return response;
                }

            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex.Message);
            }

        }
        [Route("{id:int}", Name = "GetEventByID")]
        public HttpResponseMessage GetEvent(int? id)
        {
            using (var db = new dbTourismEventAppEntities())
            {
                var entity = db.Event.FirstOrDefault(ev => ev.EventID == id);
                if (entity != null)
                {
                    return Request.CreateResponse(HttpStatusCode.OK, entity);
                }
                else
                {
                    return Request.CreateErrorResponse(HttpStatusCode.NotFound, "The event with id = " + id + " is not found");
                }

            }
        }

        [HttpPost]
        [Route("")]
        public HttpResponseMessage Post([FromBody] Event entity)
        {
            try
            {
                using (var db = new dbTourismEventAppEntities())
                {
                    var evn = db.Event.FirstOrDefault(ev => ev.EventID == entity.EventID);
                    if (evn == null)
                    {
                        db.Event.Add(entity);
                        db.SaveChanges();
                        var response = Request.CreateResponse(HttpStatusCode.Created, entity);
                        response.Headers.Location = new Uri(Url.Link("GetEventByID", new { id = entity.EventID}));
                        return response;
                    }
                    else
                    {
                        return Request.CreateErrorResponse(HttpStatusCode.Conflict, "The event name is already exist");
                    }

                }
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex.Message);
            }
        }

        [HttpPut]
        [Route("")]
        public HttpResponseMessage Update([FromBody] Event entity)
        {
            try
            {
                using (var db = new dbTourismEventAppEntities())
                {
                    var evnt = db.Event.FirstOrDefault(ev => ev.EventID == entity.EventID);
                    if (evnt != null)
                    {
                        //db.Entry(entity).State = System.Data.Entity.EntityState.Modified;
                        evnt.ImagePath = entity.ImagePath;
                        evnt.Title = entity.Title;
                        evnt.Type = entity.Type;
                        evnt.Description = entity.Description;
                        evnt.StartTime = entity.StartTime;
                        evnt.EndTime = entity.EndTime;
                        evnt.Price = entity.Price;
                        evnt.Rating = entity.Rating;
                        evnt.IsActive = entity.IsActive;
                        evnt.IsDeleted = entity.IsDeleted;
                        evnt.IsFeatured = entity.IsFeatured;
                        evnt.CategoryID = entity.CategoryID;
                        db.SaveChanges();
                        return Request.CreateResponse(HttpStatusCode.OK, entity);
                    }
                    else
                    {
                        return Request.CreateErrorResponse(HttpStatusCode.NotFound, "The event with id = " + entity.EventID+ " is not found");
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
                var entity = db.Event.FirstOrDefault(ev => ev.EventID == id);
                if (entity != null)
                {
                    db.Entry(entity).State = System.Data.Entity.EntityState.Deleted;
                    db.SaveChanges();
                    return Request.CreateResponse(HttpStatusCode.OK, entity);
                }
                else
                {
                    return Request.CreateErrorResponse(HttpStatusCode.NotFound, "The event with id = " + id + " is not found");
                }

            }
        }
    }
}
