using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace HashTableTests
{

    [TestClass]
    public class HashTableTests
    {
        [TestMethod]
        public void FindThreeElementTest()
        {
            var data = new Laba4.HashTable(3);
            data.PutPair("1", "1-odin");
            data.PutPair("2", "2-dva");
            data.PutPair("3", "3-tri");
            Assert.AreEqual(data.GetValueByKey("1"), "1-odin");
            Assert.AreEqual(data.GetValueByKey("2"), "2-dva");
            Assert.AreEqual(data.GetValueByKey("3"), "3-tri");
        }

        [TestMethod]
        public void DifferentValueTest()
        {
            var data = new Laba4.HashTable(3);
            data.PutPair("1", "1-odin");
            data.PutPair("1", "2-dva");
            Assert.AreEqual(data.GetValueByKey("1"), "2-dva");
        }

        [TestMethod]
        public void FindOneElementInSetTest()
        {
            var data = new Laba4.HashTable(10000);

            for (int i = 0; i < 10000; i++)
            {
                data.PutPair(i, i);
            }
            Random rand = new Random();
            int z = rand.Next(1, 10000);
            Assert.AreEqual(data.GetValueByKey(z), z);
        }

        [TestMethod]
        public void FindManyEmptyElementsInSetTests()
        {
            var data = new Laba4.HashTable(10000);

            for (int i = 0; i < 10000; i++)
            {
                data.PutPair(i, i);
            }
            for (int i = 10000; i < 11000; i++)
            {
                Assert.AreEqual(data.GetValueByKey(i), null);
            }
        }
    }
}
