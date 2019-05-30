using HtmlAgilityPack;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.RegularExpressions;
using Zno.DAL.Entities;
using Zno.Parser.Abstruction;
using Zno.Parser.Models.Json;

namespace Zno.Parser.Models
{
    public class HtmlTest : IHtmlParser
    {
        public HtmlTest() {
            HtmlQuestions = new List<HtmlQuestion>();
        }
        public int Id { get; set; }
        public string Url { get; set; }
        public string Name { get; set; }

        public ushort Year { get; set; }
        public string Type { get; set; }
        public string Subject { get; set; }
        public IList<HtmlQuestion> HtmlQuestions { get; set; }

        // Парсинг теста
        public void InitByHtmlNode(HtmlNode node)
        {
            string year = node.EndNode.SelectSingleNode(new StringBuilder(node.XPath).Append("/span[@class=\"year\"]").ToString()).InnerText;
            string type = node.EndNode.SelectSingleNode(new StringBuilder(node.XPath).Append("//*[@class=\"type\"]").ToString()).InnerText;
            string href = node.Attributes.Count > 0 ? node.Attributes[0].Value : "";
            var match = Regex.Match(href, "\\/\\d+");
            
            if (match.Success)
            {
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

        
        // Парсин вопросов в тесте
        public void InitTest(bool saveToFile = true)
        {
            HttpClient client = new HttpClient();
            
            var result = client.PostAsync(Url, new StringContent("do=send_all_serdata&znotest=" + Id, Encoding.UTF8, "application/x-www-form-urlencoded")).Result;
            string unicodeString = result.Content.ReadAsStringAsync().Result;

            var rootObject = JsonConvert.DeserializeObject<ZnoResult>(unicodeString);
            
            //rootObject.result.quest = new StringBuilder("<body>").Append(rootObject.result.quest).Append("</body>").ToString();

            var html = new HtmlDocument();
            html.LoadHtml(rootObject.result.quest);

            // Поиск узлов, с полным блоком вопроса, включающий и тип вопроса, и ответы, и правильные ответы
            var questionsNodes = html.DocumentNode.SelectNodes("//*[@class=\"task-card\"]");
            
            foreach (var questionNode in questionsNodes) {
                HtmlQuestion htmlQuestion = new HtmlQuestion();
                htmlQuestion.InitByHtmlNode(questionNode);
                HtmlQuestions.Add(htmlQuestion);
            }
        }


    }
}
