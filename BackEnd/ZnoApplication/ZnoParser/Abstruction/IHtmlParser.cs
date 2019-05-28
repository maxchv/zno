using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Text;

namespace Zno.Parser.Abstruction
{
    interface IHtmlParser
    {
        void InitByHtmlNode(HtmlNode node);
    }
}
