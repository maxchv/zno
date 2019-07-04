using System;
using System.Collections.Generic;
using System.Text;
using HtmlAgilityPack;
using Newtonsoft.Json;
using Zno.Parser.Abstruction;
using Zno.Parser.Helpers;

namespace Zno.Parser.Models.QuestionBodyAnswerParserImpl
{
    public class StrategyManyRightAnswerParser : IStrategyAnswerParser
    {
        class JsonAnswer
        {
            public string FirstBlock { get; set; }
            public string TwiceBlock { get; set; }
            public void SetFirstBlock(string content) {
                FirstBlock = content.Length > 1 ? content.Substring(1) : "";
            }

            public void SetTwiceBlock(string content)
            {
                TwiceBlock = content.Length > 1 ? content.Substring(1) : "";
            }
        }

        private List<HtmlAnswer> htmlAnswers = new List<HtmlAnswer>();

        public List<HtmlAnswer> GetAnswers()
        {
            return htmlAnswers;
        }

        public string GetJsonAnswer()
        {
            throw new NotImplementedException();
        }

        private HtmlAnswer GenerateAnswer(string contentOne, string contentTwo, bool isRight)
        {
            HtmlAnswer htmlAnswer = new HtmlAnswer();
            JsonAnswer jsonAnswer = new JsonAnswer();
            jsonAnswer.SetFirstBlock(contentOne);
            jsonAnswer.SetTwiceBlock(contentTwo);
            htmlAnswer.Content = JsonConvert.SerializeObject(jsonAnswer);
            htmlAnswer.IsRight = isRight;
            htmlAnswer.HtmlContentType = Enums.HtmlContentType.Json;

            return htmlAnswer;
        }

        private void SetAnswersNoneBlock(HtmlNode taskCard)
        {
            
            var selectAnswersVariants = taskCard.EndNode.SelectSingleNode(XPathBuild.XPathFromNode(taskCard, "//table[@class=\"select-answers-variants\"]"));

            if (selectAnswersVariants != null)
            {
                List<int> listAddsIndexes = new List<int>();

                var idxAnswer = 0;

                // Получение отдельной строки с ответами
                foreach (var tr in selectAnswersVariants.ChildNodes)
                {
                    var tdArray = tr.Name != "#text" ? tr.SelectNodes(XPathBuild.XPathFromNode(tr, "//td")) : null;
                    if (tdArray != null && tdArray.Count > 0)
                    {
                        var idxTd = 0;

                        // Получение каждого вопроса в строке
                        foreach (var td in tdArray)
                        {
                            var markerOk = td.EndNode.SelectSingleNode(XPathBuild.XPathFromNode(td, "//*[@class=\"marker ok\"]"));

                            if (markerOk != null)
                            {
                                HtmlAnswer htmlAnswer = GenerateAnswer("1" + (idxAnswer + 1), "1" + (idxTd + 1), true);
                                htmlAnswers.Add(htmlAnswer);
                                listAddsIndexes.Add(idxTd);
                                idxAnswer++;
                                break;
                            }
                            idxTd++;
                        }
                    }
                }
            }

        }

        /// <summary>
        /// Заполение массива ответов, если блок answers один
        /// </summary>
        private void SetAnswersOneBlock(HtmlNode taskCard) {
            var answers = taskCard.EndNode.SelectNodes(XPathBuild.XPathFromNode(taskCard, "//*[@class=\"answers\"]"));
            var selectAnswersVariants = taskCard.EndNode.SelectSingleNode(XPathBuild.XPathFromNode(taskCard, "//table[@class=\"select-answers-variants\"]"));
            HtmlNodeCollection answerBlock = null;
            if(answers != null)
            {
                if(answers.Count >= 2)
                {
                    answerBlock = answers[1].SelectNodes(XPathBuild.XPathFromNode(answers[1], "//*[@class='answer']"));
                }
                else
                {
                    answerBlock = answers[0].SelectNodes(XPathBuild.XPathFromNode(answers[0], "//*[@class='answer']"));
                }
            }

            if (selectAnswersVariants != null && answerBlock != null)
            {
                List<int> listAddsIndexes = new List<int>();

                var idxAnswer = 0;

                // Получение отдельной строки с ответами
                foreach (var tr in selectAnswersVariants.ChildNodes)
                {
                    var tdArray = tr.Name != "#text" ? tr.SelectNodes(XPathBuild.XPathFromNode(tr, "//td")) : null;
                    if (tdArray != null && tdArray.Count > 0)
                    {
                        var idxTd = 0;

                        // Получение каждого вопроса в строке
                        foreach (var td in tdArray)
                        {
                            var markerOk = td.EndNode.SelectSingleNode(XPathBuild.XPathFromNode(td, "//*[@class=\"marker ok\"]"));

                            if (markerOk != null)
                            {
                                HtmlAnswer htmlAnswer = GenerateAnswer("1" + (idxAnswer + 1), answerBlock[idxTd].InnerText, true);
                                htmlAnswers.Add(htmlAnswer);
                                listAddsIndexes.Add(idxTd);
                                idxAnswer++;
                                break;
                            }
                            idxTd++;
                        }
                    }
                }
                int idxAnswerTwo = 0;
                foreach (var answer in answerBlock)
                {
                    if (!listAddsIndexes.Contains(idxAnswerTwo))
                    {
                        HtmlAnswer htmlAnswer = GenerateAnswer("", answer.InnerText, false);
                        htmlAnswers.Add(htmlAnswer);
                    }
                    idxAnswerTwo++;
                }
            }

        }

