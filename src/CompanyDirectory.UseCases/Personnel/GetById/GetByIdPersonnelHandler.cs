using Ardalis.Result;
using CompanyDirectory.Core.Interface;
using CompanyDirectory.UseCases.Department;
using MediatR;

namespace CompanyDirectory.UseCases.Personnel.GetById;

internal sealed class GetByIdPersonnelHandler(
    IReadRepos<Core.Entities.Personnel> personnelReadRepos) : IRequestHandler<GetByIdPersonnelRequest, Result<PersonelDto>>
{
    private readonly IReadRepos<Core.Entities.Personnel> _personnelReadRepos = personnelReadRepos;

    public async Task<Result<PersonelDto>> Handle(GetByIdPersonnelRequest request, CancellationToken cancellationToken)
    {
        var personnel = await _personnelReadRepos.GetByKeyAsync(p => p.Id == request.Id);

        if(personnel is null)
        {
            return Result.NotFound($"No personnel found with the ID '{request.Id}'.");
        }

        var personnelDto = new PersonelDto(
            personnel.Id,
            personnel.FirstName ?? string.Empty,
            personnel.LastName ?? string.Empty,
            personnel.Email ?? string.Empty,
            personnel.JobTitle ?? string.Empty,
            new DepartmentDto(
                personnel.Department!.Id,
                personnel.Department.Name ?? string.Empty,
                null!));

        return await Task.FromResult(Result.Success(personnelDto));
    }
}