using Ardalis.Result;
using CompanyDirectory.Core.Interface;
using MediatR;

namespace CompanyDirectory.UseCases.Personnel.SoftDelete;

internal sealed class SoftDeletePersonnelHandler(
    IUnitOfWork unitOfWork,
    IReadRepos<Core.Entities.Personnel> personnelReadRepos) : IRequestHandler<SoftDeletePersonnelRequest, Result>
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    private readonly IReadRepos<Core.Entities.Personnel> _personnelReadRepos = personnelReadRepos;

    public async Task<Result> Handle(SoftDeletePersonnelRequest request, CancellationToken cancellationToken)
    {
        if(!await _personnelReadRepos.ExistsAsync(p => request.Ids.Contains(p.Id)))
        {
            return Result.NotFound($"No personnel found.");
        }

        await _unitOfWork.DepartmentWriteRepos.SoftDeleteAsync(request.Ids, cancellationToken);

        return Result.Success();
    }
}