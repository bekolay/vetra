using burak.Models;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using veri;

namespace burak.Areas.yonetim.Controllers
{
    public class hesapController : Controller
    {
        private burakEntities db = new burakEntities();
        // GET: yonetim/hesap
        public ActionResult giris()
        {
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult Giris(LoginModel model, string ReturnUrl)
        {
            if (ModelState.IsValid)
            {
                //string password = Tools.Md5(model.Password);
                string password = model.KullaniciSifre.Md5();


                Kullanicilar user = db.Kullanicilars.FirstOrDefault(p => p.kullaniciAdi == model.kullaniciAdi && p.KullaniciSifre == password );

                if (user == null)
                {
                    TempData["Message"] = "Kullanıcı adı veya şifreniz hatalı.";
                    return View();
                }

                if (user.KullaniciAktifmi == false)
                {
                    TempData["Message"] = "Banlandınız acaba kim bilir ne yaptınız :)";
                    return View();
                }


                FormsAuthentication.RedirectFromLoginPage(user.kullaniciAdi.ToString(), model.BeniHatirla);
            }

            return Redirect(ReturnUrl ?? "/yonetim/hesap/");
        }

        [Authorize]
        public ActionResult Cikis()
        {
            Kullanicilar kullanici3 = db.Kullanicilars.FirstOrDefault(p => p.kullaniciAdi == p.kullaniciAdi);

            Session["Kullanici3"] = kullanici3.KullaniciAdiSoyadi;

            FormsAuthentication.SignOut();

            return RedirectToAction("Index", "Home");
        }

        [Authorize]
        public ActionResult yetki()
        {
            return View();
        }
    }
}