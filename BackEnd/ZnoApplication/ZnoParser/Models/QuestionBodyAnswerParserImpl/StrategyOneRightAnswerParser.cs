using System;
using System.Collections.Generic;
using System.Text;
using HtmlAgilityPack;
using Zno.Parser.Abstruction;

namespace Zno.Parser.Models.QuestionBodyAnswerParserImpl
{
    /// <summary>
    /// Парсинг ответов в ТЕЛЕ ВОПРОСА, если они относятся к типу вопроса - Завдання з вибором однієї правильної відповіді
    /// </summary>
    public class StrategyOneRightAnswerParser : IStrategyAnswerParser
    {

        public string GetJsonAnswer()
        {
            throw new NotImplementedException();
        }

        public void InitByHtmlNode(HtmlNode node)
        {
            
        }
    }
}
