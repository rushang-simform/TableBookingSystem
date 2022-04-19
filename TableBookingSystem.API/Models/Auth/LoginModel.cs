using FluentValidation;

namespace TableBookingSystem.Models
{
    public class LoginModel
    {
        public string EmailId { get; set; }
        public string Password { get; set; }
    }

    public class LoginModelValidator : AbstractValidator<LoginModel>
    {
        public LoginModelValidator()
        {
            RuleFor(x => x.EmailId)
                .NotNull().WithMessage("Email Address is required")
                .EmailAddress().WithMessage("Invalid Email Address");

            RuleFor(x => x.Password)
                .NotNull().WithMessage("Password is required");
        }
    }
}
