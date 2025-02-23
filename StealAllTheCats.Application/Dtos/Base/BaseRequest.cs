namespace StealAllTheCats.Application.Dtos.Base;

public class BaseRequest
{
    public Guid CorrelationId { get; set; } = Guid.NewGuid();
}
