using Ardalis.Result;
using MediatR;

namespace CompanyDirectory.UseCases.Location.Update;

public sealed record UpdateLocationRequest(int Id, string Name) : IRequest<Result>;