using CompanyDirectory.Core.Entities;

namespace CompanyDirectory.Core.Interface;

public interface IUnitOfWork : IDisposable
{
    IWriteRepos<Location> LocationWriteRepos { get; }
    IWriteRepos<Department> DepartmentWriteRepos { get; }
    IWriteRepos<Personnel> PersonnelWriteRepos { get; }

    Task<int> CommitAsync(CancellationToken cancellationToken);
}