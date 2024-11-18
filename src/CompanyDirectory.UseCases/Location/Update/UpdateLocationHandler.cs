using Ardalis.Result;
using CompanyDirectory.Core.Interface;
using MediatR;

namespace CompanyDirectory.UseCases.Location.Update;

internal sealed class UpdateLocationHandler(
    IUnitOfWork unitOfWork,
    IReadRepos<Core.Entities.Location> locationReadRepos) : IRequestHandler<UpdateLocationRequest, Result>
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    private readonly IReadRepos<Core.Entities.Location> _locationReadRepos = locationReadRepos;

    public async Task<Result> Handle(UpdateLocationRequest request, CancellationToken cancellationToken)
    {
        var existLocation = await _locationReadRepos
            .GetByKeyAsync(location => location.Id == request.Id, cancellationToken);

        if (existLocation is null)
            return Result.NotFound($"No location found with the ID '{request.Id}'.");
        
        if (existLocation.Name!.Trim().Equals(request.Name.Trim(), StringComparison.OrdinalIgnoreCase))
            return Result.Conflict($"The location with the name '{request.Name}' already exists.");
        
        existLocation.UpdateLocation(request.Name);

        await _unitOfWork.LocationWriteRepos.UpdateAsync(existLocation, cancellationToken);
        await _unitOfWork.CommitAsync(cancellationToken);

        return Result.Success();       
    }
}