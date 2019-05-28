using HtmlAgilityPack;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Zno.DAL.Context;
using Zno.DAL.Entities;
using Zno.DAL.Implementation;
using Zno.DAL.Interfaces;
using Zno.Parser.Models;

namespace Zno.Parser
{
    public class ZnoParser
    {
        private const String END_POINT = "https://zno.osvita.ua/";
        private const string URL_TEST = "users/znotest/";
        private HttpClient client;
        private IList<HtmlSubject> htmlSubjects = new List<HtmlSubject>();
        private IUnitOfWork _unitOfWork;

        public ZnoParser(string connectionString)
        {
            DbContextOptionsBuilder optionsBuilder = new DbContextOptionsBuilder();
            var options = optionsBuilder.UseSqlServer(connectionString).Options;

            var context = new ApplicationDbContext(options);

            _unitOfWork = new MySqlUnitOfWork(context);
            Initialize();
        }

        public ZnoParser(ApplicationDbContext context)
        {
            _unitOfWork = new MySqlUnitOfWork(context);
            Initialize();
        }

        public ZnoParser(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
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

        public async Task StartParsing()
        {
            var subjects = await _unitOfWork.Subjects.FindAll();
            if (subjects.Count() <= 0)
            {
                _unitOfWork.BeginTransaction();

                try
                {
                    foreach (var currentSubject in GetSubjects())
                    {
                        var newSubject = new Subject();
                        newSubject.Name = currentSubject.Name;
                        await _unitOfWork.Subjects.Insert(newSubject);

                        await _unitOfWork.SaveChanges();

                        foreach (var test in currentSubject.Tests)
                        {
                            var searchTypes = await _unitOfWork.TestTypes.Find(t => t.Name.Equals(test.Type));

                            if (searchTypes is null || searchTypes.Count() <= 0)
                            {
                                var newTestType = new TestType();
                                newTestType.Name = test.Type;
                                await _unitOfWork.TestTypes.Insert(newTestType);
                                await _unitOfWork.SaveChanges();
                            }

                            try
                            {
                                var newTest = new Test();
                                newTest.Subject = (await _unitOfWork.Subjects.Find(s => s.Name == test.Subject)).FirstOrDefault();
                                newTest.Type = (await _unitOfWork.TestTypes.Find(t => t.Name == test.Type)).FirstOrDefault();
                                newTest.Year = test.Year;

                                await _unitOfWork.Tests.Insert(newTest);
                                await _unitOfWork.SaveChanges();
                            }
                            catch (Exception ex)
                            {
                                Debug.WriteLine(ex.Message);
                            }
                        }
                    }

                    await _unitOfWork.SaveChanges();
                    _unitOfWork.Commit();
                }
                catch (Exception ex)
                {
                    Debug.WriteLine(ex.Message);
                    _unitOfWork.Rollback();
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
