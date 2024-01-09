namespace DOMAIN.Utilities;

public class Response<T>
{
    public T? Data { get; set; } = default;
    public bool IsSuccess { get; set; } = false;
    public string Message { get; set; } = string.Empty;
    public List<string> Errors { get; set; } = new List<string>();
}

public class Response
{
    public bool IsSuccess { get; set; } = false;
    public string Message { get; set; } = string.Empty;
    public List<string> Errors { get; set; } = new List<string>();
}