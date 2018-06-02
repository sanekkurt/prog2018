using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PizzaOrder
{
    /// <summary>
    /// Информация о заказе
    /// </summary>
    public class Order
    {
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
        public PizzaRequirements Pizza { get; set; }
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

    public class PizzaRequirements
    {
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
        public Addition additions { get; set; }
    }

    public class Addition
    {
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
    public class Program
    {
        static void Main()
        { }
    }
}
