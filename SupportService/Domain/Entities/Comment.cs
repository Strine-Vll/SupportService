namespace Domain.Entities;

public class Comment
{
    public int Id { get; set; }

    public string Message { get; set; }

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    #region Navigation Properties

    public int? ServiceRequestId { get; set; }

    public ServiceRequest ServiceRequest { get; set; }

    public int? CreatedById { get; set; }

    public User CreatedBy { get; set; }

    public List<Attachment> Attachments { get; set; }

    #endregion
}
