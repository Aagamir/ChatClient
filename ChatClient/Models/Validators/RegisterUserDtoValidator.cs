using ChatClient.Entities;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatClient.Models.Validators
{
    public class RegisterUserDtoValidator : AbstractValidator<RegisterUserDto>
    {
        public RegisterUserDtoValidator(ChatDbContext dbContext)
        {
            RuleFor(x => x.Name)
               .Custom((value, context) =>
               {
                   if (dbContext.Users.Any(x => x.Name == value))
                   {
                       context.AddFailure("Name", "Name already used");
                   }
               });

            RuleFor(x => x.Email)
                .NotEmpty()
                .EmailAddress();
            RuleFor(x => x.Email)
                .Custom((value, context) =>
                {
                    if (dbContext.Users.Any(x => x.Email == value))
                    {
                        context.AddFailure("Email", "Email already used");
                    }
                });

            RuleFor(x => x.Password)
                .MinimumLength(6);

            RuleFor(x => x.ConfirmPassword)
                .Equal(x => x.Password);
        }
    }
}