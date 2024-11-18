using Ardalis.Result;
using MediatR;

namespace CompanyDirectory.UseCases.Department.GetById;

public sealed record GetByIdDepartmentRequest(int Id) : IRequest<Result<DepartmentDto>>;