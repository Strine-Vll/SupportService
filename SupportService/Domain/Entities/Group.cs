namespace Domain.Entities;

public class Group
{
    public int Id { get; set; }

    public string Name { get; set; }

    #region Navigation Properties

    public List<User> Users { get; set; } = new List<User>();

    public List<ServiceRequest> ServiceRequests { get; set; }

    #endregion
}
