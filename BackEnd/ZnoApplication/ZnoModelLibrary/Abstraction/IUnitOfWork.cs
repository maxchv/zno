using ZnoModelLibrary.Implementation;

namespace ZnoModelLibrary.Interfaces
{
    public interface IUnitOfWork
    {
        UserRepository Users { get; }
        SubjectRepository Subjects { get; }
        TestRepository Tests { get; }

        void BeginTransaction();
        void Commit();
        void Rollback();
        void Save();
    }
}