using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Laba2
{
    class Program
    {
        //код поиска значения value в массиве array
        public static int BinarySearch(int[] array, int value)

        {
            if (array == null)//проверка на случай пустого массива
            {
                return -1;
            }
            int left = 0; // задание левой границы поиска
            int right = array.Length-1; //задание правой границы поиска
            int middle;//задание середины
            int search = -1; // найденный индекс элемента, в случае если элемент не будет найден возвратится -1
            while (left <= right)
            {
                middle = (left + right) / 2; // нахождение середины отрезка
                if (value == array[middle])
                {
                    search = middle;
                    break;   
                }
                if (value < array[middle])
                {
                    right = middle - 1;  // смещение правой границы
                }
                else
                {
                    left = middle + 1;   // смещение левой границы
                }
            }
            if (search == -1)
            {
                return -1;
            }
            else
            {
                return search;
            }
        }
        static void Main(string[] args)
        {
            TestNegativeNumbers();
            TestNonExistentElement();
            TestSingleElementInArrayFiveElements();
            TestDuplicateItem();
            TestEmptyArray();
            TestSearchAnArrayOf100001Elements();
            Console.ReadKey();
        }
        private static void TestNegativeNumbers()
        {
            //Тестирование поиска в отрицательных числах
            int[] negativeNumbers = new[] { -5, -4, -3, -2 };
            if (BinarySearch(negativeNumbers, -3) != 2)
            {
                Console.WriteLine("! Поиск не нашёл число -3 среди чисел {-5, -4, -3, -2}");
            }
            else
            {
                Console.WriteLine("Поиск среди отрицательных чисел работает корректно");
            }
        }
        private static void TestSingleElementInArrayFiveElements()
        {
            //Тестирование поиска одного элемента в массиве из 5 элементов
            int[] array = new[] { 5, 4, 3, 2, 1 };
            if (BinarySearch(array, 3) != 2)
            {
                Console.WriteLine("! Поиск не нашёл число 3 среди чисел {5, 4, 3, 2, 1}");
            }
            else
            {
                Console.WriteLine("Поиск одного элемента в массиве из 5 элементов работает корректно");
            }
        }
        private static void TestEmptyArray()
        {
            //Тестирование поиска в пустом массиве
            int[] array = new int[0];
            if (BinarySearch(array, 3) == -1)
            {
                Console.WriteLine("Поиск в пустом массиве работает корректно");
            }
            else
            {
                Console.WriteLine("Поиск в пустом массиве работает некорректно!");
            }
        }
        private static void TestDuplicateItem()
        {
            //Тестирование поиска элемента, повторяющегося несколько раз
            int[] array = new[] { 1,5,26,30,30,30,30,30,30,30 };
            if (array[BinarySearch(array, 30)] != 30)
            {
                Console.WriteLine("! Поиск не нашёл число 30, которое повторяется в массиве несколько раз");
            }
            else
            {
                Console.WriteLine("Поиск элемента, повторяющегося несколько раз, работает корректно");
            }
        }
        private static void TestNonExistentElement()
        {
            //Тестирование поиска отсутствующего элемента
            int[] array = new[] { -5, -4, -3, -2 };
            if (BinarySearch(array, -1) >= 0)
                Console.WriteLine("! Поиск нашёл число -1 среди чисел {-5, -4, -3, -2}");
            else
                Console.WriteLine("Поиск отсутствующего элемента вернул корректный результат работает корректно");

        }
        private static void TestSearchAnArrayOf100001Elements()
        {
            //Тестирование поиска в массиве из 100001 элементов
            int[] manyElements = new int[100001];
            Random rand = new Random();
            for (int i = 0; i < 100001; i++)
            {
                manyElements[i] = rand.Next(1, 100002);
            }
            int index = rand.Next(1, 100002);
            Array.Sort(manyElements);
            int number = manyElements[index];
            if (manyElements[BinarySearch(manyElements, number)] != number)
            {
                Console.WriteLine("! Поиск работает некорректно");
            }
            else
            {
                Console.WriteLine("Поиск числа {0} со случайным индексом {1} , в рандомно заполненном массиве размером 100001, работает корректно", number, index);
            }
        }
    }
}