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
        var location = await _locationReadRepos
            .GetByKeyAsync(l => l.Id == request.Id, cancellationToken);

        if(location is null)
        {
            return await Task.FromResult(Result.NotFound($"No location found with the ID '{request.Id}'."));
        }

        var locationDto = new LocationDto(
            location.Id, 
            location.Name ?? string.Empty);

        return await Task.FromResult(Result.Success(locationDto));
    }
}