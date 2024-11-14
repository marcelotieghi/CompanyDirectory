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

    public IQueryable<T> Find(Expression<Func<T, bool>> expression)
    {
        return _dbSet.Where(expression).AsQueryable();
    }

    public IQueryable<T> GetList()
    {
        return _dbSet.AsQueryable();
    }
}