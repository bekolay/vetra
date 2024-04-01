using burak.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using veri;
using WebMatrix.WebData;

namespace burak.Areas.yonetim.Controllers
{
    [Authorize]
    public class KullanicilarsController : Controller
    {
        private burakEntities db = new burakEntities();

        // GET: yonetim/Kullanicilars

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
                var kullanicilars = db.Kullanicilars.Include(k => k.AdminRol);

                return View(kullanicilars.ToList());
            }
            else
            {

                return RedirectToAction("Bloglar");
            }


        }

        // GET: yonetim/Kullanicilars/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Kullanicilar kullanicilar = db.Kullanicilars.Find(id);
            if (kullanicilar == null)
            {
                return HttpNotFound();
            }
            return View(kullanicilar);
        }

        // GET: yonetim/Kullanicilars/Create

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
                ViewBag.KullaniciRolid = new SelectList(db.AdminRols, "KullaniciRolid", "RolAdi");
                return View();
            }
            else
            {

                return RedirectToAction("Bloglar");
            }
        }

        // POST: yonetim/Kullanicilars/Create
        // Aşırı gönderim saldırılarından korunmak için bağlamak istediğiniz belirli özellikleri etkinleştirin. 
        // Daha fazla bilgi için bkz. https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "kullanicid,kullaniciAdi,KullaniciAdiSoyadi,KullaniciAdminRol,KullaniciSifre,KullaniciSifreTekrar,KullaniciTarih,KullaniciAktifmi,KullaniciRolid,KullaniciEmail,KullaniciGuvenlik,KullaniciGuvenlikCevap")] Kullanicilar kullanicilar)
        {
            var kullanicivar = db.Kullanicilars.Where(p => p.kullaniciAdi == kullanicilar.kullaniciAdi).FirstOrDefault();



            if (kullanicivar != null)
            {
                if (kullanicilar.kullaniciAdi == kullanicilar.kullaniciAdi)
                {
                    TempData["Message"] = "Bu kullanıcı daha önce eklenmiş";
                    ViewBag.KullaniciRolid = new SelectList(db.AdminRols, "KullaniciRolid", "RolAdi", kullanicilar.KullaniciRolid);
                    return View();
                }
            }
            else
            {
                if (kullanicivar == null)
                {
                    var kullanic1s = db.Kullanicilars.Where(p => p.KullaniciEmail == kullanicilar.KullaniciEmail).FirstOrDefault();
                    if (kullanic1s == null)
                    {

                        if (kullanicilar.KullaniciSifre != kullanicilar.KullaniciSifreTekrar)
                        {
                            TempData["Message"] = "Şifreler aynı değil";
                            ViewBag.KullaniciRolid = new SelectList(db.AdminRols, "KullaniciRolid", "RolAdi", kullanicilar.KullaniciRolid);
                            return View();
                        }

                        if (ModelState.IsValid)
                        {
                            kullanicilar.KullaniciSifre = kullanicilar.KullaniciSifre.Md5();
                            kullanicilar.KullaniciSifreTekrar = kullanicilar.KullaniciSifreTekrar.Md5();
                            kullanicilar.KullaniciTarih = DateTime.Now;
                            kullanicilar.KullaniciAktifmi = true;
                            kullanicilar.KullaniciRolid = 2;
                            db.Kullanicilars.Add(kullanicilar);
                            db.SaveChanges();
                            TempData["Message"] = "Kayıt olduğunuz için teşekkür ederiz şimdi giriş yapabilirsiniz.";
                            ViewBag.KullaniciRolid = new SelectList(db.AdminRols, "KullaniciRolid", "RolAdi", kullanicilar.KullaniciRolid);
                            return View();
                        }
                    }
                }
                else
                {
                    TempData["Message"] = "Bu mail adresi daha önce kullanışmıştır.";
                    ViewBag.KullaniciRolid = new SelectList(db.AdminRols, "KullaniciRolid", "RolAdi", kullanicilar.KullaniciRolid);
                    return View();
                }

            }
            TempData["Message"] = "Bu mail adresi daha önce kullanışmıştır.";
            ViewBag.KullaniciRolid = new SelectList(db.AdminRols, "KullaniciRolid", "RolAdi", kullanicilar.KullaniciRolid);
            return View();
        }


        // GET: yonetim/Kullanicilars/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Kullanicilar kullanicilar = db.Kullanicilars.Find(id);
            if (kullanicilar == null)
            {
                return HttpNotFound();
            }
            ViewBag.KullaniciRolid = new SelectList(db.AdminRols, "KullaniciRolid", "RolAdi", kullanicilar.KullaniciRolid);
            return View(kullanicilar);
        }

        // POST: yonetim/Kullanicilars/Edit/5
        // Aşırı gönderim saldırılarından korunmak için bağlamak istediğiniz belirli özellikleri etkinleştirin. 
        // Daha fazla bilgi için bkz. https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "kullanicid,kullaniciAdi,KullaniciAdiSoyadi,KullaniciSifre,KullaniciAdminRol,KullaniciSifreTekrar,KullaniciTarih,KullaniciAktifmi,KullaniciRolid,KullaniciEmail,KullaniciGuvenlik,KullaniciGuvenlikCevap")] Kullanicilar kullanicilar)
        {
            if (ModelState.IsValid)
            {
                if (kullanicilar.KullaniciSifre != kullanicilar.KullaniciSifreTekrar)
                {
                    TempData["Message"] = "Şifreler aynı değil";
                    ViewBag.KullaniciRolid = new SelectList(db.AdminRols, "KullaniciRolid", "RolAdi", kullanicilar.KullaniciRolid);
                    return View();
                }

                kullanicilar.KullaniciSifre = kullanicilar.KullaniciSifre.Md5();
                kullanicilar.KullaniciSifreTekrar = kullanicilar.KullaniciSifreTekrar.Md5();
                kullanicilar.KullaniciRolid = 2;
                db.Entry(kullanicilar).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.KullaniciRolid = new SelectList(db.AdminRols, "KullaniciRolid", "RolAdi", kullanicilar.KullaniciRolid);
            return View(kullanicilar);
        }

        // GET: yonetim/Kullanicilars/Delete/5
        //public ActionResult Delete(int? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    Kullanicilar kullanicilar = db.Kullanicilars.Find(id);
        //    if (kullanicilar == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return View(kullanicilar);
        //}

        //// POST: yonetim/Kullanicilars/Delete/5
        //[HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        //public ActionResult DeleteConfirmed(int id)
        //{
        //    Kullanicilar kullanicilar = db.Kullanicilars.Find(id);
        //    db.Kullanicilars.Remove(kullanicilar);
        //    db.SaveChanges();
        //    return RedirectToAction("Index");
        //}

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
