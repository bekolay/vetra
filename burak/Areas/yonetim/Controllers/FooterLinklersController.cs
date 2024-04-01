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
    public class FooterLinklersController : Controller
    {
        private burakEntities db = new burakEntities();

        // GET: yonetim/FooterLinklers
        public ActionResult Index()
        {
            var footerLinklers = db.FooterLinklers.Include(f => f.Kullanicilar);
            return View(footerLinklers.ToList());
        }

        // GET: yonetim/FooterLinklers/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            FooterLinkler footerLinkler = db.FooterLinklers.Find(id);
            if (footerLinkler == null)
            {
                return HttpNotFound();
            }
            return View(footerLinkler);
        }

        // GET: yonetim/FooterLinklers/Create
        public ActionResult Create()
        {
            ViewBag.kullanicid = new SelectList(db.Kullanicilars, "kullanicid", "kullaniciAdi");
            return View();
        }

        // POST: yonetim/FooterLinklers/Create
        // Aşırı gönderim saldırılarından korunmak için bağlamak istediğiniz belirli özellikleri etkinleştirin. 
        // Daha fazla bilgi için bkz. https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Fid,kullanicid,FLinkAdi,FLinkUrl,Ftarih,FAktifMi")] FooterLinkler footerLinkler)
        {
            var footervar = db.FooterLinklers.Where(p => p.FLinkAdi == footerLinkler.FLinkAdi).FirstOrDefault();



            if (footervar != null)
            {
                if (footerLinkler.FLinkAdi == footerLinkler.FLinkAdi)
                {
                    TempData["Message"] = "Bu ad daha önce kayıt edilmişt.";
                    ViewBag.kullanicid = new SelectList(db.Kullanicilars, "kullanicid", "kullaniciAdi", footerLinkler.kullanicid);
                    return View();
                }
            }
            else
            {
                if (ModelState.IsValid)
                {
                    ViewBag.kullanicid = new SelectList(db.Kullanicilars, "kullanicid", "kullaniciAdi", footerLinkler.kullanicid);

                    footerLinkler.kullanicid = WebSecurity.GetUserId(User.Identity.Name);
                    footerLinkler.Ftarih = DateTime.Now.ToString();
                    footerLinkler.FAktifMi = "true";
                    db.FooterLinklers.Add(footerLinkler);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
            }


            ViewBag.kullanicid = new SelectList(db.Kullanicilars, "kullanicid", "kullaniciAdi", footerLinkler.kullanicid);
            return View(footerLinkler);
        }

        // GET: yonetim/FooterLinklers/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            FooterLinkler footerLinkler = db.FooterLinklers.Find(id);
            if (footerLinkler == null)
            {
                return HttpNotFound();
            }
            ViewBag.kullanicid = new SelectList(db.Kullanicilars, "kullanicid", "kullaniciAdi", footerLinkler.kullanicid);
            return View(footerLinkler);
        }

        // POST: yonetim/FooterLinklers/Edit/5
        // Aşırı gönderim saldırılarından korunmak için bağlamak istediğiniz belirli özellikleri etkinleştirin. 
        // Daha fazla bilgi için bkz. https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Fid,kullanicid,FLinkAdi,FLinkUrl,Ftarih,FAktifMi")] FooterLinkler footerLinkler)
        {
            if (ModelState.IsValid)
            {
                footerLinkler.kullanicid = WebSecurity.GetUserId(User.Identity.Name);
                db.Entry(footerLinkler).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.kullanicid = new SelectList(db.Kullanicilars, "kullanicid", "kullaniciAdi", footerLinkler.kullanicid);
            return View(footerLinkler);
        }

        // GET: yonetim/FooterLinklers/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            FooterLinkler footerLinkler = db.FooterLinklers.Find(id);
            if (footerLinkler == null)
            {
                return HttpNotFound();
            }
            return View(footerLinkler);
        }

        // POST: yonetim/FooterLinklers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            FooterLinkler footerLinkler = db.FooterLinklers.Find(id);
            db.FooterLinklers.Remove(footerLinkler);
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
