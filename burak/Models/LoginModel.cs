using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace burak.Models
{
    public class LoginModel
    {
        [Required(ErrorMessage = "Kullanıcı Adı Boş Geçilemez")]
        public string kullaniciAdi { get; set; }

        [Required(ErrorMessage = "Şifre Boş Geçilemez")]
        public string KullaniciSifre { get; set; }

        public bool BeniHatirla { get; set; }
    }
}