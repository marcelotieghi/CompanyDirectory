using Ardalis.Result;
using MediatR;

namespace CompanyDirectory.UseCases.Department.SoftDelete;

public sealed record SoftDeleteDepartmentRequest(int[] Ids) : IRequest<Result>;