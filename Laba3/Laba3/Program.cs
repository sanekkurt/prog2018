using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Laba3
{
    class Program
    {
        static void Main()
        {
            TestThreeElementArray();
            TestArrayOneHundredIdentificalNumbers();
            TestSortArrayOf1000Elements();
            TestSortEmptyArray();
            Console.ReadKey();
        }
        private static void TestThreeElementArray()
        {
            //Тестирование сортировки массива из трёх элементов
            int[] array = new[] { 3, 1, 2,};
            int[] pass = new[] { 1, 2, 3 };
            qSort(array);

            if (array.SequenceEqual(pass)!=true)
            {
                Console.WriteLine("! Сортировка для массива из трех элементов работает неверно !");
            }
            else
            {
                Console.WriteLine("Сортировка для массива из трех элементов работает корректно");
            }
        }
        private static void TestArrayOneHundredIdentificalNumbers()
        {
            //Тестирование сортировки массива из 100 одинаковых чисел
            int[] array = new int[100];
            int[] pass = new int[100];
            for (int i =0; i<100;i++)
            {
                array[i] = 1;
                pass[i] = 1;
            }
            qSort(array);
            if (array.SequenceEqual(pass) != true)
            {
                Console.WriteLine("! Сортировка для массива из ста одинаковых чисел работает неверно !");
            }
            else
            {
                Console.WriteLine("Сортировка для массива из ста одинаковых чисел работает корректно");
            }
        }
        private static void TestSortArrayOf1000Elements()
        {
            //Тестирование упорядочивания случайных пар
            int[] array = new int[1000];
            Random rand = new Random();
            for (int i = 0; i < 1000; i++)
            {
                array[i] = rand.Next(1, 1000);
            }
            qSort(array);
            for(int i = 0; i<10; i++)
            {
                int j = rand.Next(1, 999);
                if(array[j]<=array[j+1])
                {
                    Console.WriteLine("{0} случайная пара упорядочена верно", i+1);
                }
                else
                {
                    Console.WriteLine("{0} случайная пара упорядочена не верно", i+1);
                }
            }
        }
        private static void TestSortEmptyArray()
        {
            //Тест на сортировку пустого массива
            int[] array = new int[0];
            int[] pass = new int[0];
            qSort(array);
            if (array.SequenceEqual(pass) != true)
            {
                Console.WriteLine("! Сортировка пустого массива работает неверно !");
            }
            else
            {
                Console.WriteLine("Сортировка пустого массива работает корректно");
            }
        }
        static void qSort(int[] array)
        {
            if (array.Length == 0)//проверка на нулевой размер массива
            {
                int[] temp = new int[0];
            }
            else
            qSort(array, 0, array.Length - 1);
        }
        static void qSort(int[] array, int start, int end)
        {
            if (end == start) return;
            var support = array[end];
            var upperIndex = start;
            for (int lowerIndex = start; lowerIndex <= end - 1; lowerIndex++)
                if (array[lowerIndex] <= support)
                {
                    var temp = array[lowerIndex];
                    array[lowerIndex] = array[upperIndex];
                    array[upperIndex] = temp;
                    upperIndex++;
                }
            var n = array[upperIndex];
            array[upperIndex] = array[end];
            array[end] = n;
            if (upperIndex > start) qSort(array, start, upperIndex - 1);
            if (upperIndex < end) qSort(array, upperIndex + 1, end);
        }
    }
}