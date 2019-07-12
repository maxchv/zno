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
    [Serializable]
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

            /*var nextNode = textNode;
            do
            {
                string data = nextNode.InnerText;
                data = Regex.Replace(data, "\r", "");
                data = Regex.Replace(data, "\n", "");
                Data += data == "" ? "" : data + "\n\r";
                nextNode = nextNode.NextSibling;
            } while (nextNode != null);*/

            Data = textNode.InnerHtml;
            Data = Regex.Replace(Data, "\r", "");
            Data = Regex.Replace(Data, "\n", "");
        }

        public HtmlContentType GetContentType()
        {
            return ContentType;
        }
    }
}
