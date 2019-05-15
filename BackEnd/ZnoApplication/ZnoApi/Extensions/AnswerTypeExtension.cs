using System.Collections.Generic;
using ZnoModelLibrary.Entities;

namespace ZnoApi.Controllers
{
    public static class AnswerTypeExtension
    {
        public static DetailsAnswerType GetDetails(this AnswerType answerType)
        {
            string name = null;

            switch (answerType)
            {
                case AnswerType.One:
                    name = "Вопрос с одним правильным ответом";
                    break;
                case AnswerType.Many:
                    name = "Вопрос с несколькими правильным ответами";
                    break;
                case AnswerType.Manual:
                    name = "Вопрос ответ на который необходимо высчитать и вручную вписать (проверяется системой)";
                    break;
                case AnswerType.Task:
                    name = "Вопрос ответ на который необходимо высчитать и вручную вписать (проверяется преподавателем)";
                    break;
            }

            DetailsAnswerType details = new DetailsAnswerType
            {
                Name = name,
                Value = (int)answerType
            };

            return details;
        }
    }
}