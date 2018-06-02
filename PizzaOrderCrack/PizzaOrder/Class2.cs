using System.IO;
using System.Xml.Serialization;

namespace PizzaOrder
{
    public static class RideDtoHelper
    {
        private static readonly XmlSerializer Xs = new XmlSerializer(typeof(Order));
        public static void WriteToFile(string fileName, Order data)
        {
            using (var fileStream = File.Create(fileName))
            {
                Xs.Serialize(fileStream, data);
            }
        }

        public static Order LoadFromFile(string fileName)
        {
            using (var fileStream = File.OpenRead(fileName))
            {
                return (Order)Xs.Deserialize(fileStream);
            }
        }
        public static Order LoadFromStream(Stream file)
        {
            return (Order)Xs.Deserialize(file);
        }
    }
}