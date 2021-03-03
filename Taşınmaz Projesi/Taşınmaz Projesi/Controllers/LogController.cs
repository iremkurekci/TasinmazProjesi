using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Taşınmaz_Projesi.Context;
using Taşınmaz_Projesi.Models;

namespace Taşınmaz_Projesi.Controllers
{
    [Log] //log'a ait tüm attributeleri kullanabilir,işlemleri yapabilir
    public class LogController : Controller
    {
        // GET: Log

        private TasinmazContext db = new TasinmazContext();
        public ActionResult Index(Log log, string SearchString, string currentFilter, int? page)
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
                //var logs = db.log.Where(s => s.Id.ToString().Contains(SearchString) || s.KullaniciID.ToString().Contains(SearchString)
                // || s.Durum.Contains(SearchString) || s.IslemTipi.Contains(SearchString)|| s.IslemSuresi.ToString().Contains(SearchString) 
                // || s.Ip.Contains(SearchString) || s.TarihSaat.ToString().Contains(SearchString) || s.Aciklama.Contains(SearchString));
                 
                //return View(logs.OrderBy(k => k.TarihSaat).ToPagedList(pageNumber, pageSize));
            }

            return View(db.log.OrderBy(k => k.TarihSaat).ToPagedList(pageNumber, pageSize));
            //db.SaveChanges();
            //var degerler = from d in db.log select d; 
            ////sorgu ifadesinin bir yan tümceyle başlaması gerekir from:
            ////kaynak dizindeki her ögeyi temsil eden yerel bir aralık değişkeni 
            ////sorgu veya alt sorgunun çalıştırılacağı veri kaynağı
            
            //return View(degerler.ToList());
        }

        public bool Save(Log log)
        {
            db.log.Add(log);
            db.SaveChanges();
            return false;
        }
    }
}