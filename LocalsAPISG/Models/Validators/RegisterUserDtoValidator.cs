using FluentValidation;
using LocalsAPISG.Entities;
using System.Linq;

namespace LocalsAPISG.Models.Validators
{
    public class RegisterUserDtoValidator : AbstractValidator<RegisterUserDto>
    {
        public RegisterUserDtoValidator(LocalsDbContext dbContext) //walidacja unikalnosci maila
        {
            RuleFor(a => a.Email)
                .NotEmpty()
                .EmailAddress();

            RuleFor(x => x.Password).MinimumLength(6);

            RuleFor(x => x.ConfirmPassword).Equal(e => e.Password);

            RuleFor(x => x.Email)
                .Custom((value, context) =>
                {
                    var email = dbContext.Users.Any(u => u.Email == value);
                    if (email)
                    {
                        context.AddFailure("Email", "Email is taken.");
                    }
                });
        }
    }
}
