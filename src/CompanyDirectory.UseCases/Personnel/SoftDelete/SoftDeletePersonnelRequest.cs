using Ardalis.Result;
using MediatR;

namespace CompanyDirectory.UseCases.Personnel.SoftDelete;

public sealed record SoftDeletePersonnelRequest(int[] Ids) : IRequest<Result>;