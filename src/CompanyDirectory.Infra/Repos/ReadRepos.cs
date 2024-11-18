using System.Linq.Expressions;
using CompanyDirectory.Core.Abstract;
using CompanyDirectory.Core.Interface;
using CompanyDirectory.Infra.Data.Context;
using Microsoft.EntityFrameworkCore;

namespace CompanyDirectory.Infra.Repos;

public sealed class ReadRepos<T> : IReadRepos<T> where T : BaseEntity
{
    private readonly AppDbContext _appDbContext;
    private readonly DbSet<T> _dbSet;

    public ReadRepos(AppDbContext appDbContext)
    {
        _appDbContext = appDbContext;
        _dbSet = _appDbContext.Set<T>();
    }

    public async Task<T> GetByKeyAsync(Expression<Func<T, bool>> condition, CancellationToken cancellationToken = default)
    {
        return await _dbSet.FirstOrDefaultAsync(condition, cancellationToken) ?? null!;
    }

    public Task<bool> ExistsAsync(Expression<Func<T, bool>> condition, CancellationToken cancellationToken = default)
    {
        return _dbSet.AnyAsync(condition, cancellationToken);
    }

    public IQueryable<T> GetList()
    {
        return _dbSet.AsQueryable();
    }
}