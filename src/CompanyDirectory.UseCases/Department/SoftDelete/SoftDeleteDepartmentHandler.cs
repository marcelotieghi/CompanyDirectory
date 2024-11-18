using Ardalis.Result;
using CompanyDirectory.Core.Interface;
using MediatR;

namespace CompanyDirectory.UseCases.Department.SoftDelete;

internal sealed class SoftDeleteDepartmentHandler(
    IUnitOfWork unitOfWork,
    IReadRepos<Core.Entities.Department> departmentReadRepos) : IRequestHandler<SoftDeleteDepartmentRequest, Result>
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    private readonly IReadRepos<Core.Entities.Department> _departmentReadRepos = departmentReadRepos;

    public async Task<Result> Handle(SoftDeleteDepartmentRequest request, CancellationToken cancellationToken)
    {
        if(!await _departmentReadRepos.ExistsAsync(d => request.Ids.Contains(d.Id), cancellationToken))
        {
            return Result.NotFound($"No department found!");
        }
            
        await _unitOfWork.DepartmentWriteRepos.SoftDeleteAsync(request.Ids, cancellationToken);

        return Result.Success();
    }
}