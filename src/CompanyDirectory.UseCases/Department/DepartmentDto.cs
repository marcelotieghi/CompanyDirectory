using System.Text.Json.Serialization;
using CompanyDirectory.UseCases.Location;

namespace CompanyDirectory.UseCases.Department;

public sealed record DepartmentDto(
    int Id,
    string Name,
    [property: JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    LocationDto Location);