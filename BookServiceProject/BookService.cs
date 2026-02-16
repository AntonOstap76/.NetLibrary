namespace BookServiceProject;
using ValidatorProject;
using ValidatorProject.Numbers;
public class BookService
{
    public void Test()
    {
        Validator<int> validator = new Validator<int>();
        validator.IsNotNull(5).LessThan(2,1).LessThan(4,5).Validate();
    }
}