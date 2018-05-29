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
            //Database.SetInitializer(new DropCreateDatabaseIfModelChanges<ApplicationDbContext>());
            Database.SetInitializer(new MigrateDatabaseToLatestVersion<ApplicationDbContext, Configuration>());
        }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }
        public DbSet<DbOrder> Orders { get; set; }
        public DbSet<DbPizzaRequirement> DbPizzaRequirements { get; set; }
        public DbSet<DbNumberPizza> DbNumberPizzas { get; set; }
        public DbSet<DbNamePizza> DbNamePizzas { get; set; }
        public DbSet<DbAddition> Additions { get; set; }
        public DbSet<DbNumberSauce> DbNumberSauces { get; set; }
        public DbSet<DbNameSauce> DbNameSauces { get; set; }
        public DbSet<DbNumberDrink> DbNumberDrinks { get; set; }
        public DbSet<DbNameDrink> DbNameDrinks { get; set; }
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
        public DbPizzaRequirement Pizza { get; set; }
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

    public class DbPizzaRequirement
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        /// <summary>
        /// Количество пиццы
        /// </summary>
        public virtual Collection<DbNumberPizza> numberPizzaKernel { get; set; }
        /// <summary>
        /// Наименование вида пиццы
        /// </summary>
        public virtual Collection<DbNamePizza> NamePizzaKernel { get; set; }
        /// <summary>
        /// Дополнительно к пицце
        /// </summary>
        public DbAddition Additions { get; set; }
    }

    public class DbNumberPizza
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public int numberPizza { get; set; }
    }

    public class DbNamePizza
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public NamePizza NamePizza { get; set; }
    }


    public class DbAddition
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        /// <summary>
        /// Соус 
        /// </summary>
        //public bool SauceCheck { get; set; }
        /// <summary>
        /// Количество соуса
        /// </summary>
        public virtual Collection<DbNumberSauce> numberSauceKernel { get; set; }
        public virtual Collection<DbNameSauce> SauceKernel { get; set; }
        /// <summary>
        /// Питье
        /// </summary>
        //public bool DrinkCheck { get; set; }
        /// <summary>
        /// Количество напитка
        /// </summary>
        public virtual Collection<DbNumberDrink> numberDrinkKernel { get; set; }
        public virtual Collection<DbNameDrink> DrinkKernel { get; set; }
    }

    public class DbNumberSauce
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public int numberSauce { get; set; }
    }

    public class DbNameSauce
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public NameSauce NameSauce { get; set; }
    }

    public class DbNumberDrink
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public int numberDrink { get; set; }
    }

    public class DbNameDrink
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public NameDrink NameDrink { get; set; }
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