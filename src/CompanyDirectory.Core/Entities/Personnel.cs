using CompanyDirectory.Core.Abstract;

namespace CompanyDirectory.Core.Entities;

public sealed class Personnel(
    string firstName,
    string lastName,
    string email,
    string jobTitle,
    int departmentId) : BaseEntity
{
    public string? FirstName { get; set; } = firstName;
    public string? LastName { get; set; } = lastName;
    public string? Email { get; set; } = email;
    public string? JobTitle { get; set; } = jobTitle;

    public int DepartmentId { get; set; } = departmentId;
    public Department? Department { get; set; }

    public void UpdatePersonnel(
        string fisrtName,
        string lastName,
        string email,
        string jobTitle,
        int departmentId)
    {
        FirstName = fisrtName;
        LastName = lastName;
        Email = email;
        JobTitle = jobTitle;
        DepartmentId = departmentId;
    }
}