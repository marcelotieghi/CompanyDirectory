using CompanyDirectory.Core.Abstract;

namespace CompanyDirectory.Core.Entities;

public sealed class Department(
    string name,
    int locationId) : BaseEntity
{
    public string? Name { get; private set; } = name;
    
    public int LocationId { get; private set; } = locationId;
    public Location? Location { get; private set; }

    public IReadOnlyCollection<Personnel> PersonnelList = new List<Personnel>().AsReadOnly();

    public void UpdateDepartment(string name, int locationId)
    {
        Name = name;
        LocationId = locationId;
    }
 }