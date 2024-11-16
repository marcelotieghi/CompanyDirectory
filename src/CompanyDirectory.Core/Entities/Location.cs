using CompanyDirectory.Core.Abstract;

namespace CompanyDirectory.Core.Entities;

public sealed class Location(string name) : BaseEntity
{
    public string? Name { get; private set; } = name;

    public IReadOnlyCollection<Department> DepartmentList = new List<Department>().AsReadOnly();

    public void UpdateLocation(string name)
    {
        Name = name;
    }
}