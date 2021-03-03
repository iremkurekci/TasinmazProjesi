using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Taşınmaz_Projesi.Context;
using Taşınmaz_Projesi.Models;

namespace Taşınmaz_Projesi.Controllers
{
    public class TasinmazsController : Controller
    {
        private TasinmazContext db = new TasinmazContext();

        // GET: Tasinmazs
        public ActionResult Index()
        {
            return View(db.tasinmaz.ToList());
        }

        // GET: Tasinmazs/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Tasinmaz tasinmaz = db.tasinmaz.Find(id);
            if (tasinmaz == null)
            {
                return HttpNotFound();
            }
            return View(tasinmaz);
        }

        // GET: Tasinmazs/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Tasinmazs/Create
        // Aşırı gönderim saldırılarından korunmak için bağlamak istediğiniz belirli özellikleri etkinleştirin. 
        // Daha fazla bilgi için bkz. https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "TasinmazID,Il,Ilce,Mahalle,Ada,Parsel,Nitelik,Adres")] Tasinmaz tasinmaz)
        {
            if (ModelState.IsValid)
            {
                db.tasinmaz.Add(tasinmaz);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(tasinmaz);
        }

        // GET: Tasinmazs/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Tasinmaz tasinmaz = db.tasinmaz.Find(id);
            if (tasinmaz == null)
            {
                return HttpNotFound();
            }
            return View(tasinmaz);
        }

        // POST: Tasinmazs/Edit/5
        // Aşırı gönderim saldırılarından korunmak için bağlamak istediğiniz belirli özellikleri etkinleştirin. 
        // Daha fazla bilgi için bkz. https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "TasinmazID,Il,Ilce,Mahalle,Ada,Parsel,Nitelik,Adres")] Tasinmaz tasinmaz)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tasinmaz).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(tasinmaz);
        }

        // GET: Tasinmazs/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Tasinmaz tasinmaz = db.tasinmaz.Find(id);
            if (tasinmaz == null)
            {
                return HttpNotFound();
            }
            return View(tasinmaz);
        }

        // POST: Tasinmazs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Tasinmaz tasinmaz = db.tasinmaz.Find(id);
            db.tasinmaz.Remove(tasinmaz);
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
