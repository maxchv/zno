using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Text;
using Zno.Parser.Models;

namespace Zno.Parser.Abstruction
{
    /// <summary>
    ///  Интерфейс, парсинга блока ответов в теле вопроса, в зависимости от типа вопроса
    /// </summary>
    public interface IStrategyAnswerParser
    {
        string GetJsonAnswer();
        List<HtmlAnswer> GetAnswers();
        void InitByHtmlNode(HtmlNode taskCard);
    }
}
