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
using OfficeOpenXml;
using System.IO;
using System.Web.Hosting;

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
        public ActionResult Save(int? id)
        {
            /*DbOrder dbOrder = db.Orders.Find(id);
            var order = db.Orders.Find(dbOrder.Id);*/

            var dbcontext = new ApplicationDbContext();
            var order = dbcontext.Orders.Find(id);
            var pizza = dbcontext.DbPizzaRequirements.Find(id);
            var addition = dbcontext.Additions.Find(id);
            ExcelPackage pkg;
            using (var stream = System.IO.File.OpenRead(HostingEnvironment.ApplicationPhysicalPath + "information.xlsx"))
            {
                pkg = new ExcelPackage(stream);
                stream.Dispose();
            }

            var worksheet = pkg.Workbook.Worksheets[1];
            worksheet.Cells[3, 1].Value = "date Time:";
            worksheet.Cells[3, 2].Value = order.dateTime.ToString();
            worksheet.Cells[4, 1].Value = "Full Name Customer:";
            worksheet.Cells[4, 2].Value = order.FullNameCustomer;
            worksheet.Cells[5, 1].Value = "Address:";
            worksheet.Cells[5, 2].Value = order.Address;
            worksheet.Cells[6, 1].Value = "Price:";
            worksheet.Cells[6, 2].Value = order.Price;
            worksheet.Cells[7, 1].Value = "Currency:";
            worksheet.Cells[7, 2].Value = order.Currency;
            worksheet.Cells[9, 1].Value = "Name Pizza:";
            int i = 0;
            foreach (var b in pizza.NamePizzaKernel)
            {
                worksheet.Cells[10 + i, 1].Value = b.NamePizza;
                i++;
            }
            worksheet.Cells[9, 2].Value = "Number Pizza:";
            i = 0;
            foreach (var b in pizza.numberPizzaKernel)
            {
                worksheet.Cells[10 + i, 2].Value = b.numberPizza;
                i++;
            }
            worksheet.Cells[9, 3].Value = "Name Drink:";
            i = 0;
            foreach (var b in addition.DrinkKernel)
            {
                worksheet.Cells[10 + i, 3].Value = b.NameDrink;
                i++;
            }
            worksheet.Cells[9, 4].Value = "Number Drink:";
            i = 0;
            foreach (var b in addition.numberDrinkKernel)
            {
                worksheet.Cells[10 + i, 4].Value = b.numberDrink;
                i++;
            }
            worksheet.Cells[9, 5].Value = "Name Sauce:";
            i = 0;
            foreach (var b in addition.SauceKernel)
            {
                worksheet.Cells[10 + i, 5].Value = b.NameSauce;
                i++;
            }
            worksheet.Cells[9, 6].Value = "Number Sauce:";
            i = 0;
            foreach (var b in addition.numberSauceKernel)
            {
                worksheet.Cells[10 + i, 6].Value = b.numberSauce;
                i++;
            }
            /*worksheet.Cells[8, 2].Value = "Fridge:";
            worksheet.Cells[8, 3].Value = order.Fridge;
            worksheet.Cells[9, 2].Value = "Additional service price (rubles):";
            worksheet.Cells[9, 3].Value = order.AdditionalServicePrice;
            worksheet.Cells[5, 1].Value = "Filled time:";
            worksheet.Cells[6, 1].Value = order.FilledTime.ToString();*/

            worksheet.Cells.AutoFitColumns();
            var ms = new MemoryStream();
            pkg.SaveAs(ms);
            return File(ms.ToArray(), "application/ooxml", (order.dateTime.ToString()).Replace(" ", "") + ".xlsx");
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