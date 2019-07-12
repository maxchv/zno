using System;
using System.Collections.Generic;
using System.Text;
using HtmlAgilityPack;
using Zno.Parser.Abstruction;

namespace Zno.Parser.Models.QuestionBodyAnswerParserImpl
{
    public class StrategyTaskRightAnswerParser : IStrategyAnswerParser
    {
        public List<HtmlAnswer> GetAnswers()
        {
            return new List<HtmlAnswer>();
        }

        public string GetJsonAnswer()
        {
            return null;
        }

        public void InitByHtmlNode(HtmlNode node)
        {
            
        }
    }
}
