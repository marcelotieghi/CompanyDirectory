using Ardalis.Result;
using CompanyDirectory.Core.Interface;
using MediatR;

namespace CompanyDirectory.UseCases.Personnel.Create;

internal sealed class CreatePersonnelHandler(
    IUnitOfWork unitOfWork,
    IReadRepos<Core.Entities.Personnel> personnelReadRepos,
    IReadRepos<Core.Entities.Department> departmentReadRepos) : IRequestHandler<CreatePersonnelRequest, Result>
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    private readonly IReadRepos<Core.Entities.Personnel> _personnelReadRepos = personnelReadRepos;
    private readonly IReadRepos<Core.Entities.Department> _departmentReadRepos = departmentReadRepos;

    public async Task<Result> Handle(CreatePersonnelRequest request, CancellationToken cancellationToken)
    {
        if(!await _departmentReadRepos.ExistsAsync(d => d.Id == request.DepartmentId, cancellationToken))
        {
            return Result.NotFound($"No department found with the ID '{request.DepartmentId}'.");
        }

        if(await _personnelReadRepos.ExistsAsync(p => p.Email!.ToLower().Trim() == request.Email.ToLower().Trim(), cancellationToken))
        {
            return  Result.Conflict($"The personnel with the email '{request.Email}' already exists.");
        }

        var newPersonnel = new Core.Entities.Personnel(
            request.FirstName,
            request.LastName,
            request.Email,
            request.JobTitle,
            request.DepartmentId);

        await _unitOfWork.PersonnelWriteRepos.CreateAsync(newPersonnel, cancellationToken);
        await _unitOfWork.CommitAsync(cancellationToken);

        return Result.Success();
    }
}