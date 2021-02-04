using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ClassicGarage.DAL;
using ClassicGarage.Models;

namespace ClassicGarage.Controllers
{
    public class RepairModelsController : Controller
    {
        private GarageContext db = new GarageContext();

        // GET: RepairModels
        public ActionResult Index()
        {
            var repairs = db.Repairs.Include(r => r.Car);
            return View(repairs.ToList());
        }

        // GET: RepairModels/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            RepairModels repairModels = db.Repairs.Find(id);
            if (repairModels == null)
            {
                return HttpNotFound();
            }
            return View(repairModels);
        }

        // GET: RepairModels/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: RepairModels/Create
        // Aby zapewnić ochronę przed atakami polegającymi na przesyłaniu dodatkowych danych, włącz określone właściwości, z którymi chcesz utworzyć powiązania.
        // Aby uzyskać więcej szczegółów, zobacz https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,CarId,Name,Description,Cost")] RepairModels repairModels)
        {
            if (ModelState.IsValid)
            {
                db.Repairs.Add(repairModels);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(repairModels);
        }

        // GET: RepairModels/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            RepairModels repairModels = db.Repairs.Find(id);
            if (repairModels == null)
            {
                return HttpNotFound();
            }
          
            return View(repairModels);
        }

        // POST: RepairModels/Edit/5
        // Aby zapewnić ochronę przed atakami polegającymi na przesyłaniu dodatkowych danych, włącz określone właściwości, z którymi chcesz utworzyć powiązania.
        // Aby uzyskać więcej szczegółów, zobacz https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,CarId,Name,Description,Cost")] RepairModels repairModels)
        {
            if (ModelState.IsValid)
            {
                db.Entry(repairModels).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            
            return View(repairModels);
        }

        // GET: RepairModels/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            RepairModels repairModels = db.Repairs.Find(id);
            if (repairModels == null)
            {
                return HttpNotFound();
            }
            return View(repairModels);
        }

        // POST: RepairModels/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            RepairModels repairModels = db.Repairs.Find(id);
            db.Repairs.Remove(repairModels);
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
