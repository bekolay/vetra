using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Web.UI.WebControls;
using veri;
using WebMatrix.WebData;


namespace burak.Areas.yonetim.Controllers
{
    [Authorize]
    public class SiteAyarsController : Controller
    {
        private burakEntities db = new burakEntities();

        // GET: yonetim/SiteAyars
        public ActionResult Index()
        {
            var siteAyars = db.SiteAyars.Include(s => s.Kullanicilar);
            return View(siteAyars.ToList());
        }

        // GET: yonetim/SiteAyars/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SiteAyar siteAyar = db.SiteAyars.Find(id);
            if (siteAyar == null)
            {
                return HttpNotFound();
            }
            return View(siteAyar);
        }

        // GET: yonetim/SiteAyars/Create
        public ActionResult Create()
        {
            ViewBag.kullanicid = new SelectList(db.Kullanicilars, "kullanicid", "kullaniciAdi");
            return View();
        }

        // POST: yonetim/SiteAyars/Create
        // Aşırı gönderim saldırılarından korunmak için bağlamak istediğiniz belirli özellikleri etkinleştirin. 
        // Daha fazla bilgi için bkz. https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "SiteAyarid,kullanicid,SiteAdi,SiteLogo,SiteMail,SiteSmtp,SiteMailSifre,SiteTelefon,SiteAdres,SiteİnfoMail")] SiteAyar siteAyar)
        {
            if (ModelState.IsValid)
            {
                if (siteAyar.SiteLogo == null)
                {
                    ViewBag.kullanicid = new SelectList(db.Kullanicilars, "kullanicid", "kullaniciAdi", siteAyar.kullanicid);

                    siteAyar.kullanicid = WebSecurity.GetUserId(User.Identity.Name);
                    db.SiteAyars.Add(siteAyar);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                else
                {
                    string randomParola = "";
                    Random rastgele = new Random();
                    for (int i = 0; i < 6; i++)
                    {
                        randomParola += rastgele.Next(0, 9);
                    }


                    string DosyaAdi = randomParola.Replace(siteAyar.SiteLogo, "");
                    //Kaydetceğimiz resmin uzantısını aldık.
                    string uzanti = System.IO.Path.GetExtension(Request.Files[0].FileName);

                    ViewBag.kullanicid = new SelectList(db.Kullanicilars, "kullanicid", "kullaniciAdi", siteAyar.kullanicid);
                    siteAyar.kullanicid = WebSecurity.GetUserId(User.Identity.Name);
                    siteAyar.SiteLogo = DosyaAdi + uzanti;
                    db.Entry(siteAyar).State = EntityState.Modified;
                    db.SaveChanges();
                    string TamYolYeri = "~/dosya/" + DosyaAdi + uzanti;
                    return RedirectToAction("Index");
                }
            }
            ViewBag.kullanicid = new SelectList(db.Kullanicilars, "kullanicid", "kullaniciAdi", siteAyar.kullanicid);
            return View(siteAyar);
        }

        // GET: yonetim/SiteAyars/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SiteAyar siteAyar = db.SiteAyars.Find(id);
            if (siteAyar == null)
            {
                return HttpNotFound();
            }
            ViewBag.kullanicid = new SelectList(db.Kullanicilars, "kullanicid", "kullaniciAdi", siteAyar.kullanicid);
            return View(siteAyar);
        }

        // POST: yonetim/SiteAyars/Edit/5
        // Aşırı gönderim saldırılarından korunmak için bağlamak istediğiniz belirli özellikleri etkinleştirin. 
        // Daha fazla bilgi için bkz. https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "SiteAyarid,kullanicid,SiteAdi,SiteLogo,SiteMail,SiteSmtp,SiteMailSifre,SiteTelefon,SiteAdres,SiteİnfoMail")] SiteAyar siteAyar)
        {
            if (ModelState.IsValid)
            {
                if (siteAyar.SiteLogo == null)
                {
                    ViewBag.kullanicid = new SelectList(db.Kullanicilars, "kullanicid", "kullaniciAdi", siteAyar.kullanicid);

                    siteAyar.kullanicid = WebSecurity.GetUserId(User.Identity.Name);
                    db.Entry(siteAyar).State = EntityState.Modified;
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                else
                {
                    string randomParola = "";
                    Random rastgele = new Random();
                    for (int i = 0; i < 6; i++)
                    {
                        randomParola += rastgele.Next(0, 9);
                    }


                    string DosyaAdi = randomParola.Replace(siteAyar.SiteLogo, "");
                    //Kaydetceğimiz resmin uzantısını aldık.
                    string uzanti = System.IO.Path.GetExtension(Request.Files[0].FileName);
                    string TamYolYeri = "~/dosya/" + DosyaAdi + uzanti;

                    ViewBag.kullanicid = new SelectList(db.Kullanicilars, "kullanicid", "kullaniciAdi", siteAyar.kullanicid);
                    siteAyar.kullanicid = WebSecurity.GetUserId(User.Identity.Name);
                    siteAyar.SiteLogo = DosyaAdi + uzanti;
                    db.Entry(siteAyar).State = EntityState.Modified;
                    db.SaveChanges();
                    Request.Files[0].SaveAs(Server.MapPath(TamYolYeri));
                    return RedirectToAction("Index");
                }
            }
            ViewBag.kullanicid = new SelectList(db.Kullanicilars, "kullanicid", "kullaniciAdi", siteAyar.kullanicid);
            return View(siteAyar);
        }

        // GET: yonetim/SiteAyars/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SiteAyar siteAyar = db.SiteAyars.Find(id);
            if (siteAyar == null)
            {
                return HttpNotFound();
            }
            return View(siteAyar);
        }

        // POST: yonetim/SiteAyars/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            SiteAyar siteAyar = db.SiteAyars.Find(id);
            db.SiteAyars.Remove(siteAyar);
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
