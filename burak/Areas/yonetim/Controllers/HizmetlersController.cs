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
    public class HizmetlersController : Controller
    {
        private burakEntities db = new burakEntities();

        // GET: yonetim/Hizmetlers
        public ActionResult Index()
        {
            var hizmetlers = db.Hizmetlers.Include(h => h.Kullanicilar);
            return View(hizmetlers.ToList());
        }

        // GET: yonetim/Hizmetlers/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Hizmetler hizmetler = db.Hizmetlers.Find(id);
            if (hizmetler == null)
            {
                return HttpNotFound();
            }
            return View(hizmetler);
        }

        // GET: yonetim/Hizmetlers/Create
        public ActionResult Create()
        {
            ViewBag.kullanicid = new SelectList(db.Kullanicilars, "kullanicid", "kullaniciAdi");
            return View();
        }

        // POST: yonetim/Hizmetlers/Create
        // Aşırı gönderim saldırılarından korunmak için bağlamak istediğiniz belirli özellikleri etkinleştirin. 
        // Daha fazla bilgi için bkz. https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [ValidateInput(false)]
        public ActionResult Create([Bind(Include = "Hizmetlerid,kullanicid,HizmetlerAdi,HizmetlerUrl,HizmetlerYazi,HizmetlerTarih,HizmetlerAktif,HizmetlerResim,HizmetlerAnahtar,HizmetKisaYazi,HizmetlerClass")] Hizmetler hizmetler)
        {
            if (ModelState.IsValid)
            {
                string DosyaAdi = hizmetler.HizmetlerAdi.Seo().ToString().Replace(hizmetler.HizmetlerResim, "");
                //Kaydetceğimiz resmin uzantısını aldık.
                string uzanti = System.IO.Path.GetExtension(Request.Files[0].FileName);
                string TamYolYeri = "~/dosya/" + DosyaAdi + uzanti;
                //Eklediğimiz Resni Server.MapPath methodu ile Dosya Adıyla birlikte kaydettik.

                hizmetler.HizmetlerUrl = hizmetler.HizmetlerAdi.Seo();
                hizmetler.HizmetlerResim = DosyaAdi + uzanti;
                hizmetler.kullanicid = WebSecurity.GetUserId(User.Identity.Name);
                hizmetler.HizmetlerTarih = DateTime.Now.ToString();
                hizmetler.HizmetlerAktif = "true";
                db.Hizmetlers.Add(hizmetler);
                db.SaveChanges();
                Request.Files[0].SaveAs(Server.MapPath(TamYolYeri));
                return RedirectToAction("Index");
            }

            ViewBag.kullanicid = new SelectList(db.Kullanicilars, "kullanicid", "kullaniciAdi", hizmetler.kullanicid);
            return View(hizmetler);
        }

        // GET: yonetim/Hizmetlers/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Hizmetler hizmetler = db.Hizmetlers.Find(id);
            if (hizmetler == null)
            {
                return HttpNotFound();
            }
            ViewBag.kullanicid = new SelectList(db.Kullanicilars, "kullanicid", "kullaniciAdi", hizmetler.kullanicid);
            return View(hizmetler);
        }

        // POST: yonetim/Hizmetlers/Edit/5
        // Aşırı gönderim saldırılarından korunmak için bağlamak istediğiniz belirli özellikleri etkinleştirin. 
        // Daha fazla bilgi için bkz. https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [ValidateInput(false)]
        public ActionResult Edit([Bind(Include = "Hizmetlerid,kullanicid,HizmetlerAdi,HizmetlerUrl,HizmetlerYazi,HizmetlerTarih,HizmetlerAktif,HizmetlerResim,HizmetlerAnahtar,HizmetKisaYazi,HizmetlerClass")] Hizmetler hizmetler)
        {
            if (ModelState.IsValid)
            {
                string DosyaAdi = hizmetler.HizmetlerAdi.Seo().ToString().Replace(hizmetler.HizmetlerResim, "");
                //Kaydetceğimiz resmin uzantısını aldık.
                string uzanti = System.IO.Path.GetExtension(Request.Files[0].FileName);
                string TamYolYeri = "~/dosya/" + DosyaAdi + uzanti;
                //Eklediğimiz Resni Server.MapPath methodu ile Dosya Adıyla birlikte kaydettik.

                hizmetler.HizmetlerUrl = hizmetler.HizmetlerAdi.Seo();
                hizmetler.HizmetlerResim = DosyaAdi + uzanti;
                hizmetler.kullanicid = WebSecurity.GetUserId(User.Identity.Name);
                hizmetler.HizmetlerTarih = DateTime.Now.ToString();
                hizmetler.HizmetlerAktif = "true";
                db.Entry(hizmetler).State = EntityState.Modified;
                db.SaveChanges();
                Request.Files[0].SaveAs(Server.MapPath(TamYolYeri));
                return RedirectToAction("Index");
            }
            ViewBag.kullanicid = new SelectList(db.Kullanicilars, "kullanicid", "kullaniciAdi", hizmetler.kullanicid);
            return View(hizmetler);
        }

        // GET: yonetim/Hizmetlers/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Hizmetler hizmetler = db.Hizmetlers.Find(id);
            if (hizmetler == null)
            {
                return HttpNotFound();
            }
            return View(hizmetler);
        }

        // POST: yonetim/Hizmetlers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Hizmetler hizmetler = db.Hizmetlers.Find(id);
            db.Hizmetlers.Remove(hizmetler);
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
