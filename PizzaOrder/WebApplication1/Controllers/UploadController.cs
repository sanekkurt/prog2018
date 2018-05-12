using System;
using System.Collections.Generic;
using System.Linq;
using System.Collections.ObjectModel;
using System.Web;
using System.Web.Mvc;
using WebApplication1.Models;
using PizzaOrder;

namespace WebApplication1.Controllers
{
    /*public class UploadController : Controller
     {
         // GET: Upload
         public ActionResult Index()
         {
             return View();
         }

         [HttpPost]
         public ActionResult Print(HttpPostedFileBase file)
         {
             if (file != null && file.ContentLength > 0)
             {
                 var dto = RideDtoHelper.LoadFromStream(file.InputStream);
                 return View(dto);
             }

             return RedirectToAction("Index");
         }
     }*/
    public class UploadController : Controller
    {
        // GET: Upload
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Print(HttpPostedFileBase file)
        {
            if (file != null && file.ContentLength > 0)
            {
                var dto = RideDtoHelper.LoadFromStream(file.InputStream);

               /*using (var db = new ApplicationDbContext())
                {
                    var row = new DbOrder
                    {
                        dateTime = dto.dateTime,
                        FullNameCustomer = dto.FullNameCustomer,
                        Address = dto.Address,
                        Price = dto.Price,
                        Currency = (Models.Currency)dto.Currency,
                        //BabySeat = dto.Car?.BabySeat?.FirstOrDefault()?.Age,
                    };

                    row.Pizza = new DbPizzaRequirements();
                    row.Pizza.numberPizza = dto.Pizza.numberPizza;
                    //row.Pizza.NamePizza = dto.Pizza.NamePizza;
                    row.Pizza.NamePizza = new List<Models.NamePizza>();
                    foreach (var npDto in dto.Pizza.NamePizza)
                    {
                        row.Pizza.NamePizza.Add((Models.NamePizza)(int)npDto);
                    }
                    row.Pizza.Additions = new DbAddition();
                    row.Pizza.Additions.numberSauce = dto.Pizza.additions.numberSauce;
                    row.Pizza.Additions.Sauce = new List<Models.NameSauce>();
                    foreach (var npDto in dto.Pizza.additions.Sauce)
                    {
                        row.Pizza.Additions.Sauce.Add((Models.NameSauce)(int)npDto);
                    }
                    row.Pizza.Additions.numberDrink = dto.Pizza.additions.numberDrink;
                    row.Pizza.Additions.Drink = new List<Models.NameDrink>();
                    foreach (var npDto in dto.Pizza.additions.Drink)
                    {
                        row.Pizza.Additions.Drink.Add((Models.NameDrink)(int)npDto);
                    }

                    db.Orders.Add(row);
                    db.SaveChanges();
                }*/

                return View(dto);
            }

            return RedirectToAction("Index");
        }
    }
}
