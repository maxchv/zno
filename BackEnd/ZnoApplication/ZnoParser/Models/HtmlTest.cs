using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using ZnoParser.Abstruction;

namespace ZnoParser.Models
{
    public class HtmlTest : IHtmlParser
    {
        public int Id { get; set; }
        public string Url { get; set; }
        public string Name { get; set; }

        public ushort Year { get; set; }
        public string Type { get; set; }
        public string Subject { get; set; }

        public void InitByHtmlNode(HtmlNode node)
        {
            string year = node.EndNode.SelectSingleNode(new StringBuilder(node.XPath).Append("/span[@class=\"year\"]").ToString()).InnerText;
            string type = node.EndNode.SelectSingleNode(new StringBuilder(node.XPath).Append("//*[@class=\"type\"]").ToString()).InnerText;
            string href = node.Attributes.Count > 0 ? node.Attributes[0].Value : "";
            var match = Regex.Match(href, "\\/\\d+");
            if (match.Success) {
                Id = int.Parse(Regex.Replace(match.Value, "\\D+", ""));
            }
            Type = type;
            try
            {
                Year = ushort.Parse(Regex.Replace(year, "\\D+", ""));
            }
            catch (Exception ex)
            {
                Year = (ushort)DateTime.Now.Year;
            }
        }

        private void InitTest() {

        }


    }
}
