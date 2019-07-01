using System;
using System.Collections.Generic;
using System.Text;
using HtmlAgilityPack;
using Newtonsoft.Json;
using Zno.Parser.Abstruction;
using Zno.Parser.Models.Enums;

namespace Zno.Parser.Models.QuestionsParserImpl
{
    [Serializable]
    public class SerializeQuestionBody : IQuestionBodyParser
    {
        public string Url { get; set; }
        public string Title { get; set; }
        public HtmlContentType ContentType { get; set; }

        public HtmlContentType GetContentType()
        {
            return ContentType;
        }

        public string GetJsonQuestion()
        {
            return JsonConvert.SerializeObject(this);
        }

        public void InitByHtmlNode(HtmlNode node)
        {
            throw new NotImplementedException();
        }

        public void initBy(ImageQuestionBody body) {
            Url = body.Url;
            Title = body.Title;
            ContentType = body.ContentType;
        }
        public void initBy(IframeQuestionBody body)
        {
            Url = body.Url;
            Title = body.Title;
            ContentType = body.ContentType;
        }
        public void initBy(TextQuestionBody body)
        {
            Url = "";
            Title = body.Data;
            ContentType = body.ContentType;
        }
    }
}
