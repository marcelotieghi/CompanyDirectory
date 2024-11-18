using System.Text.Json.Serialization;
using CompanyDirectory.UseCases.Department;

namespace CompanyDirectory.UseCases.Personnel;

public sealed record PersonelDto(
    int Id,
    string FirstName,
    string LastName,
    string Email,
    string JobTitle,
    [property: JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    DepartmentDto Department);