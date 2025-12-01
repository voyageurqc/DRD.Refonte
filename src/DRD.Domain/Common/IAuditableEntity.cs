namespace DRD.Domain.Common;

public interface IAuditableEntity
{
    public DateTime CreationDate { get; set; }
    public string? CreatedBy { get; set; }
    public DateTime ModificationDate { get; set; }
    public string? UpdatedBy { get; set; }
    public bool IsActive { get; set; }
}