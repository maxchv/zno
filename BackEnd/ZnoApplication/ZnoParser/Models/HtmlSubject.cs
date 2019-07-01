using System;
using System.Collections.Generic;
using System.Text;
using HtmlAgilityPack;
using Zno.Parser.Abstruction;

namespace Zno.Parser.Models
{
    [Serializable]
    public class HtmlSubject : IHtmlParser
    {
        public string Name { get; set; }
        public string Url { get; set; }
        public List<HtmlTest> Tests { get; set; }

        public HtmlSubject()
        {

        }

        public HtmlSubject(string name, string url)
        {
            Name = name;
            Url = url;
            Tests = new List<HtmlTest>();
        }

        public void AddTests(IList<HtmlTest> tests) {
            Tests = (List<HtmlTest>)tests;
            
        }

        public void InitByHtmlNode(HtmlNode node)
        {
            throw new NotImplementedException();
        }
    }
}
