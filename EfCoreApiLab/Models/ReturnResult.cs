namespace EfCoreApiLab.Models;

public class ReturnResult<T>
{
    public bool IsSuccess { get; set; } = true;
    public T? Result { get; set; }
    public List<string> ErrorMessage { get; set; } = new();
}