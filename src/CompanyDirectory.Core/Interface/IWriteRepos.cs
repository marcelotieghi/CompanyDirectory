using CompanyDirectory.Core.Abstract;

namespace CompanyDirectory.Core.Interface;

public interface IWriteRepos<T> where T : BaseEntity
{
    Task CreateAsync(T entity, CancellationToken cancellationToken);
    Task UpdateAsync(T entity, CancellationToken cancellationToken);
    Task SoftDeleteAsync(int[] ids, CancellationToken cancellationToken);
}