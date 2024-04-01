using burak.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using veri;
using WebMatrix.WebData;

namespace burak.Areas.yonetim.Controllers
{
    [Authorize]
    public class MenulersController : Controller
    {
        private burakEntities db = new burakEntities();

        // GET: yonetim/Menulers
        public ActionResult Index()
        {
            int kk = WebSecurity.GetUserId(User.Identity.Name);

            var kullanici = db.Kullanicilars.Where(p => p.kullanicid == kk).FirstOrDefault();
            if (kullanici.KullaniciAdminRol == "kullanici")
            {
                //var kullanicitek = db.Kullanicilars.FirstOrDefault(x => x.kullanicid == kk);
                //return RedirectToAction("edit/" + kullanici.kullanicid);
                return Redirect("/yonetim/hesap/yetki");
            }
            else if (kullanici.KullaniciAdminRol == "admin")
            {
                var menulers = db.Menulers.Include(m => m.Kullanicilar);
                return View(menulers.ToList());
            }
            else
            {

                return RedirectToAction("Bloglar");
            }
        }

        // GET: yonetim/Menulers/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Menuler menuler = db.Menulers.Find(id);
            if (menuler == null)
            {
                return HttpNotFound();
            }
            return View(menuler);
        }

        // GET: yonetim/Menulers/Create
        public ActionResult Create()
        {
            int kk = WebSecurity.GetUserId(User.Identity.Name);
            var kullanici = db.Kullanicilars.Where(p => p.kullanicid == kk).FirstOrDefault();
            if (kullanici.KullaniciAdminRol == "kullanici")
            {
                //var kullanicitek = db.Kullanicilars.FirstOrDefault(x => x.kullanicid == kk);
                //return RedirectToAction("edit/" + kullanici.kullanicid);
                return Redirect("/yonetim/hesap/yetki");
            }
            else if (kullanici.KullaniciAdminRol == "admin")
            {
                ViewBag.kullanicid = new SelectList(db.Kullanicilars, "kullanicid", "kullaniciAdi");
                return View();
            }
            else
            {

                return RedirectToAction("Bloglar");
            }
        }

        // POST: yonetim/Menulers/Create
        // Aşırı gönderim saldırılarından korunmak için bağlamak istediğiniz belirli özellikleri etkinleştirin. 
        // Daha fazla bilgi için bkz. https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Menulerid,MenuAdi,MenuUrl,MenuTarih,MenuAktif,KategoriMi,MenuMi,kullanicid")] Menuler menuler,
            [Bind(Include = "Menuid,Menulerid,Menutarih,Resim,Yazi,Menuaktif,kullanicid")] Menu menu)
        {
            var menulervar = db.Menulers.Where(p => p.MenuAdi == menuler.MenuAdi).FirstOrDefault();
            if (menulervar != null)
            {
                if (menulervar.MenuAdi == menulervar.MenuAdi)
                {
                    TempData["Message"] = "Bu kullanıcı daha önce eklenmiş";
                    ViewBag.kullanicid = new SelectList(db.Kullanicilars, "kullanicid", "kullaniciAdi", menuler.kullanicid);
                    return View();
                }
            }
            {
                if (ModelState.IsValid)
                {
                    menuler.MenuUrl = menuler.MenuAdi.Seo();
                    menuler.MenuAktif = "true";
                    menuler.MenuTarih = DateTime.Now.ToString();
                    menuler.kullanicid = WebSecurity.GetUserId(User.Identity.Name);
                    db.Menulers.Add(menuler);
                    db.SaveChanges();
                    int? lastProductId = db.Menus.Select(item => item.Menuid).Max();
                    var lastProductIdmenuler = db.Menulers.Max(item => item.Menulerid);
                    if (lastProductId == null)
                    {
                        ViewBag.kullanicid = new SelectList(db.Kullanicilars, "kullanicid", "kullaniciAdi", menuler.kullanicid);
                        menu.Menutarih = DateTime.Now.ToString();
                        menu.kullanicid = WebSecurity.GetUserId(User.Identity.Name);
                        menu.Yazi = "Yazınızı yazınız";
                        menu.Menuaktif = "true";
                        menu.Menulerid = lastProductIdmenuler;
                        db.Menus.Add(menu);
                        db.SaveChanges();
                        return Redirect("/yonetim/Menus/edit/" + lastProductId);
                    }
                    if (menuler.MenuMi == "true")
                    {
                        ViewBag.kullanicid = new SelectList(db.Kullanicilars, "kullanicid", "kullaniciAdi", menuler.kullanicid);
                        menu.Menutarih = DateTime.Now.ToString();
                        menu.kullanicid = WebSecurity.GetUserId(User.Identity.Name);
                        menu.Yazi = "Yazınızı yazınız";
                        menu.Menuaktif = "true";
                        menu.Menulerid = lastProductIdmenuler;
                        db.Menus.Add(menu);
                        db.SaveChanges();
                        return Redirect("/yonetim/Menus/edit/" + lastProductId);
                    }
                    else
                    {
                        return RedirectToAction("Index");
                    }
                }
            }

            ViewBag.kullanicid = new SelectList(db.Kullanicilars, "kullanicid", "kullaniciAdi", menuler.kullanicid);
            return View(menuler);
        }

        // GET: yonetim/Menulers/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Menuler menuler = db.Menulers.Find(id);
            if (menuler == null)
            {
                return HttpNotFound();
            }
            ViewBag.kullanicid = new SelectList(db.Kullanicilars, "kullanicid", "kullaniciAdi", menuler.kullanicid);
            return View(menuler);
        }

        // POST: yonetim/Menulers/Edit/5
        // Aşırı gönderim saldırılarından korunmak için bağlamak istediğiniz belirli özellikleri etkinleştirin. 
        // Daha fazla bilgi için bkz. https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Menulerid,MenuAdi,MenuUrl,MenuTarih,MenuAktif,KategoriMi,MenuMi,kullanicid")] Menuler menuler)
        {
            if (ModelState.IsValid)
            {
                ViewBag.kullanicid = new SelectList(db.Kullanicilars, "kullanicid", "kullaniciAdi", menuler.kullanicid);
                menuler.kullanicid = WebSecurity.GetUserId(User.Identity.Name);
                menuler.MenuAktif = menuler.MenuAktif.ToString();
                menuler.MenuMi = menuler.MenuMi.ToString();
                menuler.MenuTarih = menuler.MenuTarih.ToString();
                menuler.MenuAdi = menuler.MenuAdi.ToString();
                menuler.KategoriMi = menuler.KategoriMi.ToString();
                menuler.MenuUrl = menuler.MenuAdi.Seo();
                db.Entry(menuler).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.kullanicid = new SelectList(db.Kullanicilars, "kullanicid", "kullaniciAdi", menuler.kullanicid);
            return View(menuler);
        }

        // GET: yonetim/Menulers/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Menuler menuler = db.Menulers.Find(id);
            if (menuler == null)
            {
                return HttpNotFound();
            }
            return View(menuler);
        }

        // POST: yonetim/Menulers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Menuler menuler = db.Menulers.Find(id);
            db.Menulers.Remove(menuler);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
