using ZnoModelLibrary.Implementation;

namespace ZnoModelLibrary.Interfaces
{
    public interface IUnitOfWork
    {
        UserRepository Users { get; }

        void BeginTransaction();
        void Commit();
        void Rollback();
        void Save();
    }
}