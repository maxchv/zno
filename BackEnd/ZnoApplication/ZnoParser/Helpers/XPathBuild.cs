using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Text;

namespace Zno.Parser.Helpers
{
    public class XPathBuild
    {
        public static string XPathFromNode(HtmlNode node, string str) {
            return new StringBuilder(node.XPath).Append(str).ToString();
        }
    }
}
