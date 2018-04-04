using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Laba4
{
    class Program
    {
        static void Main()
        {
            HashTable data = new HashTable(11);
            data.PutPair(1, "1-Odin");
            data.PutPair(2, "2-Dva");
            data.PutPair(3, "3-Tri");

            Console.WriteLine(data.GetValueByKey(1));
            Console.WriteLine(data.GetValueByKey(2));
            Console.WriteLine(data.GetValueByKey(3));
        }
    }
    public class HashTable
    {
        class Data
        {
            public object Key { get; set; }
            public object Value { get; set; }
        }
        List<List<Data>> list;
        public int size = 0;
        public HashTable(int s)
        {
            size = s;
            list = new List<List<Data>>();
            for (int i = 0; i < size; i++)
            {
                list.Add(new List<Data>());
            }
        }
        public void PutPair(object key, object value)
        {
            var position = (int)GetPosition(key);
            foreach (var i in list[position])
            {
                if (Equals(i.Key, key))
                {
                    i.Value = value;
                    return;
                }
            }
            list[position].Add(new Data { Key = key, Value = value });
        }
        public object GetValueByKey(object key)
        {
            var position = (int)GetPosition(key);
            foreach (var i in list[position])
            {
                if (Equals(i.Key, key))
                {
                    return i.Value;
                }
            }
            return null;
        }
        public object GetPosition(object key)
        {
            var position = key.GetHashCode() % size;
            return Math.Abs(position);
        }
    }
}