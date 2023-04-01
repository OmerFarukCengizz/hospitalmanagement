using hospitalmanagement.Models.Entities;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Web;
using System.Web.Mvc;

namespace hospitalmanagement.Controllers
{
    [Authorize(Roles = "doktor")]
    public class IslemlerController : Controller
    {
        // GET: Islemler
        hospitalmanageEntities db = new hospitalmanageEntities();
        [HttpGet]
        public ActionResult Add()
        {
            List<SelectListItem> hasta = (from x in db.user.Where(m => m.USERROL == "uye").ToList()
                                          select new SelectListItem
                                          {
                                              Text = x.USERAD + " " + x.USERSOYAD,
                                              Value = x.USERID.ToString(),
                                          }).ToList();
            ViewBag.hst = hasta;
            return View();
        }
        [HttpPost]
        public ActionResult Add(islemler p1, string Tahlil, string Röntgen, string Ultrason)
        {
            var fr = db.user.Where(m => m.USERID == p1.user.USERID).FirstOrDefault();
            p1.user = fr;
            if (Tahlil == "true")
            {
                p1.ISTAHLIL = true;
            }
            else
            {
                p1.ISTAHLIL = false;
            }
            if (Röntgen == "true")
            {
                p1.ISRONTGEN = true;
            }
            else
            {
                p1.ISRONTGEN = false;
            }
            if (Ultrason == "true")
            {
                p1.ISULTRASON = true;
            }
            else
            {
                p1.ISULTRASON = false;
            }
            p1.ISTENILATARIH = DateTime.Now;
            db.islemler.Add(p1);
            db.SaveChanges();
            return RedirectToAction("Index", "Home");

        }
        public ActionResult Sonuc()
        {
            List<islemler> degerler = db.islemler.ToList();
            return View(degerler);
        }
        [HttpGet]
        public ActionResult Guncelle(int id)
        {
            var sd = db.islemler.Find(id);
            return View("Guncelle", sd);
        }
        [HttpPost]
        public ActionResult Guncel(islemler p1)
        {

            var u = db.islemler.Find(p1.ISLEMID);
            u.USERID = p1.USERID;
            u.ISTAHLIL = p1.ISTAHLIL;
            u.ISRONTGEN = p1.ISRONTGEN;
            u.ISULTRASON = p1.ISULTRASON;
            u.TACIKLAMA = p1.TACIKLAMA;
            u.RACIKLAMA = p1.RACIKLAMA;
            u.UACIKLAMA = p1.UACIKLAMA;
            
            u.EKLENENTARIH = DateTime.Now;
            if (Request.Files.Count > 0)
            {
                int i = 0;
                if (p1.ISTAHLIL == true )
                {
                    string dosyaadi = Path.GetFileName(Request.Files[i].FileName);
                    string uzanti = Path.GetExtension(Request.Files[i].FileName);
                    string yol = "/Image/" + dosyaadi + uzanti;
                    Request.Files[i].SaveAs(Server.MapPath(yol));
                    p1.TAHLILIMG = "/Image/" + dosyaadi + uzanti ;
                    i++;
                }
                if (p1.ISRONTGEN == true )
                {
                    string dosya = Path.GetFileName(Request.Files[i].FileName);
                    string uzanti = Path.GetExtension(Request.Files[i].FileName);
                    string yol = "/Image/" + dosya + uzanti;
                    Request.Files[i].SaveAs(Server.MapPath(yol));
                    p1.RONTGENIMG = "/Image/" + dosya + uzanti;
                    i++;
                }
                if (p1.ISULTRASON == true)
                {
                    string dos = Path.GetFileName(Request.Files[i].FileName);
                    string uz = Path.GetExtension(Request.Files[i].FileName);
                    string yol = "/Image/" + dos + uz;
                    Request.Files[i].SaveAs(Server.MapPath(yol)); ;                 
                    p1.ULTRASONIMG = "/Image/" + dos + uz;
                }
               
            }

            u.TAHLILIMG = p1.TAHLILIMG;
            u.RONTGENIMG = p1.RONTGENIMG;
            u.ULTRASONIMG = p1.ULTRASONIMG;
            db.SaveChanges();
            return RedirectToAction("Sonuc");
        }
        public ActionResult HastaBilgi()
        {
            var hasta = db.user.Where(m => m.USERROL == "uye").ToList();
            return View(hasta);
        }
        public ActionResult HastaIslemBilgi(int id)
        {
            var blg = db.islemler.Where(x => x.user.USERID == id).ToList();
            return View(blg);
        }
        public ActionResult Sonucgoster(int id)
        {
            var model = db.islemler.Where(x => x.ISLEMID == id).ToList();

            return View(model);
        }
    }
}