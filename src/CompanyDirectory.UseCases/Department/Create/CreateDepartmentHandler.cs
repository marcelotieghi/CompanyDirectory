using Ardalis.Result;
using CompanyDirectory.Core.Interface;
using MediatR;

namespace CompanyDirectory.UseCases.Department.Create;

internal sealed class CreateDepartmentHandler(
    IUnitOfWork unitOfWork,
    IReadRepos<Core.Entities.Department> departmentReadRepos,
    IReadRepos<Core.Entities.Location> locationReadRepos) : IRequestHandler<CreateDepartmentRequest, Result>
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    private readonly IReadRepos<Core.Entities.Department> _departmentReadRepos = departmentReadRepos;
    private readonly IReadRepos<Core.Entities.Location> _locationReadRepos = locationReadRepos;

    public async Task<Result> Handle(CreateDepartmentRequest request, CancellationToken cancellationToken)
    {
        if(!await _locationReadRepos.ExistsAsync(l => l.Id == request.LocationId, cancellationToken))
        {
            return Result.NotFound($"No location found with the ID '{request.LocationId}'.");
        }
            
        if(await _departmentReadRepos.ExistsAsync(d => d.Name!.ToLower().Trim() == request.Name.ToLower().Trim(), cancellationToken))
        {
            return  Result.Conflict($"The department with the name '{request.Name}' already exists.");
        }
            
        var newDepartment = new Core.Entities.Department(request.Name, request.LocationId);

        await _unitOfWork.DepartmentWriteRepos.CreateAsync(newDepartment, cancellationToken);
        await _unitOfWork.CommitAsync(cancellationToken);

        return Result.Success();
    }
}