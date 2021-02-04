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
    public class PartsModelsController : Controller
    {
        private GarageContext db = new GarageContext();

        // GET: PartsModels
        public ActionResult Index(string CarId)
        {
            ViewBag.CarId = new SelectList(db.Cars, "Model", "Model");
            var parts = db.Parts.Include(p => p.Car);

            if (!String.IsNullOrEmpty(CarId))
            {
                parts = parts.Where(p => p.Car.Model.Contains(CarId));
            }

            return View(parts.ToList());
        }

        // GET: PartsModels/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PartsModels partsModels = db.Parts.Find(id);
            if (partsModels == null)
            {
                return HttpNotFound();
            }
            return View(partsModels);
        }

        // GET: PartsModels/Create
        public ActionResult Create()
        {
            ViewBag.CarId = new SelectList(db.Cars, "ID", "Model");
            return View();
        }

        // POST: PartsModels/Create
        // Aby zapewnić ochronę przed atakami polegającymi na przesyłaniu dodatkowych danych, włącz określone właściwości, z którymi chcesz utworzyć powiązania.
        // Aby uzyskać więcej szczegółów, zobacz https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,CarId,Name,PartNumber,PurchasePrice,SellPrice,PurchaseDate,SaleDate")] PartsModels partsModels)
        {
            if (ModelState.IsValid)
            {
                db.Parts.Add(partsModels);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.CarId = new SelectList(db.Cars, "ID", "Model", partsModels.CarId);
            return View(partsModels);
        }

        // GET: PartsModels/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PartsModels partsModels = db.Parts.Find(id);
            if (partsModels == null)
            {
                return HttpNotFound();
            }
            ViewBag.CarId = new SelectList(db.Cars, "ID", "Model", partsModels.CarId);
            return View(partsModels);
        }

        // POST: PartsModels/Edit/5
        // Aby zapewnić ochronę przed atakami polegającymi na przesyłaniu dodatkowych danych, włącz określone właściwości, z którymi chcesz utworzyć powiązania.
        // Aby uzyskać więcej szczegółów, zobacz https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,CarId,Name,PartNumber,PurchasePrice,SellPrice,PurchaseDate,SaleDate")] PartsModels partsModels)
        {
            if (ModelState.IsValid)
            {
                db.Entry(partsModels).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.CarId = new SelectList(db.Cars, "ID", "Model", partsModels.CarId);
            return View(partsModels);
        }

        // GET: PartsModels/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PartsModels partsModels = db.Parts.Find(id);
            if (partsModels == null)
            {
                return HttpNotFound();
            }
            return View(partsModels);
        }

        // POST: PartsModels/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            PartsModels partsModels = db.Parts.Find(id);
            db.Parts.Remove(partsModels);
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
