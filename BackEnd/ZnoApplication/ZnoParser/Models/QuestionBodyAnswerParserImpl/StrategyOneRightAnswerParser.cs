using System;
using System.Collections.Generic;
using System.Text;
using HtmlAgilityPack;
using Zno.Parser.Abstruction;
using Zno.Parser.Helpers;

namespace Zno.Parser.Models.QuestionBodyAnswerParserImpl
{
    /// <summary>
    /// Парсинг ответов в ТЕЛЕ ВОПРОСА, если они относятся к типу вопроса - Завдання з вибором однієї правильної відповіді
    /// </summary>
    public class StrategyOneRightAnswerParser : IStrategyAnswerParser
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

        public void InitByHtmlNode(HtmlNode taskCard)
        {
            var answers = taskCard.EndNode.SelectSingleNode(XPathBuild.XPathFromNode(taskCard, "//*[@class=\"answers\"]"));
            if (answers != null) {
                // Перебор и получение текста или ссылки картинки вопроса
                foreach (var answer in answers.ChildNodes) {
                    var imageNode = answer.EndNode.SelectSingleNode(XPathBuild.XPathFromNode(answer, "//img"));
                    HtmlAnswer htmlAnswer = new HtmlAnswer();
                    if (imageNode != null) {
                        htmlAnswer.Content = imageNode?.Attributes["src"]?.Value;
                        htmlAnswer.HtmlContentType = Enums.HtmlContentType.Image;
                    }
                    else
                    {
                        htmlAnswer.Content = answer.InnerText.Length > 1 ? answer.InnerText.Substring(1) : "";
                        htmlAnswer.HtmlContentType = Enums.HtmlContentType.String;
                    }
                    if (htmlAnswer.Content != "") {
                        htmlAnswers.Add(htmlAnswer);
                    }
                }

                var trRightAnswers = taskCard.EndNode.SelectSingleNode(XPathBuild.XPathFromNode(taskCard, "//table[@class=\"select-answers-variants\"]//tr[2]"));
                if (trRightAnswers != null) {
                    int idx = 0;
                    foreach (var td in trRightAnswers.ChildNodes) {
                        var markerOk = td.EndNode.SelectSingleNode(XPathBuild.XPathFromNode(td, "//*[@class=\"marker ok\"]"));
                        htmlAnswers[idx].IsRight = markerOk != null;
                        idx++;
                    }
                }
            }
        }
    }
}
