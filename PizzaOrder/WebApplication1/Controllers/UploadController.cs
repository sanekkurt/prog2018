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

                using (var db = new ApplicationDbContext())
                {


                    /*var mNamePizza = new Collection<Models.NamePizza>();
                    foreach (var npDto in dto.Pizza.NamePizza)
                    {
                        mNamePizza.Add((Models.NamePizza)(int)npDto);
                    }
                    var mNameSauce = new Collection<Models.NameSauce>();
                    foreach (var npDto in dto.Pizza.additions.Sauce)
                    {
                        mNameSauce.Add((Models.NameSauce)(int)npDto);
                    }
                    var mNameDrink = new Collection<Models.NameDrink>();
                    foreach (var npDto in dto.Pizza.additions.Drink)
                    {
                        mNameDrink.Add((Models.NameDrink)(int)npDto);
                    }
                    var mNumberPizza = new Collection<int>();
                    foreach (var npDto in dto.Pizza.numberPizza)
                    {
                        mNumberPizza.Add(npDto);
                    }
                    var mNumberSauce = new Collection<int>();
                    foreach (var npDto in dto.Pizza.additions.numberSauce)
                    {
                        mNumberSauce.Add(npDto);
                    }
                    var mNumberDrink = new Collection<int>();
                    foreach (var npDto in dto.Pizza.additions.numberDrink)
                    {
                        mNumberDrink.Add(npDto);
                    }*/

                    var row = new DbOrder
                    {
                        dateTime = dto.dateTime,
                        FullNameCustomer = dto.FullNameCustomer,
                        Address = dto.Address,
                        Price = dto.Price,
                        Currency = (Models.Currency)(int)dto.Currency,
                    };
                    
                    row.Pizza = new DbPizzaRequirement();

                    row.Pizza.numberPizzaKernel = new Collection<DbNumberPizza>();
                    foreach (var wpDto in dto.Pizza.numberPizza)
                    {
                        row.Pizza.numberPizzaKernel.Add(new DbNumberPizza
                        {
                            numberPizza = wpDto,
                        });
                    }

                    row.Pizza.NamePizzaKernel = new Collection<DbNamePizza>();
                    foreach (var wpDto in dto.Pizza.NamePizza)
                    {
                        row.Pizza.NamePizzaKernel.Add(new DbNamePizza
                        {
                            NamePizza = (Models.NamePizza)(int)wpDto,
                        });
                    }
                    row.Pizza.Additions = new DbAddition();
                    row.Pizza.Additions.numberDrinkKernel = new Collection<DbNumberDrink>();
                    foreach (var wpDto in dto.Pizza.additions.numberDrink)
                    {
                        row.Pizza.Additions.numberDrinkKernel.Add(new DbNumberDrink
                        {
                            numberDrink = wpDto,
                        });
                    }

                    row.Pizza.Additions.DrinkKernel = new Collection<DbNameDrink>();
                    foreach (var wpDto in dto.Pizza.additions.Drink)
                    {
                        row.Pizza.Additions.DrinkKernel.Add(new DbNameDrink
                        {
                            NameDrink = (Models.NameDrink)(int)wpDto,
                        });
                    }

                    row.Pizza.Additions.numberSauceKernel = new Collection<DbNumberSauce>();
                    foreach (var wpDto in dto.Pizza.additions.numberSauce)
                    {
                        row.Pizza.Additions.numberSauceKernel.Add(new DbNumberSauce
                        {
                            numberSauce = wpDto,
                        });
                    }

                    row.Pizza.Additions.SauceKernel = new Collection<DbNameSauce>();
                    foreach (var wpDto in dto.Pizza.additions.Sauce)
                    {
                        row.Pizza.Additions.SauceKernel.Add(new DbNameSauce
                        {
                            NameSauce = (Models.NameSauce)(int)wpDto,
                        });
                    }
                    /*row.Pizza.Additions = new DbAddition
                    {
                        numberSauce = mNumberSauce,
                        numberDrink = mNumberDrink,
                        Sauce = mNameSauce,
                        Drink = mNameDrink,
                    };*/

                    db.Orders.Add(row);
                    db.SaveChanges();
                }

                return View(dto);
            }

            return RedirectToAction("Index");
        }
    }
}
