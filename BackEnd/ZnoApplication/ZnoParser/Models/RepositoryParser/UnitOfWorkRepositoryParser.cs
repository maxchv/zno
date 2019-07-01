using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Threading.Tasks;
using Zno.DAL.Entities;
using Zno.DAL.Interfaces;
using Zno.Parser.Abstruction;
using Zno.Parser.Models.Enums;
using Zno.Parser.Helpers;

namespace Zno.Parser.Models.RepositoryParser
{
    public class UnitOfWorkRepositoryParser : IRepositoryParser
    {
        private IUnitOfWork unitOfWork;

        public UnitOfWorkRepositoryParser(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        public bool ContentTypesAny()
        {
            return unitOfWork.ContentTypes.FindAll().Result.Count() > 0;
        }

        public async Task<bool> ContentTypesAnyAsync()
        {
            return (await unitOfWork.ContentTypes.FindAll()).Count() > 0;
        }

        public bool QuestionTypesAny()
        {
            return unitOfWork.QuestionTypes.FindAll().Result.Count() > 0;
        }

        public async Task<bool> QuestionTypesAnyAsync()
        {
            return (await unitOfWork.QuestionTypes.FindAll()).Count() > 0;
        }

        public bool SubjectsAny()
        {
            return unitOfWork.Subjects.FindAll().Result.Count() > 0;
        }

        public async Task<bool> SubjectsAnyAsync()
        {
            return (await unitOfWork.Subjects.FindAll()).Count() > 0;
        }

        public bool TestsAny()
        {
            return unitOfWork.Tests.FindAll().Result.Count() > 0;
        }

        public async Task<bool> TestsAnyAsync()
        {
            return (await unitOfWork.Tests.FindAll()).Count() > 0;
        }

        public bool TestTypesAny()
        {
            return unitOfWork.TestTypes.FindAll().Result.Count() > 0;
        }

        public async Task<bool> TestTypesAnyAsync()
        {
            return (await unitOfWork.TestTypes.FindAll()).Count() > 0;
        }

        public void DbAddSubjects(Subject subjectEF)
        {
            unitOfWork.Subjects.Insert(subjectEF);
            unitOfWork.SaveChanges();
        }

        public async Task DbAddSubjectsAsync(Subject subjectEF)
        {
            await unitOfWork.Subjects.Insert(subjectEF);
            await unitOfWork.SaveChanges();
        }

        public void DbAddTest(Test test, IList<HtmlQuestion> questions)
        {
            unitOfWork.Tests.Insert(test);
            unitOfWork.SaveChanges();

            foreach (var question in questions)
            {
                try
                {
                    Question questionEF = new Question();
                    questionEF.Test = test;
                    questionEF.Content = question.QuestionBody.GetJsonQuestion();
                    questionEF.QuestionType = GetQuestionType(question.QuestionType);
                    questionEF.ContentType = GetContentType(question.QuestionBody.GetContentType());

                    unitOfWork.Questions.Insert(questionEF);
                    unitOfWork.SaveChanges();

                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
        }

        public async Task DbAddTestAsync(Test test, IList<HtmlQuestion> questions)
        {
            await unitOfWork.Tests.Insert(test);
            await unitOfWork.SaveChanges();

            foreach (var question in questions)
            {
                try
                {
                    Question questionEF = new Question();
                    questionEF.Test = test;
                    questionEF.Content = question.QuestionBody.GetJsonQuestion();
                    questionEF.QuestionType = await GetQuestionTypeAsync(question.QuestionType);
                    questionEF.ContentType = await GetContentTypeAsync(question.QuestionBody.GetContentType());

                    await unitOfWork.Questions.Insert(questionEF);
                    await unitOfWork.SaveChanges();

                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
        }

        public void DbAddTestTypes(TestType testType)
        {
            unitOfWork.TestTypes.Insert(testType);
            unitOfWork.SaveChanges();
        }

        public async Task DbAddTestTypesAsync(TestType testType)
        {
            await unitOfWork.TestTypes.Insert(testType);
            await unitOfWork.SaveChanges();
        }

        public ContentType GetContentType(HtmlContentType htmlType)
        {
            ContentType contentType;
            int id;
            ZnoParserHelper.GetContentTypeId(htmlType, out contentType, out id);

            if (id > 0)
            {
                contentType = unitOfWork.ContentTypes.FindById(id).Result;
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
                contentType = await unitOfWork.ContentTypes.FindById(id);
            }
            return contentType;
        }

        public QuestionType GetQuestionType(HtmlQuestionType htmlType)
        {
            QuestionType questionType;
            int id;
            ZnoParserHelper.GetQuestionTypeId(htmlType, out questionType, out id);
            if (id > 0)
            {
                questionType = unitOfWork.QuestionTypes.FindById(id).Result;
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
                questionType = await unitOfWork.QuestionTypes.FindById(id);
            }

            return questionType;
        }

        public async Task DbAddQuestionTypeAsync(QuestionType questionType)
        {
            await unitOfWork.QuestionTypes.Insert(questionType);
            await unitOfWork.SaveChanges();
        }

        public void DbAddQuestionType(QuestionType questionType)
        {
            unitOfWork.QuestionTypes.Insert(questionType);
            unitOfWork.SaveChanges();
        }

        public async Task DbAddContentTypeAsync(ContentType contentType)
        {
            await unitOfWork.ContentTypes.Insert(contentType);
            await unitOfWork.SaveChanges();
        }

        public void DbAddContentType(ContentType contentType)
        {
            unitOfWork.ContentTypes.Insert(contentType);
            unitOfWork.SaveChanges();
        }

        public Subject FindSubjectByName(string name)
        {
            return unitOfWork.Subjects.Find((s) => s.Name == name).Result.FirstOrDefault();
        }

        public async Task<Subject> FindSubjectByNameAsync(string name)
        {
            return (await unitOfWork.Subjects.Find((s) => s.Name == name)).FirstOrDefault();
        }

        public TestType FindTestTypeByName(string name)
        {
            return unitOfWork.TestTypes.Find((s) => s.Name == name).Result.FirstOrDefault();
        }

        public async Task<TestType> FindTestTypeByNameAsync(string name)
        {
            return (await unitOfWork.TestTypes.Find((s) => s.Name == name)).FirstOrDefault();
        }
    }
}
