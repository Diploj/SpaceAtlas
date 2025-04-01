using System.Linq.Expressions;
using SpaceAtlas.DataAccess.Entities;

namespace SpaceAtlas.DataAccess.Repository;

public interface IRepository<T> where T : IBaseEntity
{
    IEnumerable<T> GetAll();
    IEnumerable<T> GetAll(Expression<Func<T, bool>> filter);
    T? GetById(Guid id);
    Guid Save(T entity);
    void Delete(Guid entity);
}