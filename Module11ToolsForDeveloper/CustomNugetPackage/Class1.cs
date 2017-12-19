using System;
using System.IO;
using System.Xml;
using System.Xml.Serialization;

namespace CustomNugetPackage
{
    public static class Class1
    {
        public static void Method1()
        {
            var obj = new CustomClass { Property1 = "One", Property2 = "Two" };
            Console.WriteLine("This is custom nuget package: ");
            Console.WriteLine(Newtonsoft.Json.JsonConvert.SerializeObject(obj));
            XmlSerializer xs = new XmlSerializer(typeof(CustomClass));
            var xml = "";

            using (var sw = new StringWriter())
            {
                using (XmlWriter writer = XmlWriter.Create(sw))
                {
                    xs.Serialize(writer, obj);
                    xml = sw.ToString();
                }
            }

            Console.WriteLine(xml);
            Console.ReadKey();
        }
    }

    public class CustomClass
    {
        public string Property1 { get; set; }
        public string Property2 { get; set; }
    }
}
