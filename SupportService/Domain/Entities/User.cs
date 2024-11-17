namespace Domain.Entities;

public class User
{
    public int Id { get; set; }

    public string Name { get; set; }

    public string Email { get; set; }

    public string Password { get; set; }

    public byte[] Salt { get; set; }

    public bool IsDeactivated { get; set; } = false;

    #region Navigation Properties

    public UserRole Role { get; set; }

    public List<Group> Groups { get; set; }

    public List<ServiceRequest> AppointedRequests { get; set; }

    #endregion
}
