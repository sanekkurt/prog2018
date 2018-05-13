using System;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Security.Cryptography.Xml;
using PizzaOrder;
using System.Xml.Serialization;
using System.Xml;

namespace PizzaOrderLicenceGenerator
{
    class Program
    {
        private static void GenerateNewKeyPair()
        {
            string withSecret;
            string woSecret;
            using (var rsaCsp = new RSACryptoServiceProvider())
            {
                withSecret = rsaCsp.ToXmlString(true);
                woSecret = rsaCsp.ToXmlString(false);
            }
            File.WriteAllText("private.xml", withSecret);
            File.WriteAllText("public.xml", woSecret);
        }
        static void Main(string[] args)
        {
            if (args.Any(a => a == "--generate"))
            {
                GenerateNewKeyPair();
            }
            var dto = new LicenceDto()
            {
                ValidUntil = DateTime.Now.AddDays(7)
            };
            var fileName = string.Join("", DateTime.Now.ToString().Where(c => char.IsDigit(c)));
            new LicenceGenerator().CreateLicenseFile(dto, fileName + ".po_licence");
        }
    }
    class LicenceGenerator
    {
        private static string PrivateKey = @"<RSAKeyValue><Modulus>zYClyXBS1wm6fpPXqIJOete2vNcfYMorxkQW0juOPg1RJnRuUGZzUZ/Oqw4EJjea7UbnlWLrr8OkEjbhKuCqL6shRayFpC0R4iueMMENz9FCN0W+W6END7QeAmR3j6XCZGJE+jvVUnuZHNWRdzteu5nU+AlPBZhnCTM5UPWUSt0=</Modulus><Exponent>AQAB</Exponent><P>34f83MdFDjrw5msBVPN6KOh/ROHJbt40JI6nluRL1VaTbbjelfIOZnx44CyP62fbIlXzKvEVteehEC+7lowsOw==</P><Q>61pDaIr8ntOSjFnTVU8BU1lyj0qpEVBsuizCowcO6Px4XwGsvPpCSppwLaxY8d7QIU93E5dGHam4jVhnZpArxw==</Q><DP>rTcy4l7ki0dvYA6xhIP8OPEZlmYk8u3rmByXj50vio1BR3hHvAhL0m1IOecuS2w6alwSSEdz3Hc231Ut11ad+w==</DP><DQ>4QC/ly/hSfrdjT7HMzTAAK5wPpvTPNFV0Tu/rNqvn4DXl4TjXxwmymoRuyidNEefVWlCX0FtcCh1XvRYjo5guw==</DQ><InverseQ>LwxKehWQvNAAc0BmLdY6s8urJdci1TeVBY+EDeAJw9ZAv2tkHL+fCF5KnBcGL3danlNsXXQod8AssbvjNrPObQ==</InverseQ><D>OTkOcRYm6xwZYy+1yIVoZwp0JwdcBG6nW9EP7cCJrju1EhkvspGkoOD4AmenviCTsmihCdb+u/WlVTEU6AShPp8HzXBcPZxZ0+FzHRJ30ebNaYjjUWBGr+QZTTfjzMN5HNifMJ0IbOg/8z005efLmZj2KuyA/zsBKaBsMtnSRL0=</D></RSAKeyValue>";
        public void CreateLicenseFile(LicenceDto dto, string fileName)
        {
            var ms = new MemoryStream();
            new XmlSerializer(typeof(LicenceDto)).Serialize(ms, dto);
            RSACryptoServiceProvider rsaKey = new RSACryptoServiceProvider();
            rsaKey.FromXmlString(PrivateKey);
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.PreserveWhitespace = true;
            ms.Seek(0, SeekOrigin.Begin);
            xmlDoc.Load(ms);
            SignXml(xmlDoc, rsaKey);
            xmlDoc.Save(fileName);
        }
        public static void SignXml(XmlDocument xmlDoc, RSA Key)
        {
            if (xmlDoc == null)
                throw new ArgumentException("xmlDoc");
            if (Key == null)
                throw new ArgumentException("Key");
            SignedXml signedXml = new SignedXml(xmlDoc);
            signedXml.SigningKey = Key;
            Reference reference = new Reference();
            reference.Uri = "";
            XmlDsigEnvelopedSignatureTransform env = new XmlDsigEnvelopedSignatureTransform();
            reference.AddTransform(env);
            signedXml.AddReference(reference);
            signedXml.ComputeSignature();
            XmlElement xmlDigitalSignature = signedXml.GetXml();
            xmlDoc.DocumentElement.AppendChild(xmlDoc.ImportNode(xmlDigitalSignature, true));            
        }
    }
}

