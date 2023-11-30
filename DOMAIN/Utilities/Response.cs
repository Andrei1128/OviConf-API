namespace DOMAIN.Utilities;

public class Response<T>
{
    public T? Data { get; set; } = default;
    public bool IsSucces { get; set; } = false;
    public string Message { get; set; } = string.Empty;
}

public class Response
{
    public bool IsSucces { get; set; } = false;
    public string Message { get; set; } = string.Empty;
}