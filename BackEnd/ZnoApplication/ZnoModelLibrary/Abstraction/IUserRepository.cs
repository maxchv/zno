using System.Threading.Tasks;

namespace Zno.DAL.Abstraction
{
    public interface IUserRepository<TEntity>
    {
        Task<TEntity> FindByLogin(string login);
    }
}