using Ardalis.Result;
using CompanyDirectory.Core.Interface;
using CompanyDirectory.UseCases.Department;
using MediatR;

namespace CompanyDirectory.UseCases.Personnel.GetAll;

internal sealed class GetAllPersonnelHandler(
    IReadRepos<Core.Entities.Personnel> personnelReadRepos) : IRequestHandler<GetAllPersonnelRequest, Result<IEnumerable<PersonelDto>>>
{
    private readonly IReadRepos<Core.Entities.Personnel> _personnelReadRepos = personnelReadRepos;

    public async Task<Result<IEnumerable<PersonelDto>>> Handle(GetAllPersonnelRequest request, CancellationToken cancellationToken)
    {
        var personnel = _personnelReadRepos.GetList();

        var personnelDto = personnel
            .Select(p => new PersonelDto(
                p.Id,
                p.FirstName ?? string.Empty,
                p.LastName ?? string.Empty,
                p.Email ?? string.Empty,
                p.JobTitle ?? string.Empty,
                new DepartmentDto(
                    p.Department!.Id, 
                    p.Department!.Name ?? string.Empty, 
                    null!)))
            .AsEnumerable();

        return await Task.FromResult(Result.Success(personnelDto));
    }
}