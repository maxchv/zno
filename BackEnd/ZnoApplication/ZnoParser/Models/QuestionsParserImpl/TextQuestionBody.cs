using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using HtmlAgilityPack;
using Newtonsoft.Json;
using Zno.Parser.Abstruction;
using Zno.Parser.Models.Enums;

namespace Zno.Parser.Models.QuestionsParserImpl
{
    public class TextQuestionBody : IQuestionBodyParser
    {
        public string Data { get; set; }
        public HtmlContentType ContentType { get; set; }

        public TextQuestionBody()
        {
            Data = "";
            ContentType = HtmlContentType.None;
        }

        public string GetJsonQuestion()
        {
            return JsonConvert.SerializeObject(this);
        }

        public void InitByHtmlNode(HtmlNode textNode)
        {
            ContentType = HtmlContentType.String;
            //if (textNode.FirstChild.Name == "#text") {
            var nextNode = textNode;
            do
            {
                string data = nextNode.InnerText;
                data = Regex.Replace(data, "\r", "");
                data = Regex.Replace(data, "\n", "");
                Data += data == "" ? "" : data + "\n\r";
                nextNode = nextNode.NextSibling;
            } while (nextNode != null);
            //}
        }

        public HtmlContentType GetContentType()
        {
            return ContentType;
        }
    }
}
