using CompanyDirectory.Core.Entities;
using CompanyDirectory.Core.Interface;
using CompanyDirectory.Infra.Data.Context;

namespace CompanyDirectory.Infra.Repos;

public class UnitOfWork(AppDbContext appDbContext) : IUnitOfWork
{
    private readonly AppDbContext _appDbContext = appDbContext ?? throw new ArgumentNullException(nameof(appDbContext));

    private IWriteRepos<Location>? _locationWriteRepos;
    private IWriteRepos<Department>? _departmentWriteRepos;
    private IWriteRepos<Personnel>? _personnelWriteRepos;

    private bool _disposed = false;

    public IWriteRepos<Location> LocationWriteRepos => 
        _locationWriteRepos ??= new WriteRepos<Location>(_appDbContext);

    public IWriteRepos<Department> DepartmentWriteRepos => 
        _departmentWriteRepos ??= new WriteRepos<Department>(_appDbContext);

    public IWriteRepos<Personnel> PersonnelWriteRepos => 
        _personnelWriteRepos ??= new WriteRepos<Personnel>(_appDbContext);

    public Task<int> CommitAsync(CancellationToken cancellationToken)
    {
        return _appDbContext.SaveChangesAsync(cancellationToken);
    }

    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }

    protected virtual void Dispose(bool disposing)
    {
        if(!_disposed)
        {
            if(disposing)
            {
                _appDbContext?.Dispose();
            }
            _disposed = true;
        }
    }
}
