using System;
using System.Collections.Generic;
using System.Text;
using HtmlAgilityPack;
using Zno.Parser.Abstruction;

namespace Zno.Parser.Models
{
    public class HtmlSubject : IHtmlParser
    {
        public string Name { get; set; }
        public string Url { get; set; }
        public IList<HtmlTest> Tests { get; set; }

        public HtmlSubject(string name, string url)
        {
            Name = name;
            Url = url;
            Tests = new List<HtmlTest>();
        }

        public void InitByHtmlNode(HtmlNode node)
        {
            throw new NotImplementedException();
        }
    }
}
