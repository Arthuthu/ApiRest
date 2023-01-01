namespace ApiRest.Validators;

public class CustomerDtoValidator : AbstractValidator<UserDto>
{
	public CustomerDtoValidator()
	{
		RuleFor(x => x.Username).NotEmpty().MinimumLength(2);
        RuleFor(x => x.Password).NotEmpty().MinimumLength(3);
    }
}
