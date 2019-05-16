using HtmlAgilityPack;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using ZnoModelLibrary.EF;
using ZnoModelLibrary.Entities;
using ZnoParser.Models;

namespace ZnoParser
{
    public class ZnoParser
    {
        private HttpClient client;
        private const String END_POINT = "https://zno.osvita.ua/";
        private const string URL_TEST = "users/znotest/";
        private string connString;
        private IList<HtmlSubject> htmlSubjects = new List<HtmlSubject>();
        private ApplicationContext ctx;


        public ZnoParser(string connectionString)
        {
            this.connString = connectionString;

            DbContextOptionsBuilder optionsBuilder = new DbContextOptionsBuilder();
            var options = optionsBuilder.UseSqlServer(connString).Options;

            ctx = new ApplicationContext(options);

            Initialize();
        }

        public ZnoParser(ApplicationContext ctx)
        {
            this.ctx = ctx;
            Initialize();
        }

        private void Initialize()
        {
            client = new HttpClient();

            htmlSubjects.Add(new HtmlSubject("Українська мова і література", "ukrainian/"));
            htmlSubjects.Add(new HtmlSubject("Математика", "mathematics/"));
            htmlSubjects.Add(new HtmlSubject("Фізика", "physics/"));
            htmlSubjects.Add(new HtmlSubject("Англійська мова", "english/"));
        }

        public void StartParsing()
        {

            if (!ctx.Subjects.Any())
            {
                Subject subjectEF = null;
                foreach (var subject in GetSubjects())
                {
                    subjectEF = new Subject();
                    subjectEF.Name = subject.Name;
                    ctx.Subjects.Add(subjectEF);

                    ctx.SaveChanges();

                    foreach (var test in subject.Tests)
                    {
                        if (ctx.TestTypes.FirstOrDefault(t => t.Name.Equals(test.Type)) == null)
                        {
                            TestType testType = new TestType();
                            testType.Name = test.Type;
                            ctx.TestTypes.Add(testType);
                            ctx.SaveChanges();
                        }

                        try
                        {
                            Test testEF = new Test();
                            testEF.Subject = ctx.Subjects.FirstOrDefault((s) => s.Name == test.Subject);
                            testEF.Type = ctx.TestTypes.FirstOrDefault((t) => t.Name == test.Type);
                            testEF.Year = test.Year;
                            ctx.Tests.Add(testEF);
                            ctx.SaveChanges();
                        }
                        catch (Exception ex)
                        {

                        }

                    }
                }
            }

        }

        public void AddSubject(HtmlSubject htmlSubject)
        {
            htmlSubjects.Add(htmlSubject);
        }

        public IList<HtmlSubject> GetSubjects()
        {

            foreach (var subject in htmlSubjects)
            {
                subject.Tests = GetTestsBySubject(subject);
            }

            return htmlSubjects;
        }

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

                    Subject subjectEF = null;
                    //subjectEF = ctx.Subjects.FirstOrDefault((s) => s.Name == subject.Name);

                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    Console.WriteLine();
                }
            }


            return tests;

        }
    }
}
