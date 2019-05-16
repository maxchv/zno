using System;
using System.Collections.Generic;
using System.Text;

namespace ZnoParser.Models
{
    public class HtmlSubject
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


    }
}
