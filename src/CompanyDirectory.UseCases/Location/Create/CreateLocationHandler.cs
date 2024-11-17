using Ardalis.Result;
using CompanyDirectory.Core.Interface;
using MediatR;

namespace CompanyDirectory.UseCases.Location.Create;

internal sealed class CreateLocationHandler(
    IUnitOfWork unitOfWork,
    IReadRepos<Core.Entities.Location> locationReadRepos) : IRequestHandler<CreateLocationRequest, Result>
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    private readonly IReadRepos<Core.Entities.Location> _locationReadRepos = locationReadRepos;

    public async Task<Result> Handle(CreateLocationRequest request, CancellationToken cancellationToken)
    {
        var existLocation = _locationReadRepos
            .Find(location => location.Name == request.Name)
            .SingleOrDefault();

        if (existLocation is not null && existLocation.Name!.Trim().Equals(request.Name.Trim(), StringComparison.OrdinalIgnoreCase))
            return Result.Conflict($"The location with the name '{request.Name}' already exists.");

        var newLocation = new Core.Entities.Location(request.Name);

        await _unitOfWork.LocationWriteRepos.CreateAsync(newLocation, cancellationToken);
        await _unitOfWork.CommitAsync(cancellationToken);

        return Result.Success();
    }
}