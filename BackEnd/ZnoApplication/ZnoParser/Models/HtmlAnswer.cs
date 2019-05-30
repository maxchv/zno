using System;
using System.Collections.Generic;
using System.Text;
using HtmlAgilityPack;
using Zno.Parser.Abstruction;

namespace Zno.Parser.Models
{
    public class HtmlAnswer : IHtmlParser
    {
        public string Content { get; set; }
        public void InitByHtmlNode(HtmlNode node)
        {
            
        }
    }
}
