//------------------------------------------------------------------------------
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
    using System.Collections.Generic;
    
    public partial class Kullanicilar
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Kullanicilar()
        {
            this.Hizmetlers = new HashSet<Hizmetler>();
            this.Kategorilers = new HashSet<Kategoriler>();
            this.Menus = new HashSet<Menu>();
            this.Menulers = new HashSet<Menuler>();
            this.SiteAyars = new HashSet<SiteAyar>();
            this.Sliders = new HashSet<Slider>();
            this.FooterLinklers = new HashSet<FooterLinkler>();
            this.AnaSayfas = new HashSet<AnaSayfa>();
        }
    
        public int kullanicid { get; set; }
        public string kullaniciAdi { get; set; }
        public string KullaniciAdiSoyadi { get; set; }
        public string KullaniciSifre { get; set; }
        public string KullaniciSifreTekrar { get; set; }
        public System.DateTime KullaniciTarih { get; set; }
        public bool KullaniciAktifmi { get; set; }
        public int KullaniciRolid { get; set; }
        public string KullaniciEmail { get; set; }
        public string KullaniciGuvenlik { get; set; }
        public string KullaniciGuvenlikCevap { get; set; }
        public string KullaniciAdminRol { get; set; }
    
        public virtual AdminRol AdminRol { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Hizmetler> Hizmetlers { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Kategoriler> Kategorilers { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Menu> Menus { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Menuler> Menulers { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<SiteAyar> SiteAyars { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Slider> Sliders { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<FooterLinkler> FooterLinklers { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<AnaSayfa> AnaSayfas { get; set; }
    }
}