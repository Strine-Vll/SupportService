namespace Application.Exceptions;

public class ServiceRequestNotFoundException : Exception
{
    public int ServiceRequestId { get; set; }

    public ServiceRequestNotFoundException() : base("Заявок по вашему запросу не найдено") { }

    public ServiceRequestNotFoundException(int serviceRequestId) : base($"Заявка с id = '{serviceRequestId}' не найден")
    {
        ServiceRequestId = serviceRequestId;
    }
}
