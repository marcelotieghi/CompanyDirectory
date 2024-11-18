using Ardalis.Result;
using CompanyDirectory.Core.Interface;
using MediatR;

namespace CompanyDirectory.UseCases.Personnel.Update;

internal sealed class UpdatePersonnelHandler(
    IUnitOfWork unitOfWork,
    IReadRepos<Core.Entities.Personnel> personnelReadRepos,
    IReadRepos<Core.Entities.Department> departmentReadRepos) : IRequestHandler<UpdatePersonnelRequest, Result>
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    private readonly IReadRepos<Core.Entities.Personnel> _personnelReadRepos = personnelReadRepos;
    private readonly IReadRepos<Core.Entities.Department> _departmentReadRepos = departmentReadRepos;

    public async Task<Result> Handle(UpdatePersonnelRequest request, CancellationToken cancellationToken)
    {
        if(!await _departmentReadRepos.ExistsAsync(d => d.Id == request.DepartmentId, cancellationToken))
        {
            return Result.NotFound($"No department found with the ID '{request.DepartmentId}'.");
        }

        var personnel = await _personnelReadRepos.GetByKeyAsync(p => p.Id == request.Id, cancellationToken);

        if(personnel is null)
        {
            return Result.NotFound($"No personnel found with the ID '{request.Id}'.");
        }

        if(personnel.Email!.ToLower().Trim() == request.Email.ToLower().Trim())
        {
            return Result.Conflict($"The personnel with the email '{request.Email}' already exists.");
        }

        personnel.UpdatePersonnel(
            request.FirstName,
            request.LastName,
            request.Email,
            request.JobTitle,
            request.DepartmentId);

        await _unitOfWork.PersonnelWriteRepos.UpdateAsync(personnel, cancellationToken);
        await _unitOfWork.CommitAsync(cancellationToken);

        return Result.Success();
    }
}