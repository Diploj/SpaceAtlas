using Microsoft.EntityFrameworkCore;
using SpaceAtlas.DataAccess.Entities;
using System.Linq.Expressions;
namespace SpaceAtlas.DataAccess.Repository;

public class Repository<T> : IRepository<T> where T : class,IBaseEntity
{
    private readonly IDbContextFactory<SpaceAtlasDbContext> _contextFactory;
    public Repository(IDbContextFactory<SpaceAtlasDbContext> contextFactory)
    {
        _contextFactory = contextFactory;
    }
    
    public IEnumerable<T> GetAll()
    {
        using var context = _contextFactory.CreateDbContext();
        return context.Set<T>().ToList();
    }

    public IEnumerable<T> GetAll(Expression<Func<T, bool>> filter)
    {
        using var context = _contextFactory.CreateDbContext();
        return context.Set<T>().Where(filter).ToList();
    }

    public T? GetById(Guid id)
    {
        using var context = _contextFactory.CreateDbContext();
        return context.Set<T>().FirstOrDefault(x => x.Id == id);
    }

    public Guid Save(T entity)
    {
        using var context = _contextFactory.CreateDbContext();
        if (context.Set<T>().Any(x => x.Id == entity.Id))
        {
            var result = context.Set<T>().Attach(entity);
            context.Entry(entity).State = EntityState.Modified;
            context.SaveChanges();
            return result.Entity.Id;
        }
        else
        {
            var result = context.Set<T>().Add(entity);
            context.SaveChanges();
            return result.Entity.Id;
        }
    }

    public void Delete(Guid entity)
    {
        using var context = _contextFactory.CreateDbContext();
        //context.Set<T>().Attach(entity);
        //context.Entry(entity).State = EntityState.Deleted;
        //context.SaveChanges();
        context.Set<T>().Where(x => x.Id == entity)
            .ExecuteDelete();
    }
}