using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Zno.DAL.Entities;
using Zno.Parser.Models;
using Zno.Parser.Models.Enums;

namespace Zno.Parser.Abstruction
{
    public interface IRepositoryParser
    {
        Task DbAddSubjectsAsync(Subject subjectEF);
        void DbAddSubjects(Subject subjectEF);

        Task DbAddTestTypesAsync(TestType testType);
        void DbAddTestTypes(TestType testType);

        Task<ContentType> GetContentTypeAsync(HtmlContentType htmlType);
        ContentType GetContentType(HtmlContentType htmlType);

        Task<QuestionType> GetQuestionTypeAsync(HtmlQuestionType htmlType);
        QuestionType GetQuestionType(HtmlQuestionType htmlType);

        Task DbAddTestAsync(Test test, IList<HtmlQuestion> questions);
        void DbAddTest(Test test, IList<HtmlQuestion> questions);

        Task DbAddQuestionTypeAsync(QuestionType questionType);
        void DbAddQuestionType(QuestionType questionType);

        Task DbAddContentTypeAsync(ContentType contentType);
        void DbAddContentType(ContentType contentType);

        bool SubjectsAny();
        bool TestsAny();
        bool TestTypesAny();
        bool QuestionTypesAny();
        bool ContentTypesAny();

        Task<bool> SubjectsAnyAsync();
        Task<bool> TestsAnyAsync();
        Task<bool> TestTypesAnyAsync();
        Task<bool> QuestionTypesAnyAsync();
        Task<bool> ContentTypesAnyAsync();

        Subject FindSubjectByName(string name);
        Task<Subject> FindSubjectByNameAsync(string name);
        TestType FindTestTypeByName(string name);
        Task<TestType> FindTestTypeByNameAsync(string name);

    }
}
