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
    [Serializable]
    public class HtmlQuestion : IHtmlParser
    {
        public SerializeQuestionBody QuestionBody { get; set; }
        public HtmlQuestionType QuestionType { get; set; }
        public List<HtmlAnswer> HtmlAnswers { get; private set; }
        private FactoryAnswerParser factoryAnswer;

        public HtmlQuestion() {
            HtmlAnswers = new List<HtmlAnswer>();
            QuestionBody = new SerializeQuestionBody();
            factoryAnswer = new FactoryAnswerParser();
        }

        private void ParseQuestionBody(HtmlNode taskCard) {
            var questionNode = taskCard.EndNode.SelectSingleNode(XPathBuild.XPathFromNode(taskCard, "/div[@class=\"question\"]"));
            var iframeNode = questionNode.EndNode.SelectSingleNode(XPathBuild.XPathFromNode(questionNode, "//iframe"));
            if (iframeNode != null)
            {
                var questionBody = new IframeQuestionBody();
                questionBody.InitByHtmlNode(questionNode);
                QuestionBody.initBy(questionBody);
            }
            else {
                var imageNode = questionNode.EndNode.SelectSingleNode(XPathBuild.XPathFromNode(questionNode, "//img"));
                if (imageNode != null)
                {
                    var questionBody = new ImageQuestionBody();
                    questionBody.InitByHtmlNode(questionNode);
                    QuestionBody.initBy(questionBody);
                }
                else {
                    var textNode = questionNode.EndNode.SelectSingleNode(XPathBuild.XPathFromNode(questionNode, "/p"));
                    var questionBody = new TextQuestionBody();
                    //questionBody.InitByHtmlNode(textNode == null ? questionNode : textNode);
                    questionBody.InitByHtmlNode(questionNode);
                    QuestionBody.initBy(questionBody);
                }
            }   
        }

        // Парсин вопросов
        public void InitByHtmlNode(HtmlNode taskCard)
        {
            HtmlAnswers.Clear();
            var answerTypeNode = taskCard.EndNode.SelectSingleNode(new StringBuilder(taskCard.XPath).Append("/div[@class=\"description\"]/a").ToString());

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
            ParseQuestionBody(taskCard);

            factoryAnswer.QuestionContentType = QuestionBody.ContentType;
            factoryAnswer.QuestionType = QuestionType;
            factoryAnswer.StrategyAnswerParser.InitByHtmlNode(taskCard);
            HtmlAnswers.AddRange(factoryAnswer.StrategyAnswerParser.GetAnswers());
        }

    }
}
