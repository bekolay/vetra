﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace veri
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class burakEntities : DbContext
    {
        public burakEntities()
            : base("name=burakEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<AdminRol> AdminRols { get; set; }
        public virtual DbSet<Menu> Menus { get; set; }
        public virtual DbSet<Kategoriler> Kategorilers { get; set; }
        public virtual DbSet<Menuler> Menulers { get; set; }
        public virtual DbSet<Slider> Sliders { get; set; }
        public virtual DbSet<SiteAyar> SiteAyars { get; set; }
        public virtual DbSet<Hizmetler> Hizmetlers { get; set; }
        public virtual DbSet<Kullanicilar> Kullanicilars { get; set; }
        public virtual DbSet<FooterLinkler> FooterLinklers { get; set; }
        public virtual DbSet<AnaSayfa> AnaSayfas { get; set; }
    }
}
