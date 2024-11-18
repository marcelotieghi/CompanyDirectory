using Ardalis.Result;
using MediatR;

namespace CompanyDirectory.UseCases.Department.Create;

public sealed record CreateDepartmentRequest(string Name, int LocationId) : IRequest<Result>;