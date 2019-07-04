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
using Zno.Parser.Abstruction;
using Zno.Parser.Helpers;
using Zno.Parser.Models;
using Zno.Parser.Models.Enums;
using Zno.Parser.Models.RepositoryParser;

namespace Zno.Parser
{
    public class ZnoParser
    {
        private HttpClient client;
        private const string END_POINT = "https://zno.osvita.ua/";
        private const string URL_TEST = "users/znotest/";
        //private string connString;
        private IList<HtmlSubject> htmlSubjects = new List<HtmlSubject>();
        //private ApplicationDbContext ctx;
        //private IUnitOfWork unitOfWork;
        private IRepositoryParser repositoryParser;
        private ISerializeParser serializeParser = null;

        public ZnoParser(string connectionString, bool isAsync = false)
        {
            repositoryParser = new ContextRepositoryParser(connectionString);

            if (isAsync)
                InitializeAsync();
            else
                Initialize();
        }

        public ZnoParser(ApplicationDbContext ctx, bool isAsync = false)
        {
            repositoryParser = new ContextRepositoryParser(ctx);

            if (isAsync)
            {
                InitializeAsync();
            }
            else
            {
                Initialize();
            }
        }

        public ZnoParser(IUnitOfWork unitOfWork, bool isAsync = false)
        {
            repositoryParser = new UnitOfWorkRepositoryParser(unitOfWork);

            if (isAsync)
            {
                InitializeAsync();
            }
            else
            {
                Initialize();
            }
        }

        public ISerializeParser SerializeParser { get { return serializeParser; } set { serializeParser = value; } }

        private void BaseInitialize()
        {
            client = new HttpClient();

            htmlSubjects.Add(new HtmlSubject("Українська мова і література", "ukrainian/"));
            htmlSubjects.Add(new HtmlSubject("Математика", "mathematics/"));
            htmlSubjects.Add(new HtmlSubject("Фізика", "physics/"));
            htmlSubjects.Add(new HtmlSubject("Англійська мова", "english/"));
        }

        // Инициализация обьектов и добавление предметов на скачивание в список

        private void Initialize()
        {
            BaseInitialize();

            if (!repositoryParser.QuestionTypesAny())
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
                repositoryParser.DbAddQuestionType(oneType);
                repositoryParser.DbAddQuestionType(manyType);
                repositoryParser.DbAddQuestionType(manualType);
                repositoryParser.DbAddQuestionType(taskType);
            }

            if (!repositoryParser.ContentTypesAny())
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

