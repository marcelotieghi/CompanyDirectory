using Ardalis.Result;
using MediatR;

namespace CompanyDirectory.UseCases.Personnel.Update;

public sealed record UpdatePersonnelRequest(
    int Id,
    string FirstName,
    string LastName,
    string Email,
    string JobTitle,
    int DepartmentId) : IRequest<Result>;