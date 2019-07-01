using System;
using System.Collections.Generic;
using System.Text;
using HtmlAgilityPack;
using Zno.Parser.Abstruction;

namespace Zno.Parser.Models.QuestionBodyAnswerParserImpl
{
    public class StrategyManualRightAnswerParser : IStrategyAnswerParser
    {
        public List<HtmlAnswer> GetAnswers()
        {
            throw new NotImplementedException();
        }

        public string GetJsonAnswer()
        {
            throw new NotImplementedException();
        }

        public void InitByHtmlNode(HtmlNode node)
        {
            throw new NotImplementedException();
        }
    }
}
