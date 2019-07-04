using System;
using System.Collections.Generic;
using System.Text;
using HtmlAgilityPack;
using Zno.Parser.Abstruction;

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

        public void InitByHtmlNode(HtmlNode node)
        {
            
        }
    }
}
