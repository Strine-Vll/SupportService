namespace Domain.Entities;

public class Notification
{
    public int Id { get; set; }

    public string Title { get; set; }

    public string Message { get; set; }

    public bool IsRead { get; set; } = false;

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    #region Navigation Properties

    public int UserId { get; set; }

    public User User { get; set; }

    #endregion
}
