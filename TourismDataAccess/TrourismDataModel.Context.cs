﻿//------------------------------------------------------------------------------
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
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class dbTourismEventAppEntities : DbContext
    {
        public dbTourismEventAppEntities()
            : base("name=dbTourismEventAppEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<Category> Category { get; set; }
        public virtual DbSet<Cinema> Cinema { get; set; }
        public virtual DbSet<Event> Event { get; set; }
        public virtual DbSet<Hotel> Hotel { get; set; }
        public virtual DbSet<Location> Location { get; set; }
        public virtual DbSet<Museum> Museum { get; set; }
        public virtual DbSet<Restaurant> Restaurant { get; set; }
    }
}
