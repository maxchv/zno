using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Xml.Serialization;
using Zno.Parser.Models;

namespace Zno.Parser.Helpers
{
    public class XmlSerializableParser : ISerializeParser
    {
        private XmlSerializer formatter;
        private string fileName;
        public XmlSerializableParser(string fileName)
        {
            this.fileName = fileName;
            formatter = new XmlSerializer(typeof(HtmlSubject[]));
        }
        public void Serialize(HtmlSubject[] lists)
        {
            try
            {
                using (FileStream fs = new FileStream(fileName, FileMode.OpenOrCreate))
                {
                    formatter.Serialize(fs, lists);
                }
            }
            catch (Exception ex) {
                Console.WriteLine(ex);
            }
        }

        public HtmlSubject[] Deserialize()
        {
            HtmlSubject[] result = null;
            try
            {
                using (FileStream fs = new FileStream(fileName, FileMode.OpenOrCreate))
                {
                    result = (HtmlSubject[])formatter.Deserialize(fs);
                }
            }
            catch (Exception ex) {
                Console.WriteLine(ex);
                result = null;
            }
            return result;
        }
    }
}
