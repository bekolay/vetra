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
    
    public partial class AnaSayfa
    {
        public int Anasayfaid { get; set; }
        public int kullanicid { get; set; }
        public string AnasayfaAdi { get; set; }
        public string AnasayfaİsimAciklama { get; set; }
        public string AnasayfaElektrik { get; set; }
        public string AnasayfaElektrikAciklama { get; set; }
        public string AHakkindaİsim { get; set; }
        public string AHakkindaAciklama { get; set; }
        public string ADunyaİsim { get; set; }
        public string ADunyaAciklama { get; set; }
        public string Atarih { get; set; }
    
        public virtual Kullanicilar Kullanicilar { get; set; }
    }
}
