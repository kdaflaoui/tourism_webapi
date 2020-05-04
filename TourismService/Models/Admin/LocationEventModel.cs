using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace TourismService.Models.Admin
{
    public class LocationEventModel
    {

        [DisplayName("Image")]
        public string ImagePath { get; set; }
        [DisplayName("Name")]
        public string Title { get; set; }
        [DisplayName("Type")]
        public string Type { get; set; }
        [DisplayName("Description")]
        public string Description { get; set; }
        [DisplayName("Startime of the event")]
        public string StartTime { get; set; }
        [DisplayName("Endtime pf the event")]
        public string EndTime { get; set; }
        [DisplayName("Price")]
        public double Price { get; set; }
        [DisplayName("The rating")]
        public int Rating { get; set; }
        [DisplayName("Is the category active?")]
        public bool IsActive { get; set; }
        [DisplayName("Is the category to be deleted?")]
        public bool IsDeleted { get; set; }
        [DisplayName("Is the category featured?")]
        public bool IsFeatured { get; set; }
        [DisplayName("Category")]
        public int CategoryID { get; set; }
        [DisplayName("The address : ")]
        public string Address_1 { get; set; }
        [DisplayName("The compliment of the address : ")]
        public string Address_2 { get; set; }
        [DisplayName("The postal code :")]
        public string PostalCode { get; set; }
        [DisplayName("The city :")]
        public string City { get; set; }
        [DisplayName("The country :")]
        public string Country { get; set; }
        [DisplayName("The laltitude :")]
        public string Laltitude { get; set; }
        [DisplayName("The longitude :")]
        public string Longitude { get; set; }


    }
}