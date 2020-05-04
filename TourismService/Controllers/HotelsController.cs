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
    [RoutePrefix("api/hotels")]
    [RequireHttps]
    public class HotelsController : ApiController
    {
        [Route("")]
        public HttpResponseMessage GetHotels()
        {
            try
            {
                using (var db = new dbTourismEventAppEntities())
                {
                    var response = Request.CreateResponse(HttpStatusCode.OK, db.Hotel.ToList());
                    return response;
                }

            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex.Message);
            }

        }
        [Route("{id:int}", Name = "GetHotelByID")]
        public HttpResponseMessage GetHotel(int? id)
        {
            using (var db = new dbTourismEventAppEntities())
            {
                var entity = db.Hotel.FirstOrDefault(hotel => hotel.HotelID == id);
                if (entity != null)
                {
                    return Request.CreateResponse(HttpStatusCode.OK, entity);
                }
                else
                {
                    return Request.CreateErrorResponse(HttpStatusCode.NotFound, "The hotel with id = " + id + " is not found");
                }

            }
        }

        [HttpPost]
        [Route("")]
        public HttpResponseMessage Post([FromBody] Hotel hotel)
        {
            try
            {
                using (var db = new dbTourismEventAppEntities())
                {
                    var entity = db.Hotel.FirstOrDefault(cat => cat.HotelID == hotel.HotelID);
                    if (entity == null)
                    {
                        db.Hotel.Add(hotel);
                        db.SaveChanges();
                        var response = Request.CreateResponse(HttpStatusCode.Created, hotel);
                        response.Headers.Location = new Uri(Url.Link("GetHotelByID", new { id = hotel.HotelID }));
                        return response;
                    }
                    else
                    {
                        return Request.CreateErrorResponse(HttpStatusCode.Conflict, "The hotel name is already exist");
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
        public HttpResponseMessage Update(int? id, [FromBody] Hotel Hotel)
        {
            try
            {
                using (var db = new dbTourismEventAppEntities())
                {
                    var entity = db.Hotel.FirstOrDefault(cat => cat.HotelID == id);
                    if (entity != null)
                    {
                        db.Entry(Hotel).State = System.Data.Entity.EntityState.Modified;
                        db.SaveChanges();
                        return Request.CreateResponse(HttpStatusCode.OK, entity);
                    }
                    else
                    {
                        return Request.CreateErrorResponse(HttpStatusCode.NotFound, "The hotel with id = " + id + " is not found");
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
                var entity = db.Hotel.FirstOrDefault(cat => cat.HotelID == id);
                if (entity != null)
                {
                    db.Entry(entity).State = System.Data.Entity.EntityState.Deleted;
                    db.SaveChanges();
                    return Request.CreateResponse(HttpStatusCode.OK, entity);
                }
                else
                {
                    return Request.CreateErrorResponse(HttpStatusCode.NotFound, "The hotel with id = " + id + " is not found");
                }

            }
        }
    }
}
