using HtmlAgilityPack;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
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
        private string connString;
        private IList<HtmlSubject> htmlSubjects = new List<HtmlSubject>();


        public ZnoParser(string connectionString) {
            this.connString = connectionString;
            client = new HttpClient();

            htmlSubjects.Add(new HtmlSubject("Українська мова і література", "ukrainian/"));
            htmlSubjects.Add(new HtmlSubject("Математика", "mathematics/"));
            htmlSubjects.Add(new HtmlSubject("Фізика", "physics/"));
            htmlSubjects.Add(new HtmlSubject("Англійська мова", "english/"));

            /*DbContextOptionsBuilder optionsBuilder = new DbContextOptionsBuilder();
            optionsBuilder = SqlServerDbContextOptionsExtensions.UseSqlServer(optionsBuilder, connString);

            using (var ctx = new ApplicationContext(optionsBuilder.Options))
            {

            }*/
        }

        public IList<HtmlSubject> GetSubjects() {

            foreach (var subject in htmlSubjects)
            {
                subject.Tests = GetTestsBySubject(subject);
            }

            return htmlSubjects;
        }

        public IList<HtmlTest> GetTestsBySubject(HtmlSubject subject) {
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
                    test.InitByHtmlNode(node);
                    test.Subject = subject.Name;
                    tests.Add(test);
                }
                catch (Exception ex) {
                }
            }

            return tests;
            
        }
    }
}
