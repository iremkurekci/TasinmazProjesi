using Microsoft.Ajax.Utilities;
using OfficeOpenXml.Table.PivotTable;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Validation;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Services.Description;
using System.Web.UI;
using System.Web.UI.WebControls;
using Taşınmaz_Projesi.Context;
using Taşınmaz_Projesi.Models;
using Umbraco.Core.Models;
using PagedList;
using System.Data.SqlClient;
using PagedList.Mvc;
namespace Taşınmaz_Projesi.Controllers
{
    public class TasinmazController : Controller
    {
        TasinmazContext db = new TasinmazContext(); 
        //db bağlı olan TasinmazContext sınıfına erişir, db den veri çekmiş olur.
       
        public ActionResult Index(string SearchString, string currentFilter, int? page)
        {

            if(SearchString != null)
            {
                page = 1;
            } else
            {
                SearchString = currentFilter;
            }
            ViewBag.CurrentFilter = SearchString;

            int pageSize = 10;
            int pageNumber = (page ?? 1);

            if (String.IsNullOrEmpty(SearchString) == false)
            {
                var tas = db.tasinmaz.Where(s => s.Il.ToString().Contains(SearchString)
                 || s.Ilce.ToString().Contains(SearchString) || s.Mahalle.ToString().Contains(SearchString)
                 || s.Ada.Contains(SearchString) || s.Parsel.Contains(SearchString) 
                 || s.Nitelik.Contains(SearchString) || s.Adres.Contains(SearchString));
                return View(tas.OrderBy(k => k.Il).ToPagedList(pageNumber, pageSize));
            }
            
            return View(db.tasinmaz.OrderBy(k => k.Il).ToPagedList(pageNumber, pageSize)); 
            // db den çektiği taşınmaz bilgilerini ekrana liste olarak verir.
        }



        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include ="TasinmazID,Il,Ilce,Mahalle,Ada,Parsel,Nitelik,Adres")] Tasinmaz tasinmaz)
        {
            try
            {
                if(ModelState.IsValid == true)
                {
                    db.tasinmaz.Add(tasinmaz);
                    db.SaveChanges();
                }
            }
            catch (DbEntityValidationException e)
            {

                foreach (var eve in e.EntityValidationErrors)
                {
                    Response.Write(string.Format("Entity türü \"{0}\" şu hatalara sahip \"{1}\" Geçerlilik hataları:", eve.Entry.Entity.GetType().Name, eve.Entry.State));
                    foreach (var ve in eve.ValidationErrors)
                    {
                        Response.Write(string.Format("- Özellik: \"{0}\", Hata: \"{1}\"", ve.PropertyName, ve.ErrorMessage));
                    }
                    Response.End();
                }
            }
                return RedirectToAction("Index", "Tasinmaz");

        }
        
        public ActionResult Create()
        {
            List<Il> iller = new List<Il>(db.il.OrderBy(k => k.IlAdi).ToList());
            ViewBag.illerListesi = new SelectList(iller, "IlID", "IlAdi");
            return View();
        }

        public ActionResult GetIlceler(int IlID)
        {
            try
            {
                List<Ilce> ilceler = new List<Ilce>(db.ilce.Where(k => k.IlID == IlID).OrderBy(k => k.IlceAdi).ToList());
                return Json(ilceler);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }


        }

        public ActionResult GetMahalleler(int IlceID)
        {
            try
            {
                List<Mahalle> mahalleler = new List<Mahalle>(db.mahalle.Where(k => k.IlceID == IlceID).OrderBy(k => k.MahalleAdi).ToList());
                return Json(mahalleler);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }


        }
        [HttpGet]
        public ActionResult Edit(int? id)
        {
            Tasinmaz tasinmaz = db.tasinmaz.Find(id);
            List<Il> iller = new List<Il>(db.il.OrderBy(k => k.IlAdi).ToList());
            List<Ilce> ilceler = new List<Ilce>();
            ViewBag.illerListesi = new SelectList(iller, "IlID", "IlAdi");
            ViewBag.ilcelerListesi = new SelectList(ilceler, "IlceID", "IlceAdi");


            //ViewBag.seciliIlce = new SelectList(db.ilce.Where(x => x.IlID == tasinmaz), "IlceID", "IlceAdi");
            //ViewBag.seciliMahalle = new SelectList(db.mahalle, "MahalleID", "MahalleAdi");

            return View(tasinmaz);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "TasinmazID,Il,Ilce,Mahalle,Ada,Parsel,Nitelik,Adres")] Tasinmaz tasinmaz)
        {
            var tas = db.tasinmaz.Find(tasinmaz.TasinmazID);
            tas.Il = tasinmaz.Il;
            tas.Ilce = tasinmaz.Ilce;
            tas.Mahalle = tasinmaz.Mahalle;
            tas.Ada = tasinmaz.Ada;
            tas.Parsel = tasinmaz.Parsel;
            tas.Nitelik = tasinmaz.Nitelik;
            tas.Adres = tasinmaz.Adres;

            if (ModelState.IsValid == true)
            {
                db.SaveChanges();
            }
            return RedirectToAction("Index", "Tasinmaz");
        }

        public ActionResult Delete(int? id)
       {
            Tasinmaz tasinmaz = db.tasinmaz.Find(id);
            return View(tasinmaz);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id)
        {
            Tasinmaz tasinmaz = db.tasinmaz.Find(id);
            db.tasinmaz.Remove(tasinmaz);
            db.SaveChanges();
            return RedirectToAction("Index", "Tasinmaz");
        }

        public ActionResult Details(int? id)
        {
            Tasinmaz tasinmaz = db.tasinmaz.Find(id);
            var tsnmz = db.tasinmaz.Find(tasinmaz.TasinmazID);
            tsnmz.Il = tasinmaz.Il;
            tsnmz.Ilce = tasinmaz.Ilce;
            tsnmz.Mahalle = tasinmaz.Mahalle;
            tsnmz.Ada = tasinmaz.Ada;
            tsnmz.Parsel = tasinmaz.Parsel;
            tsnmz.Nitelik = tasinmaz.Nitelik;
            tsnmz.Adres = tasinmaz.Adres;

            var tas = new System.Data.DataTable("teste");
            tas.Columns.Add("Tasinmaz ID", typeof(int));
            tas.Columns.Add("Il", typeof(string));
            tas.Columns.Add("Ilce", typeof(string));
            tas.Columns.Add("Mahalle", typeof(string));
            tas.Columns.Add("Ada", typeof(string));
            tas.Columns.Add("Parsel", typeof(string));
            tas.Columns.Add("Nitelik", typeof(string));
            tas.Columns.Add("Adres", typeof(string));

            tas.Rows.Add(tsnmz.TasinmazID, tsnmz.Il, tsnmz.Ilce, tsnmz.Mahalle, tsnmz.Ada, tsnmz.Parsel, tsnmz.Nitelik, tsnmz.Adres);

            var grid = new GridView();
            grid.DataSource = tas;
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
    }
}