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
    using System.ComponentModel;

    public partial class Location
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Location()
        {
            this.Cinema = new HashSet<Cinema>();
            this.Event = new HashSet<Event>();
            this.Hotel = new HashSet<Hotel>();
            this.Museum = new HashSet<Museum>();
            this.Restaurant = new HashSet<Restaurant>();
        }
    
        public int LocationID { get; set; }
        [DisplayName("The address of the event : ")]
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
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public  ICollection<Cinema> Cinema { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public  ICollection<Event> Event { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public  ICollection<Hotel> Hotel { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public  ICollection<Museum> Museum { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public  ICollection<Restaurant> Restaurant { get; set; }
    }
}