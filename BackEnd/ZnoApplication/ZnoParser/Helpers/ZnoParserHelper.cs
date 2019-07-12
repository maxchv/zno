using System;
using System.Collections.Generic;
using System.Text;
using Zno.DAL.Entities;
using Zno.Parser.Models.Enums;

namespace Zno.Parser.Helpers
{
    public class ZnoParserHelper
    {
        public static void GetContentTypeId(HtmlContentType htmlType, out ContentType contentType, out int id)
        {
            contentType = null;
            id = -1;
            switch (htmlType)
            {
                case HtmlContentType.String:
                    id = 3; // Text
                    break;
                case HtmlContentType.Image:
                case HtmlContentType.Video:
                case HtmlContentType.Json:
                    id = 1; // Json
                    break;
                case HtmlContentType.None:
                default:
                    id = -1;
                    break;
            }
        }

        public static void GetQuestionTypeId(HtmlQuestionType htmlType, out QuestionType questionType, out int id)
        {
            questionType = null;
            id = -1;
            switch (htmlType)
            {
                case HtmlQuestionType.One:
                    id = 1;
                    break;
                case HtmlQuestionType.Many:
                    id = 2;
                    break;
                case HtmlQuestionType.Manual:
                    id = 3;
                    break;
                case HtmlQuestionType.Task:
                    id = 4;
                    break;
                default:
                    id = -1;
                    break;
            }
        }
    }
}
