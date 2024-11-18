using Ardalis.Result;
using CompanyDirectory.Core.Interface;
using CompanyDirectory.UseCases.Location;
using MediatR;

namespace CompanyDirectory.UseCases.Department.GetById;

internal sealed class GetByIdDepartmentHandler(
    IReadRepos<Core.Entities.Department> departmentReadRepos) : IRequestHandler<GetByIdDepartmentRequest, Result<DepartmentDto>>
{
    private readonly IReadRepos<Core.Entities.Department> _departmentReadRepos = departmentReadRepos;

    public async Task<Result<DepartmentDto>> Handle(GetByIdDepartmentRequest request, CancellationToken cancellationToken)
    {
        var department = await _departmentReadRepos
            .GetByKeyAsync(d => d.Id == request.Id, cancellationToken);

        if(department is null)
        {
            return Result.NotFound($"No department found with the ID '{request.Id}'.");
        }

        var departmentDto = new DepartmentDto(
            department.Id,
            department.Name ?? string.Empty,
            new LocationDto(
                department.Location!.Id,
                department.Location.Name ?? string.Empty));
        
        return await Task.FromResult(Result.Success(departmentDto));
    }
}