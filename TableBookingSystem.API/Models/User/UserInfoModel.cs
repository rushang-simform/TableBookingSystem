using FluentValidation;
using MediatR;
using TableBookingSystem.Application.Features.User.Queries;

namespace TableBookingSystem.Models
{
    public class UserInfoModel
    {
        public string UserId { get; set; }
        public string EmailId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Password { get; set; }
        public int UserType { get; set; }
    }

    public class UserInfoModelValidator : AbstractValidator<UserInfoModel>
    {
        public UserInfoModelValidator(IMediator mediator)
        {
            RuleFor(x => x.EmailId)
                  .NotNull().WithMessage("Email Address is required")
                  .EmailAddress().WithMessage("Invalid Email Address")
                  .CustomAsync(async (emailId, context, cancelToken) =>
                  {
                      var isExists = await mediator.Send<bool>(new CheckUserExistsQuery { EmailId = emailId });
                      if (isExists)
                      {
                          context.AddFailure("Email is already in use");
                      }

                  });


            RuleFor(x => x.FirstName)
                .NotNull().WithMessage("First name is required");

            RuleFor(x => x.LastName)
                .NotNull().WithMessage("Last name is required");

            RuleFor(x => x.Password)
                .NotNull().WithMessage("Password is required")
                .Matches(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[^\da-zA-Z]).{8,15}$")
                .WithMessage("Password must match requirements: Minimum eight characters, at least one uppercase letter, one lowercase letter and one number");


            RuleFor(x => x.UserType)
                .NotNull().WithMessage("UserType is required");
        }
    }
}
