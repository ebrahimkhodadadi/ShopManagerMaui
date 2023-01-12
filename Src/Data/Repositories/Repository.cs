

using Common.Utilities;
using Data.Contracts;
using Microsoft.EntityFrameworkCore;
using Entities.Common;

namespace Data.Repositories;

public class Repository<TEntity> : IRepository<TEntity>
        where TEntity : class, IEntity
{
    protected readonly ApplicationDbContext DbContext;
    public DbSet<TEntity> Entities { get; }
    public virtual IQueryable<TEntity> Table => Entities;
    public virtual IQueryable<TEntity> TableNoTracking => Entities.AsNoTracking();

    public Repository(ApplicationDbContext dbContext)
    {
        DbContext = dbContext;
        Entities = DbContext.Set<TEntity>(); // City => Cities
    }

    public virtual ValueTask<TEntity> GetByIdAsync(CancellationToken cancellationToken, params object[] ids)
    {
        return Entities.FindAsync(ids, cancellationToken);
    }

    public virtual void Add(TEntity entity, bool saveNow = true)
    {
        Assert.NotNull(entity, nameof(entity));
        Entities.Add(entity);
        if (saveNow)
            DbContext.SaveChanges();
        
    }
    
    public virtual async Task AddAsync(TEntity entity, CancellationToken cancellationToken, bool saveNow = true)
    {
        Assert.NotNull(entity, nameof(entity));
        await Entities.AddAsync(entity, cancellationToken).ConfigureAwait(false);
        if (saveNow)
            await DbContext.SaveChangesAsync(cancellationToken).ConfigureAwait(false);
    }

    public virtual async Task AddRangeAsync(IEnumerable<TEntity> entities, CancellationToken cancellationToken, bool saveNow = true)
    {
        Assert.NotNull(entities, nameof(entities));
        await Entities.AddRangeAsync(entities, cancellationToken).ConfigureAwait(false);
        if (saveNow)
            await DbContext.SaveChangesAsync(cancellationToken).ConfigureAwait(false);
    }

    public virtual async Task UpdateAsync(TEntity entity, CancellationToken cancellationToken, bool saveNow = true)
    {
        Assert.NotNull(entity, nameof(entity));
        Entities.Update(entity);
        if (saveNow)
            await DbContext.SaveChangesAsync(cancellationToken).ConfigureAwait(false);
    }

    public virtual async Task UpdateRangeAsync(IEnumerable<TEntity> entities, CancellationToken cancellationToken, bool saveNow = true)
    {
        Assert.NotNull(entities, nameof(entities));
        Entities.UpdateRange(entities);
        if (saveNow)
            await DbContext.SaveChangesAsync(cancellationToken).ConfigureAwait(false);
    }

    public virtual async Task DeleteAsync(TEntity entity, CancellationToken cancellationToken, bool saveNow = true)
    {
        Assert.NotNull(entity, nameof(entity));
        Entities.Remove(entity);
        if (saveNow)
            await DbContext.SaveChangesAsync(cancellationToken).ConfigureAwait(false);
    }

    public virtual async Task DeleteRangeAsync(IEnumerable<TEntity> entities, CancellationToken cancellationToken, bool saveNow = true)
    {
        Assert.NotNull(entities, nameof(entities));
        Entities.RemoveRange(entities);
        if (saveNow)
            await DbContext.SaveChangesAsync(cancellationToken).ConfigureAwait(false);
    }
}