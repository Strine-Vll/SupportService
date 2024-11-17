namespace Domain.Entities;

public class Group
{
    public int Id { get; set; }

    public string Name { get; set; }

    #region Navigation Properties

    public List<User> User { get; set; }

    public List<ServiceRequest> ServiceRequests { get; set; }

    #endregion
}
