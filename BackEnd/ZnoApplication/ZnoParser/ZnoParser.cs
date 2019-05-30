using HtmlAgilityPack;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Zno.DAL.Context;
using Zno.DAL.Entities;
using Zno.DAL.Interfaces;
using Zno.Parser.Models;
using Zno.Parser.Models.Enums;

namespace Zno.Parser
{
    public class ZnoParser
    {
        private HttpClient client;
        private const string END_POINT = "https://zno.osvita.ua/";
        private const string URL_TEST = "users/znotest/";
        private string connString;
        private IList<HtmlSubject> htmlSubjects = new List<HtmlSubject>();
        private ApplicationDbContext ctx;
        private IUnitOfWork unitOfWork;

        public ZnoParser(string connectionString)
        {
            this.connString = connectionString;

            DbContextOptionsBuilder optionsBuilder = new DbContextOptionsBuilder();
            var options = optionsBuilder.UseSqlServer(connString).Options;

            ctx = new ApplicationDbContext(options);

            Initialize();
        }

        public ZnoParser(ApplicationDbContext ctx)
        {
            this.ctx = ctx;
            Initialize();
        }

        public ZnoParser(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
            Initialize();
        }

        // Инициализация обьектов и добавление предметов на скачивание в список
        private async void Initialize()
        {
            client = new HttpClient();

            htmlSubjects.Add(new HtmlSubject("Українська мова і література", "ukrainian/"));
            htmlSubjects.Add(new HtmlSubject("Математика", "mathematics/"));
            htmlSubjects.Add(new HtmlSubject("Фізика", "physics/"));
            htmlSubjects.Add(new HtmlSubject("Англійська мова", "english/"));

            if (unitOfWork == null ? !ctx.QuestionTypes.Any() : unitOfWork.QuestionTypes.FindAll().Result.Count() > 0)
            {
                var oneType = new QuestionType
                {
                    Name = "C одним правильным ответом"
                };

                var manyType = new QuestionType
                {
                    Name = "C несколькими правильным ответами"
                };

                var manualType = new QuestionType
                {
                    Name = "С ручным вводом ответа"
                };

                var taskType = new QuestionType
                {
                    Name = "С ручным вводом ответа (с ручной проверкой преподавателем)"
                };
                if (unitOfWork == null)
                {
                    await ctx.QuestionTypes.AddRangeAsync(new[]
                    {
                    oneType, manyType,
                    manualType, taskType
                });

                    ctx.SaveChanges();
                }
                else
                {
                    await unitOfWork.QuestionTypes.Insert(oneType);
                    await unitOfWork.QuestionTypes.Insert(manyType);
                    await unitOfWork.QuestionTypes.Insert(manualType);
                    await unitOfWork.QuestionTypes.Insert(taskType);
                    unitOfWork.SaveChanges();
                }
            }

            if (unitOfWork == null ? !ctx.ContentTypes.Any() : unitOfWork.ContentTypes.FindAll().Result.Count() > 0)
            {
                var jsonType = new ContentType
                {
                    Name = "Json"
                };

                var imageType = new ContentType
                {
                    Name = "Image"
                };

                var textType = new ContentType
                {
                    Name = "Text"
                };

                var videoType = new ContentType
                {
                    Name = "Video"
                };

                if (unitOfWork == null)
                {
                    await ctx.ContentTypes.AddRangeAsync(new[]
                    {
                    jsonType, imageType,
                    textType, videoType
                });

                    ctx.SaveChanges();
                }
                else
                {
                    await unitOfWork.ContentTypes.Insert(jsonType);
                    await unitOfWork.ContentTypes.Insert(imageType);
                    await unitOfWork.ContentTypes.Insert(textType);
                    await unitOfWork.ContentTypes.Insert(videoType);
                    await unitOfWork.SaveChanges();
                }
            }

        }

        // Метод, для добавления предметов в бд 
        private async void _DbAddSubjects(Subject subjectEF)
        {
            if (unitOfWork == null)
            {
                ctx.Subjects.Add(subjectEF);
                ctx.SaveChanges();
            }
            else
            {
                await unitOfWork.Subjects.Insert(subjectEF);
                await unitOfWork.SaveChanges();
            }
        }

        // Метод, для добавления типа тестов в бд 
        private async void _DbAddTestTypes(TestType testType)
        {
            if (unitOfWork == null)
            {
                ctx.TestTypes.Add(testType);
                ctx.SaveChanges();
            }
            else
            {
                await unitOfWork.TestTypes.Insert(testType);
                await unitOfWork.SaveChanges();
            }
        }

