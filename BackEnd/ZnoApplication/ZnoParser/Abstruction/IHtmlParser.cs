using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Text;

namespace ZnoParser.Abstruction
{
    interface IHtmlParser
    {
        void InitByHtmlNode(HtmlNode node);
    }
}
