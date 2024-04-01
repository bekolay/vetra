using burak.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Diagnostics.Eventing.Reader;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using veri;
using WebMatrix.WebData;

namespace burak.Areas.yonetim.Controllers
{
    [Authorize]
    public class MenusController : Controller
    {
        private burakEntities db = new burakEntities();

        // GET: yonetim/Menus
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
                var menus = db.Menus.Include(m => m.Menuler).Include(m => m.Kullanicilar).Where(p => p.Menuler.MenuMi == "true" & p.Menuler.MenuAktif == "true");
                return View(menus.ToList());
            }
            else
            {

                return RedirectToAction("Bloglar");
            }
        }

        // GET: yonetim/Menus/Create
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
                ViewBag.Menulerid = new SelectList(db.Menulers, "Menulerid", "MenuAdi");
                ViewBag.kullanicid = new SelectList(db.Kullanicilars, "kullanicid", "kullaniciAdi");
                return View();
            }
            else
            {

                return RedirectToAction("Bloglar");
            }
        }

        // POST: yonetim/Menus/Create
        // Aşırı gönderim saldırılarından korunmak için bağlamak istediğiniz belirli özellikleri etkinleştirin. 
        // Daha fazla bilgi için bkz. https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Menuid,Menulerid,Menutarih,Resim,Yazi,Menuaktif,kullanicid")] Menu menu)
        {
            if (ModelState.IsValid)
            {
                db.Menus.Add(menu);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.Menulerid = new SelectList(db.Menulers, "Menulerid", "MenuAdi", menu.Menulerid);
            ViewBag.kullanicid = new SelectList(db.Kullanicilars, "kullanicid", "kullaniciAdi", menu.kullanicid);
            return View(menu);
        }

        [HttpPost]
        public ActionResult UploadFiles()
        {
            // Checking no of files injected in Request object  
            if (Request.Files.Count > 0)
            {
                try
                {
                    //  Get all files from Request object  
                    string randomParola = "";
                    Random rastgele = new Random();
                    for (int a = 0; a < 6; a++)
                    {
                        randomParola += rastgele.Next(0, 9);
                    }

                    string DosyaAdi = randomParola;
                    //Kaydetceğimiz resmin uzantısını aldık.
                    string uzanti = System.IO.Path.GetExtension(Request.Files[0].FileName);
                    string TamYolYeri = "~/dosya/" + DosyaAdi + uzanti;
                    Request.Files[0].SaveAs(Server.MapPath(TamYolYeri));
                    TempData["yuklendi"] = "Resim Yüklendi";
                    // Returns message that successfully uploaded  
                    return Json("Resim Yüklendi");
                }
                catch (Exception ex)
                {
                    return Json("Error occurred. Error details: " + ex.Message);
                }
            }
            else
            {
                return Json("No files selected.");
            }
        }

        public ActionResult Resimsayfa()
        {
            var appData = Server.MapPath("~/dosya/");
            var images = Directory.GetFiles(appData).Select(x => new PageModel
            {
                Url = Url.Content("/dosya/" + Path.GetFileName(x))
            });
            return View(images);
        }

        [HttpPost]
        public ActionResult yukle()
        {

            string randomParola = "";
            Random rastgele = new Random();
            for (int i = 0; i < 6; i++)
            {
                randomParola += rastgele.Next(0, 9);
            }

            string DosyaAdi = randomParola;
            //Kaydetceğimiz resmin uzantısını aldık.
            string uzanti = System.IO.Path.GetExtension(Request.Files[0].FileName);
            string TamYolYeri = "~/dosya/" + DosyaAdi + uzanti;
            Request.Files[0].SaveAs(Server.MapPath(TamYolYeri));
            var url = $"{"/images//"}";
            var successMessage = "image is uploaded successfully";
            dynamic success = JsonConvert.DeserializeObject("{ 'uploaded': 1,'fileName': \"" + "\",'url': \"" + url + "\", 'error': { 'message': \"" + successMessage + "\"}}");
            return Json("Öğreci başarıyla POST edilmiştir.", JsonRequestBehavior.AllowGet);
        }

        // GET: yonetim/Menus/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Menu menu = db.Menus.Find(id);
            if (menu == null)
            {
                return HttpNotFound();
            }
            ViewBag.Menulerid = new SelectList(db.Menulers, "Menulerid", "MenuAdi", menu.Menulerid);
            ViewBag.kullanicid = new SelectList(db.Kullanicilars, "kullanicid", "kullaniciAdi", menu.kullanicid);
            return View(menu);
        }

        // POST: yonetim/Menus/Edit/5
        // Aşırı gönderim saldırılarından korunmak için bağlamak istediğiniz belirli özellikleri etkinleştirin. 
        // Daha fazla bilgi için bkz. https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [ValidateInput(false)]
        public ActionResult Edit([Bind(Include = "Menuid,Menulerid,Resim,Yazi,Menutarih,Menuaktif,kullanicid")] Menu menu)
        {
            if (ModelState.IsValid)
            {
                if (menu.Resim == null)
                {
                    menu.kullanicid = WebSecurity.GetUserId(User.Identity.Name);
                    db.Entry(menu).State = EntityState.Modified;
                    db.SaveChanges();
                    return RedirectToAction("Index");
                    ViewBag.Menulerid = new SelectList(db.Menulers, "Menulerid", "MenuAdi", menu.Menulerid);
                    ViewBag.kullanicid = new SelectList(db.Kullanicilars, "kullanicid", "kullaniciAdi", menu.kullanicid);
                }
                else
                {

                    string randomParola = "";
                    Random rastgele = new Random();
                    for (int i = 0; i < 6; i++)
                    {
                        randomParola += rastgele.Next(0, 9);
                    }

                    string DosyaAdi = randomParola.Replace(menu.Resim, "");
                    //Kaydetceğimiz resmin uzantısını aldık.
                    string uzanti = System.IO.Path.GetExtension(Request.Files[0].FileName);
                    string TamYolYeri = "~/dosya/" + DosyaAdi + uzanti;
                    //Eklediğimiz Resni Server.MapPath methodu ile Dosya Adıyla birlikte kaydettik.
                    ViewBag.Menulerid = new SelectList(db.Menulers, "Menulerid", "MenuAdi", menu.Menulerid);
                    ViewBag.kullanicid = new SelectList(db.Kullanicilars, "kullanicid", "kullaniciAdi", menu.kullanicid);
                    menu.Resim = DosyaAdi + uzanti;
                    menu.kullanicid = WebSecurity.GetUserId(User.Identity.Name);
                    db.Entry(menu).State = EntityState.Modified;
                    db.SaveChanges();
                    Request.Files[0].SaveAs(Server.MapPath(TamYolYeri));
                    return RedirectToAction("Index");
                }

            }
            ViewBag.Menulerid = new SelectList(db.Menulers, "Menulerid", "MenuAdi", menu.Menulerid);
            ViewBag.kullanicid = new SelectList(db.Kullanicilars, "kullanicid", "kullaniciAdi", menu.kullanicid);
            return View(menu);
        }

        // GET: yonetim/Menus/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Menu menu = db.Menus.Find(id);
            if (menu == null)
            {
                return HttpNotFound();
            }
            return View(menu);
        }

        // POST: yonetim/Menus/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Menu menu = db.Menus.Find(id);
            db.Menus.Remove(menu);
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
