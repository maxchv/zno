using ZnoModelLibrary.Implementation;

namespace ZnoModelLibrary.Interfaces
{
    public interface IUnitOfWork
    {
        UserRepository Users { get; }
        TestRepository Tests { get; }
        TestSettingsRepository TestSettings { get; }
        QuestionRepository Questions { get; }
        GeneratedTestRepository GeneratedTests { get; }
        SubjectRepository Subjects { get; }
        TestTypeRepository TestTypes { get; }
        UserAnswerRepository UserAnswers { get; }

        void BeginTransaction();
        void Commit();
        void Rollback();
        void Save();
    }
}