                repositoryParser.DbAddContentType(jsonType);
                repositoryParser.DbAddContentType(imageType);
                repositoryParser.DbAddContentType(textType);
                repositoryParser.DbAddContentType(videoType);
            }

        }

        private async void InitializeAsync()
        {
            BaseInitialize();

            if (!await repositoryParser.QuestionTypesAnyAsync())
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
                await repositoryParser.DbAddQuestionTypeAsync(oneType);
                await repositoryParser.DbAddQuestionTypeAsync(manyType);
                await repositoryParser.DbAddQuestionTypeAsync(manualType);
                await repositoryParser.DbAddQuestionTypeAsync(taskType);
            }

            if (!await repositoryParser.ContentTypesAnyAsync())
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

                await repositoryParser.DbAddContentTypeAsync(jsonType);
                await repositoryParser.DbAddContentTypeAsync(imageType);
                await repositoryParser.DbAddContentTypeAsync(textType);
                await repositoryParser.DbAddContentTypeAsync(videoType);
            }

        }

        // Стартовый асинхронный метод работы библиотеки
        public async Task StartParsingAsync()
        {

            Console.WriteLine("Start parsing");
            // проверяется заполнены ли таблицы в бд. Если там есть хотя бы одна запись, 
            // добавление осуществляться не будет
            bool foundSubjects = await repositoryParser.SubjectsAnyAsync(),
                foundTests = await repositoryParser.TestsAnyAsync(),
                foundTestTypes = await repositoryParser.TestTypesAnyAsync();

            Subject subjectEF = null;
            // Перебор все предметов. Внутренние обьекты (тесты, предметы, вопросы и т.д.) инициализируются в процессе работы библиотеки,
            // вызывая метод интерфейса IHtmlParser.InitByHtmlNode()
            foreach (var subject in await GetSubjectsAsync())
            {
                subjectEF = new Subject();
                subjectEF.Name = subject.Name;
                if (!foundSubjects)
                {
                    await repositoryParser.DbAddSubjectsAsync(subjectEF);
                    Console.WriteLine("Add subject " + subjectEF.Name);
                }

                // Перебор тестов
                foreach (var test in subject.Tests)
                {
                    TestType testType = new TestType();
                    testType.Name = test.Type;
                    if (!foundTestTypes && (await repositoryParser.FindTestTypeByNameAsync(testType.Name)) == null)
                    {
                        await repositoryParser.DbAddTestTypesAsync(testType);
                        Console.WriteLine("Add test type " + testType.Name);
                    }

                    try
                    {
                        Test testEF = new Test();

                        testEF.Subject = await repositoryParser.FindSubjectByNameAsync(test.Subject);
                        testEF.Type = await repositoryParser.FindTestTypeByNameAsync(test.Type);
                        testEF.Year = test.Year;

                        if (!foundTests)
                        {
                            await repositoryParser.DbAddTestAsync(testEF, test.HtmlQuestions);
                            Console.WriteLine("Add test  " + testEF.Id);
                        }
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex);
                    }
                }
            }
            Console.WriteLine("Close parse");

        }

        // Стартовый метод работы библиотеки
        public void StartParsing()
        {

            Console.WriteLine("Start parsing");
            // проверяется заполнены ли таблицы в бд. Если там есть хотя бы одна запись, 
            // добавление осуществляться не будет
            bool foundSubjects = repositoryParser.SubjectsAny();
            bool foundTests = repositoryParser.TestsAny();
            bool foundTestTypes = repositoryParser.TestTypesAny();

            Subject subjectEF = null;
            // Перебор все предметов. Внутренние обьекты (тесты, предметы, вопросы и т.д.) инициализируются в процессе работы библиотеки,
            // вызывая метод интерфейса IHtmlParser.InitByHtmlNode()
            Console.WriteLine("GetSubjectsAsync().Result");
            foreach (var subject in GetSubjects())
            {
                subjectEF = new Subject();
                subjectEF.Name = subject.Name;
                if (!foundSubjects)
                {
                    repositoryParser.DbAddSubjects(subjectEF);
                    Console.WriteLine("Add subject " + subjectEF.Name);
                }

                // Перебор тестов
                foreach (var test in subject.Tests)
                {
                    TestType testType = new TestType();
                    testType.Name = test.Type;
                    if (!foundTestTypes && repositoryParser.FindTestTypeByName(testType.Name) == null)
                    {
                        repositoryParser.DbAddTestTypes(testType);
                        Console.WriteLine("Add test type " + testType.Name);
                    }

                    try
                    {
                        Test testEF = new Test();

                        testEF.Subject = repositoryParser.FindSubjectByName(test.Subject);
                        testEF.Type = repositoryParser.FindTestTypeByName(test.Type);
                        testEF.Year = test.Year;

                        if (!foundTests)
                        {
                            repositoryParser.DbAddTest(testEF, test.HtmlQuestions);
                            Console.WriteLine("Add test  " + testEF.Id);
                        }
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex);
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
        public async Task<IList<HtmlSubject>> GetSubjectsAsync()
        {
            Console.WriteLine("GetSubjectsAsync");
            foreach (var subject in htmlSubjects)
            {
                subject.AddTests(await GetTestsBySubjectAsync(subject));
            }

            return htmlSubjects;
        }

        public IList<HtmlSubject> GetSubjects()
        {
            Console.WriteLine("GetSubjectsAsync");
            var subjects = SerializeParser.Deserialize();
            if (subjects == null)
            {
                foreach (var subject in htmlSubjects)
                {
                    subject.AddTests(GetTestsBySubject(subject));
                }
                Console.WriteLine("Serialize subjects");
                SerializeParser.Serialize(htmlSubjects.ToArray());
                Console.WriteLine("Serialize completed");
            }
            else
            {
                Console.WriteLine("Get by cashe");
                htmlSubjects = subjects.ToList();
            }

            return htmlSubjects;
        }



        // Асинхронное получение тестов по заданному предмету
        public async Task<IList<HtmlTest>> GetTestsBySubjectAsync(HtmlSubject subject)
        {
            IList<HtmlTest> tests = new List<HtmlTest>();
            var result = await client.GetAsync(new StringBuilder(END_POINT).Append(subject.Url).ToString());

            var html = new HtmlDocument();
            html.LoadHtml(await result.Content.ReadAsStringAsync());
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

        public IList<HtmlTest> GetTestsBySubject(HtmlSubject subject)
        {
            IList<HtmlTest> tests = new List<HtmlTest>();
            var result = client.GetAsync(new StringBuilder(END_POINT).Append(subject.Url).ToString()).Result;

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
