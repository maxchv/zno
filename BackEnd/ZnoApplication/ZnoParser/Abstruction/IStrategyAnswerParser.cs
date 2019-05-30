using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Text;

namespace Zno.Parser.Abstruction
{
    /// <summary>
    ///  Интерфейс, парсинга блока ответов в теле вопроса, в зависимости от типа вопроса
    /// </summary>
    public interface IStrategyAnswerParser
    {
        string GetJsonAnswer();
        void InitByHtmlNode(HtmlNode node);
    }
}
