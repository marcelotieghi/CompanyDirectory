using Ardalis.Result;
using MediatR;

namespace CompanyDirectory.UseCases.Department.Update;

public sealed record UpdateDepartmentRequest(
    int Id, 
    string Name, 
    int LocationId) : IRequest<Result>;