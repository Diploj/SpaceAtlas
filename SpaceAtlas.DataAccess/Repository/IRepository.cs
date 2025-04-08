using System.Linq.Expressions;
using SpaceAtlas.DataAccess.Entities;

namespace SpaceAtlas.DataAccess.Repository;

public interface IRepository<T> where T : IBaseEntity
{
    Task<IEnumerable<T>> GetAll();
    Task<IEnumerable<T>> GetAll(Expression<Func<T, bool>> filter);
    Task<T?> GetById(Guid id);
    Task<Guid> Save(T entity);
    Task Delete(Guid entity);
}