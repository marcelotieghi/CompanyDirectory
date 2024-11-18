using Ardalis.Result;
using MediatR;

namespace CompanyDirectory.UseCases.Personnel.GetById;

public sealed record GetByIdPersonnelRequest(int Id) : IRequest<Result<PersonelDto>>;