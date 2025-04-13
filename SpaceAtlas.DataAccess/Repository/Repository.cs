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
    
    public async Task<IEnumerable<T>> GetAll()
    {
        await using var context = _contextFactory.CreateDbContext();
        return await context.Set<T>().ToListAsync();
    }

    public async Task<IEnumerable<T>> GetAll(Expression<Func<T, bool>> filter)
    {
        await using var context = _contextFactory.CreateDbContext();
        return await context.Set<T>().Where(filter).ToListAsync();
    }

    public async Task<T?> GetById(Guid id)
    {
        await using var context = _contextFactory.CreateDbContext();
        return await context.Set<T>().FirstOrDefaultAsync(x => x.Id == id);
    }

    public async Task<Guid> Save(T entity)
    {
        await using var context = _contextFactory.CreateDbContext();
        if (context.Set<T>().Any(x => x.Id == entity.Id))
        {
            var result = context.Set<T>().Attach(entity);
            context.Entry(entity).State = EntityState.Modified;
            await context.SaveChangesAsync();
            return result.Entity.Id;
        }
        else
        {
            var result = context.Set<T>().Add(entity);
            await context.SaveChangesAsync();
            return result.Entity.Id;
        }
    }

    public async Task Delete(Guid entity)
    {
        await using var context = _contextFactory.CreateDbContext();
        //context.Set<T>().Attach(entity);
        //context.Entry(entity).State = EntityState.Deleted;
        //context.SaveChanges();
        await context.Set<T>().Where(x => x.Id == entity)
            .ExecuteDeleteAsync();
    }
}