using CompanyDirectory.Core.Abstract;
using CompanyDirectory.Core.Interface;
using CompanyDirectory.Infra.Data.Context;
using Microsoft.EntityFrameworkCore;

namespace CompanyDirectory.Infra.Repos;

public sealed class WriteRepos<T> : IWriteRepos<T> where T : BaseEntity
{
    private readonly AppDbContext _appDbContext;
    private readonly DbSet<T> _dbSet;
    public WriteRepos(AppDbContext appDbContext)
    {
        _appDbContext = appDbContext;
        _dbSet = _appDbContext.Set<T>();
    }
    public async Task CreateAsync(T entity, CancellationToken cancellationToken)
    {
        await _dbSet.AddAsync(entity, cancellationToken);
    }

    public async Task UpdateAsync(T entity, CancellationToken cancellationToken)
    {
        await Task.Run(() => _dbSet.Update(entity), cancellationToken);
    }

    public async Task SoftDeleteAsync(int[] ids, CancellationToken cancellationToken)
    {
        await _dbSet
            .Where(entity => ids.Contains(entity.Id))
            .ExecuteUpdateAsync(setter => setter
                .SetProperty(entity => entity.IsDeleted, true)
                .SetProperty(entity => entity.DeletedOn, DateTime.Now), cancellationToken);
    }
}