namespace DOMAIN.Utilities;

public class Response<T>
{
    private T? Data { get; set; }
    private bool IsSucces { get; set; } = false;
    private string Message { get; set; } = string.Empty;
}
