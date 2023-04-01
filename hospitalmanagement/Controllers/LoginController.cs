using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using hospitalmanagement.Models.Entities;
using System.EnterpriseServices.CompensatingResourceManager;
using System.Threading.Tasks;
using System.Web.Security;

namespace hospitalmanagement.Controllers
{
    [AllowAnonymous]
    public class LoginController : Controller
    {
        // GET: Login
        hospitalmanageEntities db=new hospitalmanageEntities();
        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Login(user k)
        {
            var kullanici = db.user.FirstOrDefault(x => x.USERTC == k.USERTC && x.USERSIFRE == k.USERSIFRE);
            if (kullanici != null)
            {
                Session["USERTCSS"] = k.USERTC.ToString();
                TempData["UserName"] = kullanici.USERAD;
                FormsAuthentication.SetAuthCookie(k.USERTC, false);
                return RedirectToAction("Index","Home");
            }
            ViewBag.hata = "Kullanıcı adı veya şifre hatalı";
            return View();
        }
        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            Session.Abandon();
            return RedirectToAction("Index", "Home");

        }
        public ActionResult Register()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Register(user U)
        {
            if (ModelState.IsValid)
            {
                using (hospitalmanageEntities dc = new hospitalmanageEntities())
                {
                    if (dc.user.Any(x => x.USERTC == U.USERTC))
                    {
                        ViewBag.DuplicateMessage = "Kullanıcı zaten mevcut";
                        return View(U);
                    }
                    dc.user.Add(U);
                    dc.SaveChanges();
                    ModelState.Clear();
                    U = null;
                    ViewBag.Message = "Kaydınız başarıyla tamamlanmıştır";
                }
            }
            return View(U);
        }
    }
}