using Ardalis.Result;
using MediatR;

namespace CompanyDirectory.UseCases.Location.GetById;

public sealed record GetByIdLocationRequest(int Id) : IRequest<Result<LocationDto>>;