using Ardalis.Result;
using MediatR;

namespace CompanyDirectory.UseCases.Location.GetAll;

public sealed record GetAllLocationRequest : IRequest<Result<IEnumerable<LocationDto>>>;