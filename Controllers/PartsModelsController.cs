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
        [Authorize(Roles = "admin")]
        public ActionResult Index()
        {
            var viewModel = new GarageViewModels();

      
            ViewBag.CarId = new SelectList(db.Cars, "Model", "Model");

           viewModel.Parts = db.Parts;

           viewModel.Parts = db.Parts.Include(p => p.Car);
            
            return View(viewModel);

        }

        // GET: PartsModels
        [Authorize(Roles = "user")]
        public ActionResult IndexUser(string BrandID)
        {
            var viewModel = new GarageViewModels();

            ViewBag.BrandID = new SelectList(db.Brand, "Name", "Name");

            ViewBag.CarId = new SelectList(db.Cars, "Model", "Model");

            viewModel.OwnerModels = db.Owners.Where(o => o.Email == User.Identity.Name);
            viewModel.Parts = db.Parts.Include(p => p.Car);
            

            if (!String.IsNullOrEmpty(BrandID))
            {
                viewModel.Parts = viewModel.Parts.Where(s => s.Brand.Contains(BrandID));
            }

            return View(viewModel);
        }

        // GET: PartsModels/Details/5
        [Authorize(Roles = "admin")]
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
            var viewModel = new GarageViewModels();

            viewModel.Parts = db.Parts.Include(p => p.Car);
            viewModel.OwnerModels = db.Owners.Where(o => o.ID == partsModels.Owner);

            return View(viewModel);
        }

        // GET: PartsModels/Create
        [Authorize(Roles = "admin")]
        public ActionResult Create(int? id)
        {
            if(id!=null)
            {
                CarModels car = db.Cars.Where(c => c.ID == id).FirstOrDefault();
                OwnerModels owner = db.Owners.Where(o => o.Cars.Contains(car)).FirstOrDefault();
                RepairModels repair = db.Repairs.Where(r => r.Car.Owner == owner).FirstOrDefault();
                return View(repair);
            }
            
            ViewBag.CarId = new SelectList(db.Cars, "ID", "Model");
            return View();
        }

        // POST: PartsModels/Create
        // Aby zapewnić ochronę przed atakami polegającymi na przesyłaniu dodatkowych danych, włącz określone właściwości, z którymi chcesz utworzyć powiązania.
        // Aby uzyskać więcej szczegółów, zobacz https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "admin")]
        public ActionResult Create([Bind(Include = "CarId,Name,PartNumber,PurchasePrice,SellPrice,PurchaseDate,SaleDate")] PartsModels partsModels)
        {
           
            ViewBag.CarId = new SelectList(db.Cars, "ID", "Model", partsModels.CarId);

            if(db.Parts.Count()>0)
            {
                var max = db.Parts.OrderByDescending(p => p.ID).FirstOrDefault().ID;
                partsModels.ID = max + 1;
            } else
            {
                var max = 0;
                partsModels.ID = max + 1;
            }

            var carBrand =  db.Cars.Where(c => c.ID == partsModels.CarId).FirstOrDefault().Brand.Name;
            var car =  db.Cars.Where(c => c.ID == partsModels.CarId).FirstOrDefault();

            partsModels.Owner = 0;
            partsModels.Brand = carBrand;
            partsModels.Car = car;
             
             db.Parts.Add(partsModels);
             db.SaveChanges();
               return RedirectToAction("Index");
            
           
        }

        // GET: PartsModels/Edit/5
        [Authorize(Roles = "admin")]
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
        [Authorize(Roles = "admin")]
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
        [Authorize(Roles = "admin")]
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



        [Authorize(Roles = "user")]
        public ActionResult Buy(int? id)
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

        [HttpPost]
        [ValidateAntiForgeryToken]
        // GET: PartsModels/Delete/5
        [Authorize(Roles = "user")]
        public ActionResult Buy([Bind(Include = "ID,CarId,Brand,Name,PartNumber,PurchasePrice,SellPrice,PurchaseDate,SaleDate")] PartsModels partsModels)
        {

            //if (ModelState.IsValid)
            //{
                DateTime newDate = DateTime.Now;

                OwnerModels ownerId = db.Owners.Where(o => o.Email == User.Identity.Name).FirstOrDefault();
                partsModels.SaleDate = newDate;
                partsModels.Owner = ownerId.ID;
                db.Parts.Add(partsModels);
                db.SaveChanges();
                return RedirectToAction("IndexUser");
            //}
            //return RedirectToAction("IndexUser");

        }

        // POST: PartsModels/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "admin")]
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
