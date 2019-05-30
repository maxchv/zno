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

        void BeginTransaction();
        void Commit();
        void Rollback();
        Task SaveChanges();
    }
}