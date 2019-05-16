using System.Threading.Tasks;
using ZnoModelLibrary.Implementation;

namespace ZnoModelLibrary.Interfaces
{
    public interface IUnitOfWork
    {
        UserRepository Users { get; }
        SubjectRepository Subjects { get; }
        TestRepository Tests { get; }
        TestSettingsRepository TestSettings { get; }

        void BeginTransaction();
        void Commit();
        void Rollback();
        Task SaveChanges();
    }
}