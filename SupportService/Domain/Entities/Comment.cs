namespace Domain.Entities;

public class Comment
{
    public int Id { get; set; }

    public string Message { get; set; }

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    #region Navigation Properties

    public User CreatedBy { get; set; }

    #endregion
}
