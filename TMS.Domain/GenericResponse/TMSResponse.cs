namespace TMS.Domain.GenericResponse;

public class TMSResponse
{
    public TMSResponse(string? message = null, Object? data = null, bool isCompleted = false, TMSException? exception = null)
    {
        this.Message = message;
        this.Data = data;
        this.IsCompleted = isCompleted;
        this.Exception = exception;
    }

    public TMSResponse() { }

    public string? Message { get; set; }
    public Object? Data { get; set; }
    public bool IsCompleted { get; set; }
    public TMSException? Exception { get; set; }
}


public class TMSException : Exception
{
    public TMSException(string message) : base(message: message) { }

    public TMSException() { }
}
