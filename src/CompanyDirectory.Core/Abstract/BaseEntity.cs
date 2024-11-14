namespace CompanyDirectory.Core.Abstract;

public abstract class BaseEntity 
{
    public int Id { get; init; }     
    
    //Audit Entity
    public int CreatedBy { get; set; } 
    public DateTime CreatedOn { get; set; } = DateTime.Now;
    public int UpdatedBy { get; set; }
    public DateTime? UpdatedOn { get; set; }
    public bool IsDeleted { get; set; } = false;
    public DateTime? DeletedOn { get; set; } 
}