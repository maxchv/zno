using System.Threading.Tasks;

namespace ZnoModelLibrary.Abstraction
{
    public interface IUserRepository<TEntity>
    {
        Task<TEntity> FindByLogin(string login);
    }
}