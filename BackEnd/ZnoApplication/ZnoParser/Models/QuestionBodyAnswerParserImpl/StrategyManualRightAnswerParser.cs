using System;
using System.Collections.Generic;
using System.Text;
using HtmlAgilityPack;
using Zno.Parser.Abstruction;
using Zno.Parser.Helpers;

namespace Zno.Parser.Models.QuestionBodyAnswerParserImpl
{
    public class StrategyManualRightAnswerParser : IStrategyAnswerParser
    {
        private List<HtmlAnswer> htmlAnswers = new List<HtmlAnswer>();

        public List<HtmlAnswer> GetAnswers()
        {
            return htmlAnswers;
        }

        public string GetJsonAnswer()
        {
            throw new NotImplementedException();
        }

        public void ParseTwiceAnswer(HtmlNode taskCard) {
            var answers = taskCard.SelectNodes(XPathBuild.XPathFromNode(taskCard, "//*[@class='answer']"));
            var rightAnswers = answers[1].SelectNodes(XPathBuild.XPathFromNode(answers[1], "//strong"));
            foreach (var answer in rightAnswers)
            {
                HtmlAnswer htmlAnswer = new HtmlAnswer();
                htmlAnswer.HtmlContentType = Enums.HtmlContentType.String;
                htmlAnswer.Content = answer.InnerText;
                htmlAnswer.IsRight = true;
                htmlAnswers.Add(htmlAnswer);
            }
        }

        public void ParseOneAnswer(HtmlNode taskCard)
        {
            var qInfo = taskCard.SelectNodes(XPathBuild.XPathFromNode(taskCard, "//*[@class='q-info']"));
            var answer = qInfo[1].SelectSingleNode(XPathBuild.XPathFromNode(qInfo[1], "//strong"));

            HtmlAnswer htmlAnswer = new HtmlAnswer();
            htmlAnswer.HtmlContentType = Enums.HtmlContentType.String;
            htmlAnswer.Content = answer.InnerText;
            htmlAnswer.IsRight = true;
            htmlAnswers.Add(htmlAnswer);

        }

        public void InitByHtmlNode(HtmlNode taskCard)
        {
            var answerTypeNode = taskCard.EndNode.SelectSingleNode(new StringBuilder(taskCard.XPath).Append("/div[@class=\"description\"]/a").ToString());

            //Получение правильных ответов
            switch (answerTypeNode.InnerText)
            {
                case "Завдання відкритої форми з короткою відповіддю (1 вид)":
                    ParseOneAnswer(taskCard);
                    break;
                case "Завдання відкритої форми з короткою відповіддю, структуроване":
                    ParseTwiceAnswer(taskCard);
                    break;
            }
        }
    }
}
