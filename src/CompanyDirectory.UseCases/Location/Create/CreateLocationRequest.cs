using Ardalis.Result;
using MediatR;

namespace CompanyDirectory.UseCases.Location.Create;

public sealed record CreateLocationRequest(string Name) : IRequest<Result>;