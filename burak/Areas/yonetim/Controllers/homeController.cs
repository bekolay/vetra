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
    public class homeController : Controller
    {
        private burakEntities db = new burakEntities();

        // GET: yonetim/AnaSayfas
        public ActionResult Index()
        {
            var anaSayfas = db.AnaSayfas.Include(a => a.Kullanicilar);
            return View(anaSayfas.ToList());
        }
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AnaSayfa anaSayfa = db.AnaSayfas.Find(id);
            if (anaSayfa == null)
            {
                return HttpNotFound();
            }
            ViewBag.kullanicid = new SelectList(db.Kullanicilars, "kullanicid", "kullaniciAdi", anaSayfa.kullanicid);
            return View(anaSayfa);
        }

        // POST: yonetim/AnaSayfas/Edit/5
        // Aşırı gönderim saldırılarından korunmak için bağlamak istediğiniz belirli özellikleri etkinleştirin. 
        // Daha fazla bilgi için bkz. https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Anasayfaid,kullanicid,AnasayfaAdi,AnasayfaİsimAciklama,AnasayfaElektrik,AnasayfaElektrikAciklama,AHakkindaİsim,AHakkindaAciklama,ADunyaİsim,ADunyaAciklama,Atarih")] AnaSayfa anaSayfa)
        {
            if (ModelState.IsValid)
            {
                anaSayfa.kullanicid = WebSecurity.GetUserId(User.Identity.Name);
                anaSayfa.Atarih = DateTime.Now.ToString();
                db.Entry(anaSayfa).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.kullanicid = new SelectList(db.Kullanicilars, "kullanicid", "kullaniciAdi", anaSayfa.kullanicid);
            return View(anaSayfa);
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