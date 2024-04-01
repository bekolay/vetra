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
    public class SlidersController : Controller
    {
        private burakEntities db = new burakEntities();

        // GET: yonetim/Sliders
        public ActionResult Index()
        {
            var sliders = db.Sliders.Include(s => s.Kullanicilar);
            return View(sliders.ToList());
        }

        // GET: yonetim/Sliders/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Slider slider = db.Sliders.Find(id);
            if (slider == null)
            {
                return HttpNotFound();
            }
            return View(slider);
        }

        // GET: yonetim/Sliders/Create
        public ActionResult Create()
        {
            ViewBag.kullanicid = new SelectList(db.Kullanicilars, "kullanicid", "kullaniciAdi");
            return View();
        }

        // POST: yonetim/Sliders/Create
        // Aşırı gönderim saldırılarından korunmak için bağlamak istediğiniz belirli özellikleri etkinleştirin. 
        // Daha fazla bilgi için bkz. https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Sliderid,kullanicid,SliderTarih,SliderAdi,AkitfMi,SliderResimAdi")] Slider slider)
        {
            if (ModelState.IsValid)
            {
                if (slider.SliderResimAdi == null)
                {
                    TempData["Message"] = "Resim Seçilmedi";
                    ViewBag.kullanicid = new SelectList(db.Kullanicilars, "kullanicid", "kullaniciAdi", slider.kullanicid);
                    return View();
                }
                if (slider.SliderAdi == null)
                {
                    TempData["Message"] = "Ad Girilmedi";
                    ViewBag.kullanicid = new SelectList(db.Kullanicilars, "kullanicid", "kullaniciAdi", slider.kullanicid);
                    return View();
                }
                string DosyaAdi = slider.SliderAdi.Seo().ToString().Replace(slider.SliderResimAdi, "");
                //Kaydetceğimiz resmin uzantısını aldık.
                string uzanti = System.IO.Path.GetExtension(Request.Files[0].FileName);
                string TamYolYeri = "~/dosya/" + DosyaAdi + uzanti;
                //Eklediğimiz Resni Server.MapPath methodu ile Dosya Adıyla birlikte kaydettik.

                slider.SliderTarih = DateTime.Now;
                slider.kullanicid = WebSecurity.GetUserId(User.Identity.Name);
                slider.SliderResimAdi = DosyaAdi + uzanti;
                db.Sliders.Add(slider);
                db.SaveChanges();
                Request.Files[0].SaveAs(Server.MapPath(TamYolYeri));
                return RedirectToAction("Index");
            }

            ViewBag.kullanicid = new SelectList(db.Kullanicilars, "kullanicid", "kullaniciAdi", slider.kullanicid);
            return View(slider);
        }

        // GET: yonetim/Sliders/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Slider slider = db.Sliders.Find(id);
            if (slider == null)
            {
                return HttpNotFound();
            }
            ViewBag.kullanicid = new SelectList(db.Kullanicilars, "kullanicid", "kullaniciAdi", slider.kullanicid);
            return View(slider);
        }

        // POST: yonetim/Sliders/Edit/5
        // Aşırı gönderim saldırılarından korunmak için bağlamak istediğiniz belirli özellikleri etkinleştirin. 
        // Daha fazla bilgi için bkz. https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Sliderid,kullanicid,SliderTarih,SliderAdi,AkitfMi,SliderResimAdi")] Slider slider)
        {
            if (ModelState.IsValid)
            {
                if (slider.SliderResimAdi == null)
                {
                    TempData["Message"] = "Resim Seçilmedi";
                    ViewBag.kullanicid = new SelectList(db.Kullanicilars, "kullanicid", "kullaniciAdi", slider.kullanicid);
                    return View();
                }
                if (slider.SliderAdi == null)
                {
                    TempData["Message"] = "Ad Girilmedi";
                    ViewBag.kullanicid = new SelectList(db.Kullanicilars, "kullanicid", "kullaniciAdi", slider.kullanicid);
                    return View();
                }
                string DosyaAdi = slider.SliderAdi.Seo().ToString().Replace(slider.SliderResimAdi, "");
                //Kaydetceğimiz resmin uzantısını aldık.
                string uzanti = System.IO.Path.GetExtension(Request.Files[0].FileName);
                string TamYolYeri = "~/dosya/" + DosyaAdi + uzanti;
                //Eklediğimiz Resni Server.MapPath methodu ile Dosya Adıyla birlikte kaydettik.
                slider.kullanicid = WebSecurity.GetUserId(User.Identity.Name);
                slider.SliderResimAdi = DosyaAdi + uzanti;
                db.Entry(slider).State = EntityState.Modified;
                db.SaveChanges();
                Request.Files[0].SaveAs(Server.MapPath(TamYolYeri));
                return RedirectToAction("Index");
            }
            ViewBag.kullanicid = new SelectList(db.Kullanicilars, "kullanicid", "kullaniciAdi", slider.kullanicid);
            return View(slider);
        }

        // GET: yonetim/Sliders/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Slider slider = db.Sliders.Find(id);
            if (slider == null)
            {
                return HttpNotFound();
            }
            return View(slider);
        }

        // POST: yonetim/Sliders/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Slider slider = db.Sliders.Find(id);
            db.Sliders.Remove(slider);
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
