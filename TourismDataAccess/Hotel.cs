//------------------------------------------------------------------------------
// <auto-generated>
//     Ce code a été généré à partir d'un modèle.
//
//     Des modifications manuelles apportées à ce fichier peuvent conduire à un comportement inattendu de votre application.
//     Les modifications manuelles apportées à ce fichier sont remplacées si le code est régénéré.
// </auto-generated>
//------------------------------------------------------------------------------

namespace TourismDataAccess
{
    using System;
    using System.Collections.Generic;
    
    public partial class Hotel
    {
        public int HotelID { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string ImagePath { get; set; }
        public double Price { get; set; }
        public int Rating { get; set; }
        public bool IsActive { get; set; }
        public bool IsDeleted { get; set; }
        public bool IsFeatured { get; set; }
        public int CategoryID { get; set; }
        public int LocationID { get; set; }
    
        public Category Category { get; set; }
        public Location Location { get; set; }
    }
}
