using CompanyDirectory.Core.Abstract;

namespace CompanyDirectory.Core.Interface;

public interface IWriteRepos<T> where T : BaseEntity
{
    Task<T> CreateAsync(T entity, CancellationToken cancellationToken);
    Task<T> UpdateAsync(T entity, CancellationToken cancellationToken);
    Task<T> SoftDeleteAsync(int id, CancellationToken cancellationToken);
}