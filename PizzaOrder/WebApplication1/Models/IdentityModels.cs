using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using WebApplication1.Migrations;

namespace WebApplication1.Models
{
    // В профиль пользователя можно добавить дополнительные данные, если указать больше свойств для класса ApplicationUser. Подробности см. на странице https://go.microsoft.com/fwlink/?LinkID=317594.
    public class ApplicationUser : IdentityUser
    {
        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager)
        {
            // Обратите внимание, что authenticationType должен совпадать с типом, определенным в CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            // Здесь добавьте утверждения пользователя
            return userIdentity;
        }
    }

    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext()
            : base("DefaultConnection", throwIfV1Schema: false)
        {
            Database.SetInitializer(new MigrateDatabaseToLatestVersion<ApplicationDbContext, Configuration>());
        }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }
        public DbSet<DbOrder> Orders { get; set; }
        public DbSet<DbPizzaRequirements> DbPizzaRequirement { get; set; }
        public DbSet<DbAddition> Addition { get; set; }
    }








    /// <summary>
    /// Информация о заказе
    /// </summary>
    public class DbOrder
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        /// <summary>
        /// Дата заполнения
        /// </summary>
        public DateTime dateTime { get; set; }
        /// <summary>
        /// Полное имя заказчика
        /// </summary>
        public string FullNameCustomer { get; set; }
        /// <summary>
        /// Требования к пицце
        /// </summary>
        public DbPizzaRequirements Pizza { get; set; }
        /// <summary>
        /// Адрес
        /// </summary>
        public string Address { get; set; }
        /// <summary>
        /// Стоимость
        /// </summary>
        public double Price { get; set; }
        public Currency Currency { get; set; }
    }

    public class DbPizzaRequirements
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        /// <summary>
        /// Количество пиццы
        /// </summary>
        public List<int> numberPizza;
        /// <summary>
        /// Наименование вида пиццы
        /// </summary>
        public List<NamePizza> NamePizza { get; set; }
        /// <summary>
        /// Дополнительно к пицце
        /// </summary>
        public DbAddition Additions { get; set; }
    }

    public class DbAddition
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        /// <summary>
        /// Соус 
        /// </summary>
        public bool SauceCheck { get; set; }
        /// <summary>
        /// Количество соуса
        /// </summary>
        public List<int> numberSauce;
        public List<NameSauce> Sauce { get; set; }
        /// <summary>
        /// Питье
        /// </summary>
        public bool DrinkCheck { get; set; }
        /// <summary>
        /// Количество напитка
        /// </summary>
        public List<int> numberDrink;
        public List<NameDrink> Drink { get; set; }
    }
    /// <summary>
    /// Названия соусов
    /// </summary>
    public enum NameSauce
    {
        Italyanskij,
        Tomatnyj,
        Chesnochnyj,
        Slivochnyj
    }
    /// <summary>
    /// Названия напитков
    /// </summary>
    public enum NameDrink
    {
        CocaCola,
        Pepsi,
        Mirinda,
        Lipton,
        Nestea
    }
    /// <summary>
    /// Названия пицц
    /// </summary>
    public enum NamePizza
    {
        Bavarskaja,
        Derevnskaja,
        Grecheskaja,
        Syrnaja,
        Peperoni,
        Freeday
    }
    /// <summary>
    /// Валюта
    /// </summary>
    public enum Currency
    {
        RUB,
        USR,
        EUR
    }
}