using System.ComponentModel.DataAnnotations;

namespace ValidatorProject;

public class Validator<T> : IValidator<T>
{
    internal readonly ValidatorResult result = new ();


    public IValidator<T> IsNotNull(T entity)
    {
        if (entity == null)
        {
          result.Errors.Add($"Entity can not be null: {typeof(T)}");  
        }
        return this;
    }

    public ValidatorResult Validate()
    {
        return result.Errors.Any()
            ? ValidatorResult.Fail(result.Errors)
            : ValidatorResult.Success(); ;
    }
}