namespace ValidatorProject;

public interface IValidator<T>
{
    IValidator<T> IsNotNull (T entity);
    ValidatorResult Validate();
}