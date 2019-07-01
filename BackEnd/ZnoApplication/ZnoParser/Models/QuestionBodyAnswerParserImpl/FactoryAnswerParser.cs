using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Text;
using Zno.Parser.Abstruction;
using Zno.Parser.Models.Enums;

namespace Zno.Parser.Models.QuestionBodyAnswerParserImpl
{
    public class FactoryAnswerParser
    {
        private HtmlQuestionType questionType;
        public HtmlContentType QuestionContentType { get; set; }
            
        public HtmlQuestionType QuestionType
        {
            get { return questionType; }
            set {
                questionType = value;
                switch (questionType)
                {
                    case HtmlQuestionType.One:
                        StrategyAnswerParser = new StrategyOneRightAnswerParser();
                        break;
                    case HtmlQuestionType.Many:
                        StrategyAnswerParser = new StrategyManyRightAnswerParser();
                        break;
                    case HtmlQuestionType.Manual:
                        StrategyAnswerParser = new StrategyManualRightAnswerParser();
                        break;
                    case HtmlQuestionType.Task:
                    default:
                        StrategyAnswerParser = new StrategyTaskRightAnswerParser();
                        break;
                }
            }
        }

        public IStrategyAnswerParser StrategyAnswerParser { get; private set;}

        public FactoryAnswerParser()
        {

        }

    }
}
