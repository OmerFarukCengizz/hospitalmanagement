using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using hospitalmanagement.Models.Entities;

namespace hospitalmanagement.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home
        hospitalmanageEntities db = new hospitalmanageEntities();
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Hakkımızda()
        {
            return View();
        }
        [Authorize]
        public ActionResult HBilgi()
        {
            var kullaniciadi = User.Identity.Name;
            var kullanici = db.user.FirstOrDefault(x => x.USERTC == kullaniciadi);
            var model = db.islemler.Where(x => x.USERID == kullanici.USERID).ToList();
            return View(model);
        }
        public ActionResult Goster(int id)
        {
            var model = db.islemler.Where(x => x.ISLEMID == id).ToList();

            return View(model);
        }
    }
}