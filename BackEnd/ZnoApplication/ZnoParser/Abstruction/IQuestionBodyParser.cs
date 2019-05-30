using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Text;
using Zno.Parser.Models.Enums;

namespace Zno.Parser.Abstruction
{
    /// <summary>
    /// Интерфейс, парсинга тела вопроса
    /// </summary>
    public interface IQuestionBodyParser
    {
        string GetJsonQuestion();
        void InitByHtmlNode(HtmlNode node);

        HtmlContentType GetContentType();
    }
}
