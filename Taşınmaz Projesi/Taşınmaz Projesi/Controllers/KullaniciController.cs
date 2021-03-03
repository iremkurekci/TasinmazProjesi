using NPoco;
using PagedList;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Reflection.Metadata;
using System.Web;
using System.Web.Mvc;
using System.Web.UI;
using System.Web.UI.WebControls;
using Taşınmaz_Projesi.Context;
using Taşınmaz_Projesi.Models;
using Umbraco.Forms.Core.Providers.FieldTypes;

namespace Taşınmaz_Projesi.Controllers
{
    public class KullaniciController : Controller
    {
        // GET: Kullanici

        TasinmazContext db = new TasinmazContext();
        public ActionResult Index(string SearchString, string currentFilter, int? page)
        {
            if (SearchString != null)
            {
                page = 1;
            }
            else
            {
                SearchString = currentFilter;
            }
            int pageSize = 10;
            int pageNumber = (page ?? 1);

            ViewBag.CurrentFilter = SearchString;
            if (String.IsNullOrEmpty(SearchString) == false)
            {
               var kull = db.kullanici.Where(s => s.KullaniciAdi.Contains(SearchString)
                || s.KullaniciSoyadi.Contains(SearchString) || s.Email.Contains(SearchString)
                || s.Rol.Contains(SearchString) || s.Adres.Contains(SearchString));
                return View(kull.OrderBy(k => k.KullaniciAdi).ToPagedList(pageNumber, pageSize));
            }
             
            return View(db.kullanici.OrderBy(k => k.KullaniciAdi).ToPagedList(pageNumber, pageSize));
        }

        public ActionResult Create() //bind işlemi içerikle veriyi bağlar
        {
            return View();
        }

        [HttpPost] //eğer sayfada bir işlem gerçekleşiyorsa demek
        public ActionResult Create([Bind(Include ="KullaniciID,KullaniciAdi,KullaniciSoyadi,Email,Parola,Rol,Adres")] Kullanici kullanici) //bind işlemi içerikle veriyi bağlar
        {
            if (ModelState.IsValid == true) //eğer girilen bilgilerde eksik, hata vs yoksa geçerli kabul eder.
            {
                db.kullanici.Add(kullanici);
                db.SaveChanges();
            }
            else
            {
                TempData["Message"] = "Eksik veya hatalı bilgi girdiniz.";
            }

            return RedirectToAction("Index","Kullanici");
        }

        public ActionResult Delete(int? id)
        {
            Kullanici kullanicilar = db.kullanici.Find(id);
            return View(kullanicilar);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(Kullanici kullanici)
        { 
            var kull = db.kullanici.Find(kullanici.KullaniciID);
            kull.KullaniciAdi = kullanici.KullaniciAdi;
            kull.KullaniciSoyadi = kullanici.KullaniciSoyadi;
            kull.Email = kullanici.Email;
            kull.Parola = kullanici.Parola;
            kull.Rol = kullanici.Rol;
            kull.Adres = kullanici.Adres;
            db.kullanici.Remove(kull);
            db.SaveChanges();
            return RedirectToAction("Index", "Kullanici");
        }
        
        public ActionResult Details(int? id)
        {
            Kullanici kullanici = db.kullanici.Find(id);
            var kllnc = db.kullanici.Find(kullanici.KullaniciID);
            kllnc.KullaniciAdi = kullanici.KullaniciAdi;
            kllnc.KullaniciSoyadi = kullanici.KullaniciSoyadi;
            kllnc.Email = kullanici.Email;
            kllnc.Rol = kullanici.Rol;
            kllnc.Adres = kullanici.Adres;

            var kull = new System.Data.DataTable("teste");
            kull.Columns.Add("Kullanici ID", typeof(int));
            kull.Columns.Add("Ad", typeof(string));
            kull.Columns.Add("Soyad", typeof(string));
            kull.Columns.Add("E-mail", typeof(string));
            kull.Columns.Add("Rol", typeof(string));
            kull.Columns.Add("Adres", typeof(string));

            kull.Rows.Add(kllnc.KullaniciID,kllnc.KullaniciAdi,kllnc.KullaniciSoyadi,kllnc.Email,kllnc.Rol,kllnc.Adres);

            var grid = new GridView();
            grid.DataSource = kull;
            grid.DataBind();

            Response.ClearContent();
            Response.Buffer = true;
            Response.AddHeader("content-disposition", "attachment; filename=MyExcelFile.xls");
            Response.ContentType = "application/ms-excel";

            Response.Charset = "utf-8";
            StringWriter sw = new StringWriter();
            HtmlTextWriter htw = new HtmlTextWriter(sw);

            grid.RenderControl(htw);

            Response.Output.Write(sw.ToString());
            Response.Flush();
            Response.End();

            return View();
        }


        public ActionResult Edit(int? id)
        {
            Kullanici kullanicilar = db.kullanici.Find(id);
            return View(kullanicilar);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Kullanici kullanici)
        {
            var kull = db.kullanici.Find(kullanici.KullaniciID);
            kull.KullaniciAdi = kullanici.KullaniciAdi;
            kull.KullaniciSoyadi = kullanici.KullaniciSoyadi;
            kull.Email = kullanici.Email;
            kull.Parola = kullanici.Parola;
            kull.Rol = kullanici.Rol;
            kull.Adres = kullanici.Adres;

            if(ModelState.IsValid == true)
            {
                db.SaveChanges();
            }
            else
            {
                TempData["Message"] = "Eksik veya hatalı bilgi girdiniz.";
            }
            return RedirectToAction("Index","Kullanici");
        }
    }
}