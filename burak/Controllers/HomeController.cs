using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Diagnostics.Eventing.Reader;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using veri;
using WebMatrix.WebData;

namespace burak.Controllers
{
    public class HomeController : Controller
    {
        private burakEntities db = new burakEntities();
        public ActionResult Index()
        {
            ViewBag.deneme = db.Menulers.Include(P => P.Kategorilers).Where(p => p.MenuAktif == "true" & p.KategoriMi == "true"
                        & p.MenuMi == "false").ToList();


            ViewBag.deneme1 = db.Kategorilers.Include(P => P.Menuler).Where(p => p.Menuler.Menulerid == p.Menulerid).ToList();

            var anaSayfas = db.AnaSayfas.Include(a => a.Kullanicilar);
            return View(anaSayfas.ToList());
        }


        public ActionResult menu(string id)
        {
            var menuler = db.Menulers.Include(P => P.Kategorilers).Include(p => p.Menus)
                .FirstOrDefault(p => p.MenuAktif == "true" && p.MenuUrl == id);




            var menulericerik = db.Menus.Include(p => p.Kullanicilar).Include(p => p.Menuler).FirstOrDefault(p => p.Menuaktif == "true" && p.Menuler.MenuUrl == id);


            if (menulericerik == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.NotFound);
            }

            ViewBag.Adi = menulericerik.Menuler.MenuAdi;
            return View(menulericerik);
        }

        public ActionResult kategoriler(string id)
        {

            var menulericerik = db.Kategorilers.Include(p => p.Kullanicilar).Include(p => p.Menuler).FirstOrDefault(p => p.KategoriAktif == "true" && p.KategorilerUrl == id);


            if (menulericerik == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.NotFound);
            }
            ViewBag.Adi = menulericerik.kategoriAdi;
            return View(menulericerik);
        }

        public ActionResult hizmetler(string id)
        {

            var menulericerik = db.Hizmetlers.Include(p => p.Kullanicilar).FirstOrDefault(p => p.HizmetlerAktif == "true" && p.HizmetlerUrl == id);


            if (menulericerik == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.NotFound);
            }
            ViewBag.Adi = menulericerik.HizmetlerAdi;
            return View(menulericerik);
        }
    }
}