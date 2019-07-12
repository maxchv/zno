using System;
using Zno.Parser;
using Zno.Parser.Helpers;

namespace ZnoParserConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            ZnoParser parser = new ZnoParser("server=localhost;port=3306;userid=root;password=;database=zno;");
            parser.SerializeParser = new XmlSerializableParser("parser.xml");
            parser.StartParsing();
            Console.WriteLine("Add comleted");
        }
    }
}
