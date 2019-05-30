using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Text;
using Zno.Parser.Abstruction;

namespace Zno.Parser.Models
{
    public class AnswerParserContext
    {
        private IStrategyAnswerParser strategyAnswerParser;

        public void setStrategy(IStrategyAnswerParser strategy) {
            strategyAnswerParser = strategy;
        }

        public void executeParser(HtmlNode node) {
            strategyAnswerParser.InitByHtmlNode(node);
        }

        public string executeGetJson() {
            return strategyAnswerParser.GetJsonAnswer();
        }
    }
}
