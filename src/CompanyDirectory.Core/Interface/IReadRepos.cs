using System.Linq.Expressions;
using CompanyDirectory.Core.Abstract;

namespace CompanyDirectory.Core.Interface;

public interface IReadRepos<T> where T : BaseEntity
{
    Task<T> GetByKeyAsync(Expression<Func<T, bool>> condition, CancellationToken cancellationToken = default);
    IQueryable<T> GetList();
    Task<bool> ExistsAsync(Expression<Func<T, bool>> condition, CancellationToken cancellationToken = default);
}