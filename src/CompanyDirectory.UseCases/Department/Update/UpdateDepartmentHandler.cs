using Ardalis.Result;
using CompanyDirectory.Core.Interface;
using MediatR;

namespace CompanyDirectory.UseCases.Department.Update;

internal sealed class UpdateDepartmentHandler(
    IUnitOfWork unitOfWork,
    IReadRepos<Core.Entities.Location> locationReadRepos,
    IReadRepos<Core.Entities.Department> departmentReadRepos) : IRequestHandler<UpdateDepartmentRequest, Result>
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    private readonly IReadRepos<Core.Entities.Location> _locationReadRepos = locationReadRepos;
    private readonly IReadRepos<Core.Entities.Department> _departmentReadRepos = departmentReadRepos;

    public async Task<Result> Handle(UpdateDepartmentRequest request, CancellationToken cancellationToken)
    {
        if(await _locationReadRepos.ExistsAsync(l => l.Id == request.LocationId, cancellationToken))
            return Result.NotFound($"No location found with the ID '{request.Id}'.");

        var department = await _departmentReadRepos.GetByKeyAsync(d => d.Id == request.Id, cancellationToken);

        if(department is null)
            return Result.NotFound($"No department found with the ID '{request.Id}'.");

        if(department.Name!.ToLower().Trim() == request.Name.ToLower().Trim())
            return  Result.Conflict($"The department with the name '{request.Name}' already exists.");

        await _unitOfWork.DepartmentWriteRepos.UpdateAsync(department, cancellationToken);
        await _unitOfWork.CommitAsync(cancellationToken);

        return Result.Success();
    }
}