using System;
using FluentValidation;

namespace TableBookingSystem.Web.Models
{
    public class RestaurantModel
    {
        public string RestaurantName { get; set; }
        public Guid RestaurantCompanyId { get; set; }
        public string Description { get; set; }
        public string Website { get; set; }
        public string Phone { get; set; }
        public string StreetAddress { get; set; }
        public string State { get; set; }
        public string Country { get; set; }
        public TimeSpan OpeningTime { get; set; }
        public TimeSpan ClosingTime { get; set; }
        public string Latitude { get; set; }
        public string Longitude { get; set; }
    }

    public class RestaurantValidator : AbstractValidator<RestaurantModel>
    {
        public RestaurantValidator()
        {
            RuleFor(x => x.RestaurantName)
                .NotEmpty().NotNull().WithMessage("RestaurantName is required.")
                .Matches(@"^[a-zA-Z0-9\s,]*$").WithMessage("RestaurantName must contains only character and numbers");

            RuleFor(x => x.RestaurantCompanyId)
                .NotNull().WithMessage("RestaurantCompanyId is required.");

            RuleFor(x => x.Description)
                .NotNull().WithMessage("Restaurant Description is required.");

            RuleFor(x => x.Phone)
                .NotNull().NotEmpty().WithMessage("Phone is required");

            RuleFor(x => x.StreetAddress)
                .NotEmpty().NotNull().WithMessage("StreetAddress is required");

            RuleFor(x => x.State)
                .NotEmpty().NotNull().WithMessage("State is requried");

            RuleFor(x => x.Country)
                .NotNull().NotEmpty().WithMessage("Country is required");

            RuleFor(x => x.OpeningTime)
                .NotNull().WithMessage("OpeningTime is required");

            RuleFor(x => x.ClosingTime)
                .NotNull().WithMessage("Closingtime is required");
                

            RuleFor(x => x.Latitude)
                .Matches(@"^(\+|-)?(?:90(?:(?:\.0{1,6})?)|(?:[0-9]|[1-8][0-9])(?:(?:\.[0-9]{1,6})?))$")
                .WithMessage("Invalid Latitude value");

            RuleFor(x => x.Longitude)
                .Matches(@"^(\+|-)?(?:180(?:(?:\.0{1,6})?)|(?:[0-9]|[1-9][0-9]|1[0-7][0-9])(?:(?:\.[0-9]{1,6})?))$")
                .WithMessage("Invalid Longtitude value");
                
        }
    }
}
