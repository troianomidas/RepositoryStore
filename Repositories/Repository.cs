using Microsoft.EntityFrameworkCore;
using RepositoryStore.Repositories.Abstractions;

namespace RepositoryStore.Repositories;

// abstrata pois ninguém pode instancia-la somente herda-la
public abstract class Repository<T>(DbContext context) : IRepository<T> where T : class
{
    private readonly DbSet<T> _dbSet = context.Set<T>();
    
    public async Task<T> CreateAsync(T entity, CancellationToken cancellationToken = default)
    {
        await _dbSet.AddAsync(entity, cancellationToken);
        await context.SaveChangesAsync(cancellationToken);
        return entity;
    }

    public async Task<T> UpdateAsync(T entity, CancellationToken cancellationToken = default)
    {
        _dbSet.Update(entity);
        await context.SaveChangesAsync(cancellationToken);
        return entity;
    }

    public async Task<T> DeleteAsync(T entity, CancellationToken cancellationToken = default)
    {
        _dbSet.Remove(entity);
        await context.SaveChangesAsync(cancellationToken);
        return entity;
    }

    public async Task<T?> GetByIdAsync(int id, CancellationToken cancellationToken = default)
        => await _dbSet.FindAsync(id, cancellationToken);

    public async Task<List<T>?> GetAllAsync(int skip = 0, int take = 25, CancellationToken cancellationToken = default)
    => await _dbSet.AsNoTracking().Skip(skip).Take(take).ToListAsync(cancellationToken);
}