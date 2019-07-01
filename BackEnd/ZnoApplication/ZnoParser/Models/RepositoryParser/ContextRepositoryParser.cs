using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Zno.DAL.Context;
using Zno.DAL.Entities;
using Zno.Parser.Abstruction;
using Zno.Parser.Helpers;
using Zno.Parser.Models.Enums;

namespace Zno.Parser.Models.RepositoryParser
{
    public class ContextRepositoryParser : IRepositoryParser
    {
        private ApplicationDbContext ctx;

        public ContextRepositoryParser(ApplicationDbContext dbContext)
        {
            ctx = dbContext;
        }

        public ContextRepositoryParser(string connectionString)
        {
            DbContextOptionsBuilder optionsBuilder = new DbContextOptionsBuilder();
            var options = optionsBuilder.UseSqlServer(connectionString).Options;

            ctx = new ApplicationDbContext(options);
        }

        public bool ContentTypesAny()
        {
            bool res = false;
            lock (ctx)
            {
                res = ctx.ContentTypes.Any();
            }

            return res;
        }

        public async Task<bool> ContentTypesAnyAsync()
        {
            return await ctx.ContentTypes.AnyAsync();
        }

        public bool QuestionTypesAny()
        {
            bool res = false;

            lock (ctx) {
                res = ctx.QuestionTypes.Any();
            }

            return res;
        }

        public async Task<bool> QuestionTypesAnyAsync()
        {
            return await ctx.QuestionTypes.AnyAsync();
        }

        public bool SubjectsAny()
        {
            bool res = false;

            lock (ctx)
            {
                res = ctx.Subjects.Any();
            }
            return res;
        }

        public async Task<bool> SubjectsAnyAsync()
        {
            return await ctx.Subjects.AnyAsync();
        }

        public bool TestsAny()
        {
            bool res = false;

            lock (ctx)
            {
                res = ctx.Tests.Any();
            }

            return res;
        }

        public async Task<bool> TestsAnyAsync()
        {
            return await ctx.Tests.AnyAsync();
        }

        public bool TestTypesAny()
        {
            bool res = false;

            lock (ctx)
            {
                res = ctx.TestTypes.Any();
            }
            return res;
        }

        public async Task<bool> TestTypesAnyAsync()
        {
            return await ctx.TestTypes.AnyAsync();
        }

        public void DbAddSubjects(Subject subjectEF)
        {
            lock (ctx)
            {
                ctx.Subjects.Add(subjectEF);
                ctx.SaveChanges();
            }
        }

        public async Task DbAddSubjectsAsync(Subject subjectEF)
        {
            await ctx.Subjects.AddAsync(subjectEF);
            await ctx.SaveChangesAsync();
        }

