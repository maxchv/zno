using System;
using Zno.Parser;
using Zno.Parser.Helpers;

namespace ZnoParserConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            ZnoParser parser = new ZnoParser("Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=ZnoDB;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False");
            parser.SerializeParser = new XmlSerializableParser("parser.xml");
            parser.StartParsing();
            Console.WriteLine("Add comleted");
        }
    }
}
