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
    [RoutePrefix("api/restaurants")]
    [RequireHttps]
    public class RestaurantsController : ApiController
    {
        [Route("")]
        public HttpResponseMessage GetRestaurants()
        {
            try
            {
                using (var db = new dbTourismEventAppEntities())
                {
                    var response = Request.CreateResponse(HttpStatusCode.OK, db.Restaurant.ToList());
                    return response;
                }

            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex.Message);
            }

        }
        [Route("{id:int}", Name = "GetRestaurantByID")]
        public HttpResponseMessage GetRestaurant(int? id)
        {
            using (var db = new dbTourismEventAppEntities())
            {
                var entity = db.Restaurant.FirstOrDefault(cat => cat.RestaurantID == id);
                if (entity != null)
                {
                    return Request.CreateResponse(HttpStatusCode.OK, entity);
                }
                else
                {
                    return Request.CreateErrorResponse(HttpStatusCode.NotFound, "The restaurant with id = " + id + " is not found");
                }

            }
        }

        [HttpPost]
        [Route("")]
        public HttpResponseMessage Post([FromBody] Restaurant restaurant)
        {
            try
            {
                using (var db = new dbTourismEventAppEntities())
                {
                    var entity = db.Restaurant.FirstOrDefault(cat => cat.RestaurantID == restaurant.RestaurantID);
                    if (entity == null)
                    {
                        db.Restaurant.Add(restaurant);
                        db.SaveChanges();
                        var response = Request.CreateResponse(HttpStatusCode.Created, restaurant);
                        response.Headers.Location = new Uri(Url.Link("GetRestaurantByID", new { id = restaurant.RestaurantID }));
                        return response;
                    }
                    else
                    {
                        return Request.CreateErrorResponse(HttpStatusCode.Conflict, "The Restaurant name is already exist");
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
        public HttpResponseMessage Update(int? id, [FromBody] Restaurant Restaurant)
        {
            try
            {
                using (var db = new dbTourismEventAppEntities())
                {
                    var entity = db.Restaurant.FirstOrDefault(cat => cat.RestaurantID == id);
                    if (entity != null)
                    {
                        db.Entry(Restaurant).State = System.Data.Entity.EntityState.Modified;
                        db.SaveChanges();
                        return Request.CreateResponse(HttpStatusCode.OK, entity);
                    }
                    else
                    {
                        return Request.CreateErrorResponse(HttpStatusCode.NotFound, "The restaurant with id = " + id + " is not found");
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
                var entity = db.Restaurant.FirstOrDefault(cat => cat.RestaurantID == id);
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
    }
}

