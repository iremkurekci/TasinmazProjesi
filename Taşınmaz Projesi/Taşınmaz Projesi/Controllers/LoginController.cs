using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using Taşınmaz_Projesi.Context;
using Taşınmaz_Projesi.Models;

namespace Taşınmaz_Projesi.Controllers
{
    public class LoginController : Controller
    {
        // GET: Login
        private TasinmazContext db = new TasinmazContext();
        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }
        

        
        public ActionResult Login(string email, string parola)
        {
            if (email != null && parola != null)
            {
                Kullanici kullanici = db.kullanici.Where(x => x.Email == email).FirstOrDefault();
                //FirstOrDefault(): koleksiyonda veri varsa onun ilk değerini, veri yoksa default değeri döner

                if (kullanici.Parola == parola)
                {
                    Session["Kullanici"] = kullanici.Rol;
                }
                else
                {
                    TempData["Message"] = "Eksik veya hatalı bilgi girdiniz.";
                }
            }
            else
            {
                TempData["Message"] = "Eksik veya hatalı bilgi girdiniz.";
            }
            //var kullaniciModel = db.login.Where(k => k.Email == model.Email && k.Parola == model.Parola).FirstOrDefault();
            
            return RedirectToAction("Index", "Login");
        }

        [HttpGet]
        public ActionResult Cıkıs()
        {
            Session.Remove("Kullanici");
            return RedirectToAction("Index", "Login");
        }
    }
}