        private ContentType _GetContentType(HtmlContentType htmlType)
        {
            ContentType contentType = null;
            int id = -1;
            switch (htmlType)
            {
                case HtmlContentType.String:
                    id = 3; // Text
                    break;
                case HtmlContentType.Image:
                case HtmlContentType.Video:
                    id = 1; // Json
                    break;
                case HtmlContentType.None:
                default:
                    id = -1;
                    break;
            }

            if (id > 0)
            {
                contentType = unitOfWork == null ? ctx.ContentTypes.FirstOrDefault((q) => q.Id == id) : unitOfWork.ContentTypes.FindById(id).Result;
            }

            return contentType;
        }
        private QuestionType _GetQuestionType(HtmlQuestionType htmlType)
        {

            QuestionType questionType = null;
            int id = -1;
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
            if (id > 0)
            {
                questionType = unitOfWork == null ? ctx.QuestionTypes.FirstOrDefault((q) => q.Id == id) : unitOfWork.QuestionTypes.FindById(id).Result;
            }

            return questionType;
        }

        // Метод, для добавления тестов в бд 
        private async void _DbAddTest(Test test, IList<HtmlQuestion> questions)
        {
            if (unitOfWork == null)
            {
                ctx.Tests.Add(test);
                ctx.SaveChanges();
            }
            else
            {
                await unitOfWork.Tests.Insert(test);
                await unitOfWork.SaveChanges();
            }

            foreach (var question in questions)
            {
                try
                {
                    Question questionEF = new Question();
                    questionEF.Test = test;
                    questionEF.Content = question.QuestionBody.GetJsonQuestion();
                    questionEF.QuestionType = _GetQuestionType(question.QuestionType);
                    questionEF.ContentType = _GetContentType(question.QuestionBody.GetContentType());

                    if (unitOfWork == null)
                    {
                        ctx.Questions.Add(questionEF);
                        ctx.SaveChanges();
                    }
                    else
                    {
                        await unitOfWork.Questions.Insert(questionEF);
                        await unitOfWork.SaveChanges();
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }


        }

        // Стартовый метод работы библиотеки
        public async void StartParsing()
        {
            Console.WriteLine("Start parsing");
            // проверяется заполнены ли таблицы в бд. Если там есть хотя бы одна запись, 
            // добавление осуществляться не будет
            bool foundSubjects = unitOfWork == null ? ctx.Subjects.Any() : false,
                foundTests = unitOfWork == null ? ctx.Tests.Any() : false,
                foundTestTypes = unitOfWork == null ? ctx.TestTypes.Any() : false;

            

            Subject subjectEF = null;
            // Перебор все предметов. Внутренние обьекты (тесты, предметы, вопросы и т.д.) инициализируются в процессе работы библиотеки,
            // вызывая метод интерфейса IHtmlParser.InitByHtmlNode()
            foreach (var subject in GetSubjects())
            {
                subjectEF = new Subject();
                subjectEF.Name = subject.Name;
                if (!foundSubjects)
                {
                    _DbAddSubjects(subjectEF);
                    Console.WriteLine("Add subject " + subjectEF.Name);
                }

                // Перебор тестов
                foreach (var test in subject.Tests)
                {
                    if (!ctx.TestTypes.Any(t => t.Name.Equals(test.Type)))
                    {
                        TestType testType = new TestType();
                        testType.Name = test.Type;
                        if (!foundTestTypes)
                        {
                            _DbAddTestTypes(testType);
                            Console.WriteLine("Add test type " + testType.Name);
                        }
                    }

                    try
                    {
                        Test testEF = new Test();
                        testEF.Subject = ctx.Subjects.FirstOrDefault((s) => s.Name == test.Subject);
                        testEF.Type = ctx.TestTypes.FirstOrDefault((t) => t.Name == test.Type);
                        testEF.Year = test.Year;


                        if (!foundTests)
                        {
                            _DbAddTest(testEF, test.HtmlQuestions);
                            Console.WriteLine("Add test  " + testEF.Id);
                        }
                    }
                    catch (Exception)
                    {

                    }

                }
            }
            Console.WriteLine("Close parse");

        }

        // Добавление предметов вне библиотеки
        public void AddSubject(HtmlSubject htmlSubject)
        {
            htmlSubjects.Add(htmlSubject);
        }

        // Получение всех распарсинних предметов и тестов
        public IList<HtmlSubject> GetSubjects()
        {

            foreach (var subject in htmlSubjects)
            {
                subject.Tests = GetTestsBySubject(subject);
            }

            return htmlSubjects;
        }

        // Получение тестов по заданному предмету
        public IList<HtmlTest> GetTestsBySubject(HtmlSubject subject)
        {
            IList<HtmlTest> tests = new List<HtmlTest>();
            var result = Task.Run(() =>
            {
                return client.GetAsync(new StringBuilder(END_POINT).Append(subject.Url).ToString());
            }).Result;

            var html = new HtmlDocument();
            html.LoadHtml(result.Content.ReadAsStringAsync().Result);
            var collectionNodes = html.DocumentNode.SelectNodes("//*[@class=\"test-item\"]/a");

            var testNum = 1;

            foreach (var node in collectionNodes)
            {
                try
                {
                    HtmlTest test = new HtmlTest();
                    test.Url = new StringBuilder(END_POINT).Append(URL_TEST).ToString();
                    test.InitByHtmlNode(node);
                    test.Subject = subject.Name;
                    test.InitTest();
                    tests.Add(test);

                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    Console.WriteLine();
                }
                Console.WriteLine(testNum++);
            }


            return tests;

        }
    }
}
