using Ardalis.Result;
using MediatR;

namespace CompanyDirectory.UseCases.Personnel.GetAll;

public sealed record GetAllPersonnelRequest : IRequest<Result<IEnumerable<PersonelDto>>>;