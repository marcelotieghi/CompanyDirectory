using Ardalis.Result;
using CompanyDirectory.Core.Interface;
using MediatR;

namespace CompanyDirectory.UseCases.Location.GetById;

internal sealed class GetByIdLocationHandler(
    IReadRepos<Core.Entities.Location> locationReadRepos) : IRequestHandler<GetByIdLocationRequest, Result<LocationDto>>
{
    private readonly IReadRepos<Core.Entities.Location> _locationReadRepos = locationReadRepos;

    public async Task<Result<LocationDto>> Handle(GetByIdLocationRequest request, CancellationToken cancellationToken)
    {
        var existLocation = _locationReadRepos
            .Find(location => location.Id == request.Id)
            .SingleOrDefault();

        if(existLocation is null)
            return await Task.FromResult(Result.NotFound($"No location found with the ID '{request.Id}'."));

        var locationDto = new LocationDto(
            existLocation.Id, 
            existLocation.Name ?? string.Empty);

        return await Task.FromResult(Result.Success(locationDto));
    }
}