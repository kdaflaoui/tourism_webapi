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

    public partial class Category
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Category()
        {
            this.Cinema = new HashSet<Cinema>();
            this.Event = new HashSet<Event>();
            this.Hotel = new HashSet<Hotel>();
            this.Museum = new HashSet<Museum>();
            this.Restaurant = new HashSet<Restaurant>();
        }
    
        public int CategoryID { get; set; }
        [DisplayName("Category Name")]
        public string CategoryName { get; set; }
        public string Description { get; set; }
        [DisplayName("Is the category active?")]
        public bool IsActive { get; set; }
        [DisplayName("Is the category to be deleted?")]
        public bool IsDeleted { get; set; }
        [DisplayName("Is the category featured?")]
        public bool IsFeatured { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public  ICollection<Cinema> Cinema { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public ICollection<Event> Event { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public  ICollection<Hotel> Hotel { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public  ICollection<Museum> Museum { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public  ICollection<Restaurant> Restaurant { get; set; }
    }
}
