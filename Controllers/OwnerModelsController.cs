using ClassicGarage.DAL;
using ClassicGarage.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace ClassicGarage.Controllers
{
    public class OwnerModelsController : Controller
    {
        private GarageContext db = new GarageContext();

        // GET: OwnerModel
        //[Authorize(Users = "admin@admin.pl")]
        [Authorize (Roles ="admin")]
        public ActionResult Index(int? id )
        {
          
            var viewModel = new GarageViewModels();

            viewModel.OwnerModels = db.Owners.Include(s => s.Cars);

            if (id != null)
            {
                ViewBag.Id_Owner = id.Value;
                viewModel.CarModels = db.Cars.Where(i => i.OwnerId == id.Value).Include(o => o.Advertisement).Include(m => m.Brand);
            }


            return View(viewModel);
        }

        // GET: OwnerModel
        //[Authorize(Users = "admin@admin.pl")]
        [Authorize(Roles = "user")]
        public ActionResult IndexUser()
        {
            var viewModel = new GarageViewModels();

            viewModel.OwnerModels = db.Owners.Where(i => i.Email==User.Identity.Name).Include(s => s.Cars);
            
            Console.WriteLine(viewModel.OwnerModels);

            return View(viewModel);
        }


        //nazwa taka sama jak nazwa pliku html
        [Authorize(Roles = "admin,user")]
        public ActionResult Details(int? id)
        {
            if(id==null)
            {
                return RedirectToAction("Index");
            }
            OwnerModels owner = db.Owners.Find(id);

            if(owner == null)
            {
                return HttpNotFound();
            }

            return View(owner);
        }

        [Authorize(Roles = "admin,user")]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return RedirectToAction("Index");
            }
           
            OwnerModels owner = db.Owners.Find(id);

            if (owner == null)
            {
                return HttpNotFound();
            }
            return View(owner);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "admin,user")]
        public ActionResult Edit([Bind(Include = "ID,FirstName,LastName,PhoneNo,Email")] OwnerModels owner)
        {
            if (ModelState.IsValid)
            {
                db.Entry(owner).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(owner);
        }

        [Authorize(Roles = "admin")]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            OwnerModels owner = db.Owners.Find(id);
            if (owner == null)
            {
                return HttpNotFound();
            }
            return View(owner);
        }

        // POST: /Movies/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "admin")]
        public ActionResult DeleteConfirmed(int id)
        {
            OwnerModels owner = db.Owners.Find(id);
            db.Owners.Remove(owner);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        [Authorize(Roles = "admin,user")]
        public ActionResult Create()
        {
            return View();
        }

        [Authorize(Roles = "admin,user")]
        [HttpPost]
        public ActionResult Create([Bind(Include ="FirstName, LastName, PhoneNo, Email")] OwnerModels owner)
        {
            if(ModelState.IsValid)
            {
                db.Owners.Add(owner);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(owner);
        }
    }
}