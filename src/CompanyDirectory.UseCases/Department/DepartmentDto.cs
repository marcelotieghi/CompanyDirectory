using CompanyDirectory.UseCases.Location;

namespace CompanyDirectory.UseCases.Department;

public sealed record DepartmentDto(
    int Id,
    string Name,
    LocationDto LocationDto);