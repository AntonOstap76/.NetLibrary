namespace ValidatorProject;

public class OperationResult<T>
{
    public OperationStatus Status { get; private set; }
    public T Result { get; private set; }
    public string Operation { get; private set; }
    public List<string>? Errors { get; private set; }

    private OperationResult()
    {
    }

    public static OperationResult<T> Success(T result, string operation)
    {
        return new OperationResult<T>()
        {
            Status = OperationStatus.Ok,
            Result = result,
            Operation = operation,
            Errors = null
        };
    }

    public static OperationResult<T> Failed(List<string> errors, string operation)
    {
        return new OperationResult<T>()
        {
            Status = OperationStatus.Failed,
            Result = default,
            Operation = operation,
            Errors = errors,
        };
    }
}