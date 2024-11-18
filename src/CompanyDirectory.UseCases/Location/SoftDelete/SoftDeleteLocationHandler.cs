using Ardalis.Result;
using CompanyDirectory.Core.Interface;
using MediatR;

namespace CompanyDirectory.UseCases.Location.SoftDelete;

internal sealed class SoftDeleteLocationHandler(
    IUnitOfWork unitOfWork,
    IReadRepos<Core.Entities.Location> locatinoReadRepos) : IRequestHandler<SoftDeleteLocationRequest, Result>
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    private readonly IReadRepos<Core.Entities.Location> _locatinoReadRepos = locatinoReadRepos;

    public async Task<Result> Handle(SoftDeleteLocationRequest request, CancellationToken cancellationToken)
    {
        if(!await _locatinoReadRepos.ExistsAsync(location => request.Ids.Contains(location.Id), cancellationToken))
            return Result.Conflict($"No location found!");

        await _unitOfWork.LocationWriteRepos.SoftDeleteAsync(request.Ids, cancellationToken);

        return Result.Success();
    }
}