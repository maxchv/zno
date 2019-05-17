using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace ZnoModelLibrary.Interfaces
{
    /// <summary>
    /// Обобщенный репозиторий
    /// </summary>
    /// <typeparam name="TEntity">Тип сущности</typeparam>
    public interface IGenericRepository<TEntity> where TEntity : class
    {
        // Поиск элементов, соответствующих указанному предикату
        Task<IEnumerable<TEntity>> Find(Expression<Func<TEntity, bool>> predicate);

        // Поиск элемента по идентификатору
        Task<TEntity> FindById(int id);

        // Поиск всех элементов
        Task<IEnumerable<TEntity>> FindAll();

        // Вставка новой сущности
        Task Insert(TEntity entity);

        // Обновление сущности
        Task Update(TEntity entityToUpdate);

        // Удаление сущности по идентификатору
        Task Delete(int id);
    }
}