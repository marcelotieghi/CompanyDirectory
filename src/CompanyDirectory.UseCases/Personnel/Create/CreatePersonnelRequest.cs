using Ardalis.Result;
using CompanyDirectory.UseCases.Department;
using MediatR;

namespace CompanyDirectory.UseCases.Personnel.Create;

public sealed record CreatePersonnelRequest(
    string FirstName,
    string LastName,
    string Email,
    string JobTitle,
    int DepartmentId) : IRequest<Result>;