        public void DbAddTest(Test test, IList<HtmlQuestion> questions)
        {
            lock (ctx)
            {
                ctx.Tests.Add(test);
                ctx.SaveChanges();
            }

            foreach (var question in questions)
            {
                try
                {
                    Question questionEF = new Question();
                    questionEF.Test = test;
                    questionEF.Content = question.QuestionBody.GetJsonQuestion();
                    questionEF.QuestionType = GetQuestionType(question.QuestionType);
                    questionEF.ContentType = GetContentType(question.QuestionBody.GetContentType());

                    lock (ctx)
                    {
                        ctx.Questions.Add(questionEF);
                        ctx.SaveChanges();
                    }

                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }

        }

        public async Task DbAddTestAsync(Test test, IList<HtmlQuestion> questions)
        {
            await ctx.Tests.AddAsync(test);
            await ctx.SaveChangesAsync();

            foreach (var question in questions)
            {
                try
                {
                    Question questionEF = new Question();
                    questionEF.Test = test;
                    questionEF.Content = question.QuestionBody.GetJsonQuestion();
                    questionEF.QuestionType = await GetQuestionTypeAsync(question.QuestionType);
                    questionEF.ContentType = await GetContentTypeAsync(question.QuestionBody.GetContentType());

                    await ctx.Questions.AddAsync(questionEF);
                    await ctx.SaveChangesAsync();

                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
        }

        public void DbAddTestTypes(TestType testType)
        {
            lock (ctx)
            {
                ctx.TestTypes.Add(testType);
                ctx.SaveChanges();
            }
        }

        public async Task DbAddTestTypesAsync(TestType testType)
        {
            await ctx.TestTypes.AddAsync(testType);
            await ctx.SaveChangesAsync();
        }

        //private static void _GetContentTypeId(HtmlContentType htmlType, out ContentType contentType, out int id)
        //{
        //    contentType = null;
        //    id = -1;
        //    switch (htmlType)
        //    {
        //        case HtmlContentType.String:
        //            id = 3; // Text
        //            break;
        //        case HtmlContentType.Image:
        //        case HtmlContentType.Video:
        //            id = 1; // Json
        //            break;
        //        case HtmlContentType.None:
        //        default:
        //            id = -1;
        //            break;
        //    }
        //}

        public ContentType GetContentType(HtmlContentType htmlType)
        {
            ContentType contentType;
            int id;
            ZnoParserHelper.GetContentTypeId(htmlType, out contentType, out id);

            if (id > 0)
            {
                lock (ctx)
                {
                    contentType = ctx.ContentTypes.FirstOrDefault((q) => q.Id == id);
                }
            }
            return contentType;
        }

        public async Task<ContentType> GetContentTypeAsync(HtmlContentType htmlType)
        {
            ContentType contentType;
            int id;
            ZnoParserHelper.GetContentTypeId(htmlType, out contentType, out id);

            if (id > 0)
            {
                contentType = await ctx.ContentTypes.FirstOrDefaultAsync((q) => q.Id == id);
            }
            return contentType;
        }

        //private static void _GetQuestionTypeId(HtmlQuestionType htmlType, out QuestionType questionType, out int id)
        //{
        //    questionType = null;
        //    id = -1;
        //    switch (htmlType)
        //    {
        //        case HtmlQuestionType.One:
        //            id = 1;
        //            break;
        //        case HtmlQuestionType.Many:
        //            id = 2;
        //            break;
        //        case HtmlQuestionType.Manual:
        //            id = 3;
        //            break;
        //        case HtmlQuestionType.Task:
        //            id = 4;
        //            break;
        //        default:
        //            id = -1;
        //            break;
        //    }
        //}

        public QuestionType GetQuestionType(HtmlQuestionType htmlType)
        {
            QuestionType questionType;
            int id;
            ZnoParserHelper.GetQuestionTypeId(htmlType, out questionType, out id);
            if (id > 0)
            {
                lock (ctx)
                    questionType = ctx.QuestionTypes.FirstOrDefault((q) => q.Id == id);
            }

            return questionType;
        }

        public async Task<QuestionType> GetQuestionTypeAsync(HtmlQuestionType htmlType)
        {
            QuestionType questionType;
            int id;
            ZnoParserHelper.GetQuestionTypeId(htmlType, out questionType, out id);
            if (id > 0)
            {
                questionType = await ctx.QuestionTypes.FirstOrDefaultAsync((q) => q.Id == id);
            }

            return questionType;
        }

        public async Task DbAddQuestionTypeAsync(QuestionType questionType)
        {
            await ctx.QuestionTypes.AddAsync(questionType);
            await ctx.SaveChangesAsync();
        }

        public void DbAddQuestionType(QuestionType questionType)
        {
            lock (ctx)
            {
                ctx.QuestionTypes.Add(questionType);
                ctx.SaveChanges();
            }
        }

        public async Task DbAddContentTypeAsync(ContentType contentType)
        {
            await ctx.ContentTypes.AddAsync(contentType);
            await ctx.SaveChangesAsync();
        }

        public void DbAddContentType(ContentType contentType)
        {
            lock (ctx)
            {
                ctx.ContentTypes.Add(contentType);
                ctx.SaveChanges();
            }
        }

        public Subject FindSubjectByName(string name)
        {
            Subject res = null;

            lock (ctx)
            {
                res = ctx.Subjects.FirstOrDefault((s) => s.Name == name);
            }
            return res;
        }

        public async Task<Subject> FindSubjectByNameAsync(string name)
        {
            return await ctx.Subjects.FirstOrDefaultAsync((s) => s.Name == name);
        }

        public TestType FindTestTypeByName(string name)
        {
            TestType res = null;

            lock (ctx)
            {
                res = ctx.TestTypes.FirstOrDefault((s) => s.Name == name);
            }
            return res;
        }

        public async Task<TestType> FindTestTypeByNameAsync(string name)
        {
            return await ctx.TestTypes.FirstOrDefaultAsync((s) => s.Name == name);
        }
    }
}
