using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;
using TourismDataAccess;
using TourismService.Models.Admin;

namespace TourismService.Controllers
{
    public class AdminController : Controller
    {
        // GET: Admin
        public ActionResult Dashboard()
        {
            return View();
        }

        // -------------------- Category -------------------------------------------------------------

        public ActionResult GetGategories()
        {

            IEnumerable<Category> categories = null;
            using(var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://localhost:44348/api/");
                var responseTask = client.GetAsync("categories");
                responseTask.Wait();
                var results = responseTask.Result;
                if (results.IsSuccessStatusCode)
                {
                    var readJob = results.Content.ReadAsAsync<List<Category>>();
                    readJob.Wait();
                    categories = readJob.Result;
                }
                else
                {
                    categories = Enumerable.Empty<Category>();
                    ModelState.AddModelError(string.Empty, "An error is occured during the processing");
                }
            }
            return View(categories);
        }
        public ActionResult EditCategory(int? id)
        {
            Category categorie = null;
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://localhost:44348/api/categories/");
                var responseTask = client.GetAsync(id.ToString());
                responseTask.Wait();
                var results = responseTask.Result;
                if (results.IsSuccessStatusCode)
                {
                    var readJob = results.Content.ReadAsAsync<Category>();
                    readJob.Wait();
                    categorie = readJob.Result;
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "An error is occured during the processing");
                }
            }
            return View(categorie);
        }

        [HttpPost]
        public ActionResult EditCategory(Category category)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://localhost:44348/api/");
                var responseTask = client.PutAsJsonAsync<Category>("categories", category);
                responseTask.Wait();
                var results = responseTask.Result;
                if (results.IsSuccessStatusCode)
                {
                    var readJob = results.Content.ReadAsAsync<Category>();
                    readJob.Wait();
                    return RedirectToAction("GetGategories");
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "An error is occured during the processing");
                    return View("EditCategory");
                }
            }
            
        }

        public ActionResult AddCategory()
        {
            return View();
        }

        [HttpPost]
        public ActionResult AddCategory(Category category)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://localhost:44348/api/");
                var responseTask = client.PostAsJsonAsync<Category>("categories", category);
                responseTask.Wait();
                var results = responseTask.Result;
                if (results.IsSuccessStatusCode)
                {
                    var readJob = results.Content.ReadAsAsync<Category>();
                    readJob.Wait();
                    return RedirectToAction("GetGategories");
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "An error is occured during the processing");
                    return View("AddCategory");
                }
            }
        }


        public ActionResult DeleteCategory(int? id)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://localhost:44348/api/categories/");
                var responseTask = client.DeleteAsync(id.ToString());
                responseTask.Wait();
                var results = responseTask.Result;
                if (results.IsSuccessStatusCode)
                {
                    var readJob = results.Content.ReadAsAsync<Category>();
                    readJob.Wait();
                    return RedirectToAction("GetGategories");
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "An error is occured during the processing");
                    return View("EditCategory");
                }
            }

        }


        // -------------------- Events -------------------------------------------------------------

        public ActionResult GetEvents()
        {

            IEnumerable<Event> events = null;
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://localhost:44348/api/");
                var responseTask = client.GetAsync("events");
                responseTask.Wait();
                var results = responseTask.Result;
                if (results.IsSuccessStatusCode)
                {
                    var readJob = results.Content.ReadAsAsync<List<Event>>();
                    readJob.Wait();
                    events = readJob.Result;
                }
                else
                {
                    events = Enumerable.Empty<Event>();
                    ModelState.AddModelError(string.Empty, "An error is occured during the processing");
                }
            }
            return View(events);
        }

        public ActionResult EditEvent(int? id)
        {
            ViewBag.Categories = GetMapGategories();
            Event entity = null;
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://localhost:44348/api/events/");
                var responseTask = client.GetAsync(id.ToString());
                responseTask.Wait();
                var results = responseTask.Result;
                if (results.IsSuccessStatusCode)
                {
                    var readJob = results.Content.ReadAsAsync<Event>();
                    readJob.Wait();
                    entity = readJob.Result;
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "An error is occured during the processing");
                }
            }
            return View(entity);
        }


        [HttpPost]
        public ActionResult EditEvent(Event entity, HttpPostedFileBase file)
        {
            ViewBag.Categories = GetMapGategories();
            string filename = "";
            if (file != null)
            {
                filename = System.IO.Path.GetFileName(file.FileName);
                string path = System.IO.Path.Combine(Server.MapPath("~/Content/Images/Events/"), filename);
                file.SaveAs(path);
            }
            entity.ImagePath = file != null ? filename : entity.ImagePath;

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://localhost:44348/api/");
                var responseTask = client.PutAsJsonAsync<Event>("events", entity);
                responseTask.Wait();
                var results = responseTask.Result;
                if (results.IsSuccessStatusCode)
                {
                    var readJob = results.Content.ReadAsAsync<Event>();
                    readJob.Wait();
                    return RedirectToAction("GetEvents");
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "An error is occured during the processing");
                    return View("EditEvent");
                }
            }

        }
        public ActionResult AddEvent()
        {
            ViewBag.Categories = GetMapGategories();
            return View();
        }

        [HttpPost]
        public ActionResult AddEvent(LocationEventModel entity, HttpPostedFileBase file)
        {
            Event evnt = new Event();

            ViewBag.Categories = GetMapGategories();

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
            Location location = new Location();

            location.Address_1 = entity.Address_1;
            location.Address_2 = entity.Address_2;
            location.City = entity.City;
            location.PostalCode = entity.PostalCode;
            location.Laltitude = entity.Laltitude;
            location.Longitude = entity.Longitude;

            evnt.Location = location;

            

            string filename = "";
            if (file != null)
            {
                filename = System.IO.Path.GetFileName(file.FileName);
                string path = System.IO.Path.Combine(Server.MapPath("~/Content/Images/Events/"), filename);
                file.SaveAs(path);
            }

            evnt.ImagePath = file != null ? filename : entity.ImagePath;

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://localhost:44348/api/");
                var responseTask = client.PostAsJsonAsync<Event>("events", evnt);
                responseTask.Wait();
                var results = responseTask.Result;
                if (results.IsSuccessStatusCode)
                {
                    var readJob = results.Content.ReadAsAsync<Event>();
                    readJob.Wait();
                    return RedirectToAction("GetEvents");
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "An error is occured during the processing");
                    return View("AddEvent");
                }
            }
        }


        // ------------------------------ Utils ------------------------------------------------------------------

        public IEnumerable<SelectListItem> GetMapGategories()
        {
            List<SelectListItem> selectList = new List<SelectListItem>();

            IEnumerable<Category> categories = null;
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://localhost:44348/api/");
                var responseTask = client.GetAsync("categories");
                responseTask.Wait();
                var results = responseTask.Result;
                if (results.IsSuccessStatusCode)
                {
                    var readJob = results.Content.ReadAsAsync<List<Category>>();
                    readJob.Wait();
                    categories = readJob.Result;
                }
            }
            foreach (var cat in categories)
            {
                selectList.Add(new SelectListItem { Value = cat.CategoryID.ToString(), Text = cat.CategoryName });
            }
            return selectList;
        }
    }
}