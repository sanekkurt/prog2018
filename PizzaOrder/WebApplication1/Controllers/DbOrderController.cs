using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Collections.ObjectModel;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    public class DbOrderController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        // GET: DbOrder
        public ActionResult Index()
        {
            return View(db.Orders.ToList());
        }
        
        
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,dateTime,FullNameCustomer,Address,Price")] DbOrder dbOrders)
        {
            if (ModelState.IsValid)
            {
                db.Orders.Add(dbOrders);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(dbOrders);
        }
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DbOrder dbOrder = db.Orders.Find(id);
            if (dbOrder == null)
            {
                return HttpNotFound();
            }
            return View(dbOrder);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,dateTime,FullNameCustomer,Address,Price")] DbOrder dbOrders)
        {
            if (ModelState.IsValid)
            {
                db.Entry(dbOrders).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(dbOrders);
        }

        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DbOrder dbOrders = db.Orders.Find(id);
            if (dbOrders == null)
            {
                return HttpNotFound();
            }
            return View(dbOrders);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            DbOrder dbOrders = db.Orders.Find(id);
            //dbOrders.Pizza.Clear();
            db.Orders.Remove(dbOrders);
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