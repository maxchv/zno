using System;
using System.Collections.Generic;
using System.Text;
using HtmlAgilityPack;
using Zno.Parser.Abstruction;
using Zno.Parser.Helpers;
using Zno.Parser.Models.QuestionBodyAnswerParserImpl;
using Zno.Parser.Models.Enums;
using Zno.Parser.Models.Json;
using Zno.Parser.Models.QuestionsParserImpl;
using Zno.DAL.Entities;

namespace Zno.Parser.Models
{
    public class HtmlQuestion : IHtmlParser
    {
        public IQuestionBodyParser QuestionBody { get; set; }
        public HtmlQuestionType QuestionType { get; set; }
        public IList<HtmlAnswer> Answers { get; private set; }

        public HtmlQuestion() {
            Answers = new List<HtmlAnswer>();
        }

        //private string ParseQuestionAnswers(HtmlNode node) {
        //    string returnAnswers = "";

        //    var answersNode = node.EndNode.SelectSingleNode(XPathBuild.XPathFromNode(node, "/div[@class=\"answers\"]"));

        //    if (answersNode != null) {
        //        var answerNode = answersNode.FirstChild;
        //        while (answerNode != null && answerNode.Name == "div") {
        //            //returnAnswers = new StringBuilder(returnAnswers).Append(answer.)
        //            JsonContent content = new JsonContent();
        //            content.Mark = answerNode.FirstChild.InnerText;
        //            var dataNode = answerNode.FirstChild?.NextSibling;
        //            if (dataNode?.Name == "img")
        //            {
        //                content.Data = dataNode.Attributes["src"]?.Value;
        //            }
        //            else {
        //                content.Data = dataNode?.InnerText;
        //            }

        //            answerNode = answerNode.NextSibling;
        //        }
        //    }

        //    return returnAnswers;
        //}


        private void ParseQuestionBody(HtmlNode taskCard) {
            var questionNode = taskCard.EndNode.SelectSingleNode(XPathBuild.XPathFromNode(taskCard, "/div[@class=\"question\"]"));
            var iframeNode = questionNode.EndNode.SelectSingleNode(XPathBuild.XPathFromNode(questionNode, "//iframe"));
            if (iframeNode != null)
            {
                QuestionBody = new IframeQuestionBody();
                QuestionBody.InitByHtmlNode(questionNode);

            }
            else {
                var imageNode = questionNode.EndNode.SelectSingleNode(XPathBuild.XPathFromNode(questionNode, "//img"));
                if (imageNode != null)
                {
                    QuestionBody = new ImageQuestionBody();
                    QuestionBody.InitByHtmlNode(questionNode);
                }
                else {
                    var textNode = questionNode.EndNode.SelectSingleNode(XPathBuild.XPathFromNode(questionNode, "/p"));
                    QuestionBody = new TextQuestionBody();
                    QuestionBody.InitByHtmlNode(textNode == null ? questionNode : textNode);
                }
            }
            
        }

        // Парсин вопросов
        public void InitByHtmlNode(HtmlNode node)
        {
            var answerTypeNode = node.EndNode.SelectSingleNode(new StringBuilder(node.XPath).Append("/div[@class=\"description\"]/a").ToString());


            //Получение правильных ответов
            switch (answerTypeNode.InnerText)
            {
                case "Завдання з вибором однієї правильної відповіді":
                    QuestionType = HtmlQuestionType.One;
                    break;
                case "Завдання на встановлення відповідності (логічні пари)":
                    QuestionType = HtmlQuestionType.Many;
                    break;
                case "Завдання відкритої форми з короткою відповіддю, структуроване":
                case "Завдання відкритої форми з короткою відповіддю (1 вид)":
                    QuestionType = HtmlQuestionType.Manual;
                    break;
                case "Завдання відкритого типу з розгорнутою відповіддю, тому не передбачає автоматичного оцінюваня":
                default:
                    QuestionType = HtmlQuestionType.Task;
                    break;
            }

            // Получение тела вопроса
            ParseQuestionBody(node);


        }

    }
}
