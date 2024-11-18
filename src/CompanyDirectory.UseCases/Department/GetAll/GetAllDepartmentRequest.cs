using Ardalis.Result;
using MediatR;

namespace CompanyDirectory.UseCases.Department.GetAll;

public sealed record GetAllDepartmentRequest : IRequest<Result<IEnumerable<DepartmentDto>>>;