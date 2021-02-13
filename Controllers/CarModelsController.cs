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
    public class CarModelsController : Controller
    {
        private GarageContext db = new GarageContext();

        // GET: CarModels
        [Authorize (Roles ="admin")]
        public ActionResult Index(string BrandID, int? id, int? idRepair)
        {
            ViewBag.BrandID = new SelectList(db.Brand, "Name", "Name");

            var viewModel = new GarageViewModels();
            viewModel.CarModels = db.Cars.Include(a => a.Advertisement);
            viewModel.CarModels = db.Cars.Include(r => r.Repairs);

            if (id != null)
            {
                ViewBag.Id_Ad = id.Value;
                viewModel.AdsModels = db.Advertisements.Where(i => i.CarID == id.Value);
                viewModel.CarModels = db.Cars.Where(i => i.OwnerId == id.Value).Include(o => o.Advertisement).Include(m => m.Brand);
            }

            if (idRepair != null)
            {
                ViewBag.Id_Repair = idRepair.Value;
                viewModel.Repairs = db.Repairs.Where(i => i.CarId == idRepair.Value);
                viewModel.CarModels = db.Cars.Where(i => i.OwnerId == id.Value).Include(o => o.Repairs).Include(m => m.Brand);
            }

            viewModel.CarModels = db.Cars.Include(c => c.Owner).Include(s => s.Advertisement).Include(b => b.Brand).Include(r => r.Repairs);

            if(!String.IsNullOrEmpty(BrandID))
            {
                viewModel.CarModels = viewModel.CarModels.Where(s => s.Brand.Name.Contains(BrandID));
            }

            return View(viewModel);
        }

        // GET: CarModels
        [Authorize(Roles = "user")]
        public ActionResult IndexUser(string BrandID, int? id)
        {
            ViewBag.BrandID = new SelectList(db.Brand, "Name", "Name");

            var viewModel = new GarageViewModels();
            viewModel.CarModels = db.Cars.Where(c => c.Owner.Email == User.Identity.Name);
            

            if (id != null)
            {
                ViewBag.Id_Ad = id.Value;
                viewModel.AdsModels = db.Advertisements.Where(i => i.CarID == id.Value);
                viewModel.CarModels = db.Cars.Where(i => i.OwnerId == id.Value).Include(o => o.Advertisement).Include(m => m.Brand);
            }


            viewModel.CarModels = db.Cars.Include(c => c.Owner).Where(c => c.Owner.Email == User.Identity.Name).Include(s => s.Advertisement).Include(b => b.Brand);

            if (!String.IsNullOrEmpty(BrandID))
            {
                viewModel.CarModels = viewModel.CarModels.Where(s => s.Brand.Name.Contains(BrandID));
            }

            return View(viewModel);
        }



        // GET: CarModels/Details/5
        [Authorize(Roles = "admin, user")]
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CarModels carModels = db.Cars.Find(id);
            if (carModels == null)
            {
                return HttpNotFound();
            }
            return View(carModels);
        }

        // GET: CarModels/Create
        [Authorize(Roles = "admin, user")]
        public ActionResult Create()
        {
            ViewBag.BrandID = new SelectList(db.Brand, "Id", "Name");
            ViewBag.OwnerId = new SelectList(db.Owners, "ID", "FirstName");
            return View();
        }

        [Authorize(Roles = "user")]
        public ActionResult CreateUser()
        {
            ViewBag.BrandID = new SelectList(db.Brand, "Id", "Name");
  
            return View();
        }

        // POST: CarModels/Create
        // Aby zapewnić ochronę przed atakami polegającymi na przesyłaniu dodatkowych danych, włącz określone właściwości, z którymi chcesz utworzyć powiązania.
        // Aby uzyskać więcej szczegółów, zobacz https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "admin")]
        public ActionResult Create([Bind(Include = "ID,BrandID,Model,Year,VIN,Series,Photo,PurchaseDate,SaleDate,PurchasePrice,SellPrice,OwnerId")] CarModels carModels)
        {
           
            if (ModelState.IsValid)
            {
                db.Cars.Add(carModels);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.BrandID = new SelectList(db.Brand, "Id", "Name", carModels.BrandID);
            ViewBag.OwnerId = new SelectList(db.Owners, "ID", "FirstName", carModels.OwnerId);
            return View(carModels);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "user")]
        public ActionResult CreateUser([Bind(Include = "ID,BrandID,Model,Year,VIN,Series,Photo,PurchaseDate,SaleDate,PurchasePrice,SellPrice")] CarModels carModels)
        {
            var userId = db.Cars.Where(i => i.Owner.Email == User.Identity.Name);//.Select(n => n.Owner.ID);
            

            carModels.OwnerId = userId.FirstOrDefault().Owner.ID;

            if (ModelState.IsValid)
            {
                db.Cars.Add(carModels);
                db.SaveChanges();
                return RedirectToAction("IndexUser");
            }

            ViewBag.BrandID = new SelectList(db.Brand, "Id", "Name", carModels.BrandID);
            
            return View(carModels);
        }


        // GET: CarModels/Edit/5
        [Authorize(Roles = "admin, user")]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CarModels carModels = db.Cars.Find(id);
            if (carModels == null)
            {
                return HttpNotFound();
            }
            ViewBag.OwnerId = new SelectList(db.Owners, "ID", "FirstName", carModels.OwnerId);
            return View(carModels);
        }

        // POST: CarModels/Edit/5
        // Aby zapewnić ochronę przed atakami polegającymi na przesyłaniu dodatkowych danych, włącz określone właściwości, z którymi chcesz utworzyć powiązania.
        // Aby uzyskać więcej szczegółów, zobacz https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "admin, user")]
        public ActionResult Edit([Bind(Include = "ID,BrandID,Model,Year,VIN,Series,Photo,PurchaseDate,SaleDate,PurchasePrice,SellPrice,OwnerId")] CarModels carModels)
        {
            if (ModelState.IsValid)
            {
                db.Entry(carModels).State = EntityState.Modified;
                db.SaveChanges();
                if(User.IsInRole("user"))
                {
                    return RedirectToAction("IndexUser");
                } else 
                    return RedirectToAction("Index");

            }
            ViewBag.OwnerId = new SelectList(db.Owners, "ID", "FirstName", carModels.OwnerId);
            return View(carModels);
        }

   

        // GET: CarModels/Delete/5
        [Authorize(Roles = "admin, user")]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CarModels carModels = db.Cars.Find(id);
            if (carModels == null)
            {
                return HttpNotFound();
            }
            return View(carModels);
        }

        // POST: CarModels/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "admin, user")]
        public ActionResult DeleteConfirmed(int id)
        {
            CarModels carModels = db.Cars.Find(id);
            db.Cars.Remove(carModels);
            db.SaveChanges();
            
            if(User.IsInRole("user"))
            {
                return RedirectToAction("IndexUser");
            } else
            {
                return RedirectToAction("Index");
            }
           
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
