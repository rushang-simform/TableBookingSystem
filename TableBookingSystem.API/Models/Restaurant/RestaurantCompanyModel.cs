using FluentValidation;
using System;

namespace TableBookingSystem.Models
{
    public class RestaurantCompanyModel
    {
        public string RestaurantCompanyName { get; set; }
        public string Description { get; set; }
        public string Phone { get; set; }
        public string WebSite { get; set; }
    }
    public class RestaurantCompanyModelValidator : AbstractValidator<RestaurantCompanyModel>
    {
        public RestaurantCompanyModelValidator()
        {
            RuleFor(x=>x.RestaurantCompanyName)
                .NotEmpty().NotNull().WithMessage("RestaurantCompanyName is required")
                .Matches(@"^[a-zA-Z0-9\s,]*$").WithMessage("RestaurantCompanyName must contains only character and numbers");

            RuleFor(x => x.Description).NotNull().NotNull().WithMessage("Description is required");

            RuleFor(x => x.Phone).NotNull().NotEmpty().WithMessage("Phone is required");

            RuleFor(x => x.WebSite).NotNull().NotNull().WithMessage("WebSite is requried");
        }
    }

}
