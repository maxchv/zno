using System;
using System.Collections.Generic;
using System.Text;
using HtmlAgilityPack;
using Zno.Parser.Abstruction;
using Zno.Parser.Models.Enums;

namespace Zno.Parser.Models
{
    [Serializable]
    public class HtmlAnswer : IHtmlParser
    {
        public HtmlAnswer()
        {
            HtmlContentType = HtmlContentType.None;
        }
        public string Content { get; set; }
        public HtmlContentType HtmlContentType { get; set; }
        public bool IsRight { get; set; }
        public void InitByHtmlNode(HtmlNode answer)
        {
            
        }
    }
}
