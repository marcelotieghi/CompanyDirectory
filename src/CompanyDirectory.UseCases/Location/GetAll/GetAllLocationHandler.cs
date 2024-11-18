using Ardalis.Result;
using CompanyDirectory.Core.Interface;
using MediatR;

namespace CompanyDirectory.UseCases.Location.GetAll;

internal sealed class GetAllLocationHandler(
    IReadRepos<Core.Entities.Location> locationReadRepos) : IRequestHandler<GetAllLocationRequest, Result<IEnumerable<LocationDto>>>
{
    private readonly IReadRepos<Core.Entities.Location> _locationReadRepos = locationReadRepos;

    public async Task<Result<IEnumerable<LocationDto>>> Handle(GetAllLocationRequest request, CancellationToken cancellationToken)
    {
        var locations = _locationReadRepos.GetList();

        var locationsDto = locations
            .Select(location => new LocationDto(
                location.Id,
                location.Name ?? string.Empty))
            .AsEnumerable();

        return await Task.FromResult(Result.Success(locationsDto));
    }
}