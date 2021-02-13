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
    public class AdvertisementModelsController : Controller
    {
        private GarageContext db = new GarageContext();

        
        // GET: AdvertisementModels
        public ActionResult Index()
        {
            return View(db.Advertisements.ToList());
        }


        // GET: AdvertisementModels/Details/5
        
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AdvertisementModels advertisementModels = db.Advertisements.Find(id);
            if (advertisementModels == null)
            {
                return HttpNotFound();
            }
            return View(advertisementModels);
        }

        // GET: AdvertisementModels/Create
        [Authorize(Roles = "admin")]
        public ActionResult Create()
        {
            ViewBag.CarID = new SelectList(db.Cars, "ID", "Model");
            return View();
        }

        [Authorize(Roles = "user")]
        public ActionResult CreateUser()
        {
            ViewBag.CarID = new SelectList(db.Cars, "ID", "Model");
            return View();
        }

        // POST: AdvertisementModels/Create
        // Aby zapewnić ochronę przed atakami polegającymi na przesyłaniu dodatkowych danych, włącz określone właściwości, z którymi chcesz utworzyć powiązania.
        // Aby uzyskać więcej szczegółów, zobacz https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "admin")]
        public ActionResult Create([Bind(Include = "ID,CarID,Active,OfferAmount")] AdvertisementModels advertisementModels)
        {
            if (ModelState.IsValid)
            {
                db.Advertisements.Add(advertisementModels);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(advertisementModels);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "user")]
        public ActionResult CreateUser([Bind(Include = "ID,CarID,Active,OfferAmount")] AdvertisementModels advertisementModels)
        {
            if (ModelState.IsValid)
            {
                db.Advertisements.Add(advertisementModels);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(advertisementModels);
        }

        // GET: AdvertisementModels/Edit/5
        [Authorize(Roles = "admin, user")]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AdvertisementModels advertisementModels = db.Advertisements.Find(id);
            if (advertisementModels == null)
            {
                return HttpNotFound();
            }
            return View(advertisementModels);
        }

        // POST: AdvertisementModels/Edit/5
        // Aby zapewnić ochronę przed atakami polegającymi na przesyłaniu dodatkowych danych, włącz określone właściwości, z którymi chcesz utworzyć powiązania.
        // Aby uzyskać więcej szczegółów, zobacz https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "admin, user")]
        public ActionResult Edit([Bind(Include = "ID,CarID,Active,OfferAmount")] AdvertisementModels advertisementModels)
        {
            if (ModelState.IsValid)
            {
                db.Entry(advertisementModels).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(advertisementModels);
        }

        // GET: AdvertisementModels/Delete/5
        [Authorize(Roles = "admin, user")]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AdvertisementModels advertisementModels = db.Advertisements.Find(id);
            if (advertisementModels == null)
            {
                return HttpNotFound();
            }
            return View(advertisementModels);
        }

        // POST: AdvertisementModels/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "admin, user")]
        public ActionResult DeleteConfirmed(int id)
        {
            AdvertisementModels advertisementModels = db.Advertisements.Find(id);
            db.Advertisements.Remove(advertisementModels);
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
