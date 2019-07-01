using System;
using System.Collections.Generic;
using System.Text;
using HtmlAgilityPack;
using Zno.Parser.Abstruction;
using Zno.Parser.Helpers;

namespace Zno.Parser.Models.QuestionBodyAnswerParserImpl
{
    public class StrategyManyRightAnswerParser : IStrategyAnswerParser
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
            var answers = taskCard.EndNode.SelectNodes(XPathBuild.XPathFromNode(taskCard, "//*[@class=\"answers\"]"));
            if (answers != null && answers?.Count == 2) {

            }
        }
    }
}
