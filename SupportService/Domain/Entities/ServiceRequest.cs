namespace Domain.Entities;

public class ServiceRequest
{
    public int Id { get; set; }

    public string Title { get; set; }

    public string Description { get; set; }

    public DateTime CreatedDate { get; set; } = DateTime.UtcNow;

    public DateTime? UpdatedDate { get; set; }

    public int GroupId { get; set; }

    #region Navigation Properties

    public int CreatedById { get; set; }

    public User CreatedBy { get; set; }

    public int? AppointedId { get; set; }

    public User Appointed { get; set; }

    public Status Status { get; set; }

    public List<Comment> Comments { get; set; }

    public Group? Group { get; set; }

    #endregion
}
