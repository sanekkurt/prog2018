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
        /// Наименование вида пиццы
        /// </summary>
        public NamePizza NamePizza { get; set; }
        /// <summary>
        /// Дополнительно к пицце
        /// </summary>
        public List<Addition> additions { get; set; }
    }

    public class Addition
    {
        /// <summary>
        /// Соус 
        /// </summary>
        public bool Sauce { get; set; }
        /// <summary>
        /// Питье
        /// </summary>
        public Drink Drink { get; set; }
    }
    /// <summary>
    /// Названия напитков
    /// </summary>
    public enum Drink
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
        Rubles
    }
}
