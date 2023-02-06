namespace Aspose.Core.Models;

public class ExecutionFailure : ICloneable
{
    public string Message { get; set; }
    public string Code { get; set; }

    public object Clone()
    {
        return MemberwiseClone();
    }
}

public class ExecutionResult
{
    public bool Success => Errors.Count == 0;

    public List<ExecutionFailure> Errors { get; } = new();

    public void AddError(string message, string code = null)
    {
        Errors.Add(new ExecutionFailure
        {
            Message = message,
            Code = code,
        });
    }
}

public class ExecutionResult<T> : ExecutionResult
{
    public T Result { get; set; }
}