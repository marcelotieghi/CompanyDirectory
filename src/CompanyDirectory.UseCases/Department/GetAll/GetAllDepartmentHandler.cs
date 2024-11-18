using Ardalis.Result;
using CompanyDirectory.Core.Interface;
using CompanyDirectory.UseCases.Location;
using MediatR;

namespace CompanyDirectory.UseCases.Department.GetAll;

internal sealed class GetAllDepartmentHandler(
    IReadRepos<Core.Entities.Department> departmetReadRepos) : IRequestHandler<GetAllDepartmentRequest, Result<IEnumerable<DepartmentDto>>>
{
    private readonly IReadRepos<Core.Entities.Department> _departmetReadRepos = departmetReadRepos;

    public async Task<Result<IEnumerable<DepartmentDto>>> Handle(GetAllDepartmentRequest request, CancellationToken cancellationToken)
    {
        var departments = _departmetReadRepos.GetList();

        var departmentsDto = departments
            .Select(d => new DepartmentDto(
                d.Id,
                d.Name ?? string.Empty,
                new LocationDto(
                    d.Location!.Id,
                    d.Location.Name ?? string.Empty)))
            .AsEnumerable();

        return await Task.FromResult(Result.Success(departmentsDto));
    }
}