        /// <summary>
        /// Заполение массива ответов, если блока answers два
        /// </summary>
        private void SetAnswersTwiceBlock(HtmlNode taskCard)
        {
            var answers = taskCard.EndNode.SelectNodes(XPathBuild.XPathFromNode(taskCard, "//*[@class=\"answers\"]"));
            var selectAnswersVariants = taskCard.EndNode.SelectSingleNode(XPathBuild.XPathFromNode(taskCard, "//table[@class=\"select-answers-variants\"]"));

            var answersOne = answers[0].SelectNodes(XPathBuild.XPathFromNode(answers[0], "//*[@class='answer']"));
            var answersTwo = answers[1].SelectNodes(XPathBuild.XPathFromNode(answers[1], "//*[@class='answer']"));

            if (selectAnswersVariants != null)
            {
                List<int> listAddsIndexes = new List<int>();
                bool addAnswer = false;
                // Получение блока логических вопросов с номерами (от 1 до 4)
                //foreach (var answerOne in answersOne)
                //{
                    if (!addAnswer)
                    {
                    int idxTr = 0;
                    // Получение отдельной строки с ответами
                    foreach (var tr in selectAnswersVariants.ChildNodes)
                    {
                        var tdArray = tr.Name != "#text" ? tr.SelectNodes(XPathBuild.XPathFromNode(tr, "//td")) : null;
                        if (tdArray != null && tdArray.Count > 0)
                        {
                            var idxTd = 0;

                            // Получение каждого вопроса в строке
                            foreach (var td in tdArray)
                            {
                                var markerOk = td.EndNode.SelectSingleNode(XPathBuild.XPathFromNode(td, "//*[@class=\"marker ok\"]"));

                                if (markerOk != null)
                                {
                                    HtmlAnswer htmlAnswer = GenerateAnswer(answersOne[idxTr].InnerText, answersTwo[idxTd].InnerText, true);
                                    htmlAnswers.Add(htmlAnswer);
                                    listAddsIndexes.Add(idxTd);
                                    addAnswer = true;
                                    idxTr++;
                                    break;
                                }
                                idxTd++;
                            }
                        }
                    }
                }
                //}

                int idxAnswerTwo = 0;
                foreach(var answerTwo in answersTwo)
                {
                    if (!listAddsIndexes.Contains(idxAnswerTwo))
                    {
                        HtmlAnswer htmlAnswer = GenerateAnswer("", answerTwo.InnerText, false);
                        htmlAnswers.Add(htmlAnswer);
                    }
                    idxAnswerTwo++;
                }
            }
        }

        public void InitByHtmlNode(HtmlNode taskCard)
        {
            var answers = taskCard.EndNode.SelectNodes(XPathBuild.XPathFromNode(taskCard, "//*[@class=\"answers\"]"));
            if (answers != null) {
                if (answers.Count == 1)
                    SetAnswersOneBlock(taskCard);
                else if (answers.Count >= 2) {
                    if (answers[0].ChildNodes != null && answers[0].ChildNodes.Count > 2)
                    {
                        SetAnswersTwiceBlock(taskCard);
                    }
                    else
                    {
                        if (answers[1].ChildNodes != null && answers[1].ChildNodes.Count > 2)
                        {
                            SetAnswersOneBlock(taskCard);
                        }
                        else
                        {
                            SetAnswersNoneBlock(taskCard);
                        }
                    }
                }
            }
        }
    }
}

