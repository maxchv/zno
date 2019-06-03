using System.Threading.Tasks;
using Zno.DAL.Implementation;

namespace Zno.DAL.Interfaces
{
    public interface IUnitOfWork
    {
        UserRepository Users { get; }
        SubjectRepository Subjects { get; }
        TestRepository Tests { get; }
        TestSettingsRepository TestSettings { get; }
        TestTypeRepository TestTypes { get; }
        QuestionRepository Questions { get; }
        QuestionTypeRepository QuestionTypes { get; }
        ContentTypeRepository ContentTypes { get; }
        GeneratedTestRepository GeneratedTests { get; }
        AnswerRepository Answers { get; }
        UserAnswerRepository UserAnswers { get; }

        void BeginTransaction();
        void Commit();
        void Rollback();
        Task SaveChanges();
    }
}