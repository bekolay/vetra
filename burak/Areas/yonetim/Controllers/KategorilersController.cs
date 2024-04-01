using burak.Models;
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
    public class KategorilersController : Controller
    {
        private burakEntities db = new burakEntities();

        // GET: yonetim/Kategorilers
        public ActionResult Index()
        {
            var kategorilers = db.Kategorilers.Include(k => k.Kullanicilar).Include(k => k.Menuler);
            return View(kategorilers.ToList());
        }

        // GET: yonetim/Kategorilers/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Kategoriler kategoriler = db.Kategorilers.Find(id);
            if (kategoriler == null)
            {
                return HttpNotFound();
            }
            return View(kategoriler);
        }

        // GET: yonetim/Kategorilers/Create
        public ActionResult Create()
        {
            ViewBag.kullanicid = new SelectList(db.Kullanicilars, "kullanicid", "kullaniciAdi");
            ViewBag.Menulerid = new SelectList(db.Menulers, "Menulerid", "MenuAdi");
            ViewBag.kategorilers1 = db.Kategorilers.Include(k => k.Kullanicilar).Include(k => k.Menuler).Where(p => p.Menuler.KategoriMi == "true").ToList();
            ViewBag.Accounts = new SelectList(db.Menulers.Where(p => p.KategoriMi == "true"), "Menulerid", "MenuAdi");
            return View();
        }

        // POST: yonetim/Kategorilers/Create
        // Aşırı gönderim saldırılarından korunmak için bağlamak istediğiniz belirli özellikleri etkinleştirin. 
        // Daha fazla bilgi için bkz. https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [ValidateInput(false)]
        public ActionResult Create([Bind(Include = "Kategorid,kategoriAdi,KategoriYazi,KategoriResim,KategoriAktif,kullanicid,Menulerid,KategorilerUrl,kategorilertarih")] Kategoriler kategoriler)
        {
            var kategorivar = db.Kategorilers.Where(p => p.kategoriAdi == kategoriler.kategoriAdi).FirstOrDefault();
            if (kategorivar != null)
            {
                if (kategorivar.kategoriAdi == kategorivar.kategoriAdi)
                {
                    TempData["Message"] = "Bu Kategori daha önce eklenmiş";
                    ViewBag.kullanicid = new SelectList(db.Kullanicilars, "kullanicid", "kullaniciAdi", kategoriler.kullanicid);
                    ViewBag.Menulerid = new SelectList(db.Menulers, "Menulerid", "MenuAdi", kategoriler.Menulerid);
                    return View();
                }
            }
            {
                if (ModelState.IsValid)
                {
                    if (kategoriler.KategoriResim == null)
                    {
                        kategoriler.kullanicid = WebSecurity.GetUserId(User.Identity.Name);
                        kategoriler.KategorilerUrl = kategoriler.kategoriAdi.Seo();
                        kategoriler.kategorilertarih = DateTime.Now.ToString();
                        kategoriler.KategoriAktif = "true";
                        db.Kategorilers.Add(kategoriler);
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


                        string DosyaAdi = randomParola.Replace(kategoriler.kategoriAdi, "");
                        //Kaydetceğimiz resmin uzantısını aldık.
                        string uzanti = System.IO.Path.GetExtension(Request.Files[0].FileName);
                        string TamYolYeri = "~/dosya/" + DosyaAdi + uzanti;
                        kategoriler.kullanicid = WebSecurity.GetUserId(User.Identity.Name);
                        kategoriler.KategorilerUrl = kategoriler.kategoriAdi.Seo();
                        kategoriler.kategorilertarih = DateTime.Now.ToString();
                        kategoriler.KategoriAktif = "true";
                        kategoriler.KategoriResim = DosyaAdi + uzanti;
                        db.Kategorilers.Add(kategoriler);
                        db.SaveChanges();
                        Request.Files[0].SaveAs(Server.MapPath(TamYolYeri));
                        return RedirectToAction("Index");
                    }
                }



                ViewBag.kullanicid = new SelectList(db.Kullanicilars, "kullanicid", "kullaniciAdi", kategoriler.kullanicid);
                ViewBag.Menulerid = new SelectList(db.Menulers, "Menulerid", "MenuAdi", kategoriler.Menulerid);
                return View(kategoriler);
            }
        }
        // GET: yonetim/Kategorilers/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Kategoriler kategoriler = db.Kategorilers.Find(id);
            if (kategoriler == null)
            {
                return HttpNotFound();
            }
            ViewBag.kullanicid = new SelectList(db.Kullanicilars, "kullanicid", "kullaniciAdi", kategoriler.kullanicid);
            ViewBag.Menulerid = new SelectList(db.Menulers, "Menulerid", "MenuAdi", kategoriler.Menulerid);
            ViewBag.Accounts = new SelectList(db.Menulers.Where(p => p.KategoriMi == "true"), "Menulerid", "MenuAdi");
            return View(kategoriler);
        }

        // POST: yonetim/Kategorilers/Edit/5
        // Aşırı gönderim saldırılarından korunmak için bağlamak istediğiniz belirli özellikleri etkinleştirin. 
        // Daha fazla bilgi için bkz. https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [ValidateInput(false)]
        public ActionResult Edit([Bind(Include = "Kategorid,kategoriAdi,KategoriYazi,KategoriResim,KategoriAktif,kullanicid,Menulerid,KategorilerUrl,kategorilertarih")] Kategoriler kategoriler)
        {
            if (ModelState.IsValid)
            {
                if (kategoriler.KategoriResim == null)
                {
                    ViewBag.kullanicid = new SelectList(db.Kullanicilars, "kullanicid", "kullaniciAdi", kategoriler.kullanicid);
                    ViewBag.Menulerid = new SelectList(db.Menulers, "Menulerid", "MenuAdi", kategoriler.Menulerid);
                    kategoriler.kullanicid = WebSecurity.GetUserId(User.Identity.Name);
                    kategoriler.KategorilerUrl = kategoriler.kategoriAdi.Seo();
                    kategoriler.KategoriAktif = "true";
                    db.Entry(kategoriler).State = EntityState.Modified;
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

                    ViewBag.kullanicid = new SelectList(db.Kullanicilars, "kullanicid", "kullaniciAdi", kategoriler.kullanicid);
                    ViewBag.Menulerid = new SelectList(db.Menulers, "Menulerid", "MenuAdi", kategoriler.Menulerid);
                    string DosyaAdi = randomParola.Replace(kategoriler.kategoriAdi, "");
                    //Kaydetceğimiz resmin uzantısını aldık.
                    string uzanti = System.IO.Path.GetExtension(Request.Files[0].FileName);
                    string TamYolYeri = "~/dosya/" + DosyaAdi + uzanti;
                    kategoriler.kullanicid = WebSecurity.GetUserId(User.Identity.Name);
                    kategoriler.KategoriResim = DosyaAdi + uzanti;
                    kategoriler.KategorilerUrl = kategoriler.kategoriAdi.Seo();
                    kategoriler.KategoriAktif = "true";
                    db.Entry(kategoriler).State = EntityState.Modified;
                    db.SaveChanges();
                    Request.Files[0].SaveAs(Server.MapPath(TamYolYeri));
                    return RedirectToAction("Index");
                }

                
            }
            ViewBag.kullanicid = new SelectList(db.Kullanicilars, "kullanicid", "kullaniciAdi", kategoriler.kullanicid);
            ViewBag.Menulerid = new SelectList(db.Menulers, "Menulerid", "MenuAdi", kategoriler.Menulerid);
            return View(kategoriler);
        }

        // GET: yonetim/Kategorilers/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Kategoriler kategoriler = db.Kategorilers.Find(id);
            if (kategoriler == null)
            {
                return HttpNotFound();
            }
            return View(kategoriler);
        }

        // POST: yonetim/Kategorilers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Kategoriler kategoriler = db.Kategorilers.Find(id);
            db.Kategorilers.Remove(kategoriler);
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
