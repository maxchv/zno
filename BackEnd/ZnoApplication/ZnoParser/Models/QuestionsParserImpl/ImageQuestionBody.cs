using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using HtmlAgilityPack;
using Newtonsoft.Json;
using Zno.Parser.Abstruction;
using Zno.Parser.Helpers;
using Zno.Parser.Models.Enums;

namespace Zno.Parser.Models.QuestionsParserImpl
{
    public class ImageQuestionBody : IQuestionBodyParser
    {
        public string Url { get; set; }
        public string Title { get; set; }
        public HtmlContentType ContentType { get; set; }

        public ImageQuestionBody()
        {
            Title = "";
            Url = "";
            ContentType = HtmlContentType.None;
        }

        public string GetJsonQuestion()
        {
            return JsonConvert.SerializeObject(this);
        }

        public void InitByHtmlNode(HtmlNode questionNode)
        {
            var imageNode = questionNode.EndNode.SelectSingleNode(XPathBuild.XPathFromNode(questionNode, "//img"));

            Url = imageNode.Attributes["src"].Value;

            if (imageNode.Name == "img")
                ContentType = HtmlContentType.Image;

            var paragraphs = questionNode.EndNode.SelectNodes(XPathBuild.XPathFromNode(questionNode, "//p//text()"));
            if (ContentType == HtmlContentType.Image && paragraphs?.Count > 0)
            {
                foreach (var nextNode in paragraphs)
                {

                    string text = nextNode.InnerText;
                    text = Regex.Replace(text, "\\n", "");
                    text = Regex.Replace(text, "\\r", "");
                    text = text == "" ? "" : text + "\n\r";
                    Title += text;
                }
            }


        }

        public HtmlContentType GetContentType()
        {
            return ContentType;
        }
    }
}
