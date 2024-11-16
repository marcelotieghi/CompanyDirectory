using Ardalis.Result;
using MediatR;

namespace CompanyDirectory.UseCases.Location.SoftDelete;

public sealed record SoftDeleteLocationRequest(int[] Ids) : IRequest<Result>;