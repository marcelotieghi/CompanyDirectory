namespace CompanyDirectory.Core.Abstract;

public abstract class BaseEntity
{
    public int Id { get; init; }

    //Audit Entity
    public int CreatedBy { get; set; } = 0;
    public DateTime CreatedOn { get; set; } = DateTime.Now;
    public int UpdatedBy { get; set; } = 0;
    public DateTime? UpdatedOn { get; set; } = DateTime.Now;
    public bool IsDeleted { get; set; } = false;
}