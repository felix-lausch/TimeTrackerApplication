namespace TimeTrackerApi.Repositories;

using Ardalis.Specification;
using Ardalis.Specification.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

public interface IRepository<T> : IRepositoryBase<T>
    where T : class
{
}

public class EfRepository<T> : RepositoryBase<T>, IRepository<T>
    where T : class
{
    public EfRepository(TimeTrackerContext dbContext)
        : base(dbContext)
    {
    }
}

public class CachedRepository<T> : IRepository<T>
    where T : class
{
    private readonly EfRepository<T> repo;
    private readonly IMemoryCache cache;
    private readonly ILogger<CachedRepository<T>> logger;
    private readonly MemoryCacheEntryOptions cacheOptions;

    public CachedRepository(EfRepository<T> repo, IMemoryCache cache, ILogger<CachedRepository<T>> logger)
    {
        this.repo = repo;
        this.cache = cache;
        this.logger = logger;

        cacheOptions = new MemoryCacheEntryOptions().SetAbsoluteExpiration(TimeSpan.FromSeconds(10));
    }

    public Task<T> AddAsync(T entity, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<T>> AddRangeAsync(IEnumerable<T> entities, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public Task<bool> AnyAsync(ISpecification<T> specification, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public Task<bool> AnyAsync(CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public Task<int> CountAsync(ISpecification<T> specification, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public Task<int> CountAsync(CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public Task DeleteAsync(T entity, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public Task DeleteRangeAsync(IEnumerable<T> entities, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public Task<T?> FirstOrDefaultAsync(ISpecification<T> specification, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public Task<TResult?> FirstOrDefaultAsync<TResult>(ISpecification<T, TResult> specification, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public Task<T?> GetByIdAsync<TId>(TId id, CancellationToken cancellationToken = default) where TId : notnull
    {
        throw new NotImplementedException();
    }

    public Task<T?> GetBySpecAsync<Spec>(Spec specification, CancellationToken cancellationToken = default) where Spec : ISingleResultSpecification, ISpecification<T>
    {
        if (specification.CacheEnabled)
        {
            string key = $"{specification.CacheKey}-GetBySpecAsync";
            logger.LogInformation("Checking cache for " + key);
            return cache.GetOrCreate(key, entry =>
            {
                entry.SetOptions(cacheOptions);
                logger.LogWarning("Fetching source data for " + key);
                return repo.GetBySpecAsync(specification);
            });
        }
        return repo.GetBySpecAsync(specification);
    }

    public Task<TResult> GetBySpecAsync<TResult>(ISpecification<T, TResult> specification, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public Task<T?> GetBySpecAsync(ISpecification<T> specification, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public async Task<List<T>> ListAsync(CancellationToken cancellationToken = default)
    {
        var key = $"{nameof(T)}-ListAsync";
        logger.LogInformation("Checking cache for " + key);
        return await cache.GetOrCreateAsync(key, async entry =>
        {
            entry.SetOptions(cacheOptions);
            logger.LogWarning("Fetching source data for " + key);
            return await repo.ListAsync();
        });
    }

    public Task<List<T>> ListAsync(ISpecification<T> specification, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public Task<List<TResult>> ListAsync<TResult>(ISpecification<T, TResult> specification, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public Task SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public Task<T?> SingleOrDefaultAsync(ISingleResultSpecification<T> specification, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public Task<TResult?> SingleOrDefaultAsync<TResult>(ISingleResultSpecification<T, TResult> specification, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public Task UpdateAsync(T entity, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public Task UpdateRangeAsync(IEnumerable<T> entities, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    Task<int> IRepositoryBase<T>.SaveChangesAsync(CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}
