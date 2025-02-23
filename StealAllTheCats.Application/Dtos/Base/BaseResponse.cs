using System.Text.Json.Serialization;

namespace StealAllTheCats.Application.Dtos.Base;
public class BaseResponse<T> where T : class
{
    public BaseResponse() { }
    public BaseResponse(Guid correlationId) : this()
    {
        CorrelationId = correlationId;
    }

    [JsonPropertyOrder(-2)]
    public Guid CorrelationId { get; set; }
    [JsonPropertyOrder(-1)]
    public T? Data { get; set; }
    public IList<ErrorInfo>? ErrorDetails { get; set; }
    public bool IsSuccess { get; set; } = true;
}