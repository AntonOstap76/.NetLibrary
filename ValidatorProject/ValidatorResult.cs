namespace ValidatorProject;

public class ValidatorResult
{
    public bool IsValid { get; private set; }
    public List<string>? Errors { get; private set; }

    public ValidatorResult()
    {
    }

    public static ValidatorResult Success()
    {
        return new ValidatorResult()
        {
            IsValid = true,
            Errors = null
        };
    }

    public static ValidatorResult Fail(List<string> errors)
    {
        return new ValidatorResult()
        {
            IsValid = false,
            Errors = errors,
        };
    }
}