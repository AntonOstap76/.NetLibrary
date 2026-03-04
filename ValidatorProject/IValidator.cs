namespace ValidatorProject;

public interface IValidator<T>
{
    IValidator<T> IsNotNull (T entity);
    ValidatorResult Validate();
    IValidator<T> AddError(string errorMessage);
}