using AutoMapper;
using TableBookingSystem.Models;
using TableBookingSystem.Application.Features.User.Commands;
using TableBookingSystem.Application.Features.RestaurantCompany.Commands;
using TableBookingSystem.Application.Features.Restaurant.Commands;
using System;

namespace TableBookingSystem.Models
{
    public class ModelsMappingProfile : Profile
    {
        public ModelsMappingProfile()
        {
            CreateMap<SignUpModel, CreateUserCommand>();
            CreateMap<UserInfoModel, CreateUserCommand>();
            CreateMap<RestaurantCompanyModel, CreateRestaurantCompanyCommand>();
            CreateMap<RestaurantCompanyModel, UpdateRestaurantCompanyCommand>();

            CreateMap<RestaurantModel, CreateRestaurantCommand>()
                .ForMember(x => x.Latitude, x => x.MapFrom(src => decimal.Parse(src.Latitude)))
                .ForMember(x => x.Longitude, x => x.MapFrom(src => decimal.Parse(src.Longitude)));

            CreateMap<RestaurantModel, UpdateRestaurantCommand>()
                .ForMember(x => x.Latitude, x => x.MapFrom(src => decimal.Parse(src.Latitude)))
                .ForMember(x => x.Longitude, x => x.MapFrom(src => decimal.Parse(src.Longitude)));
        }
    }
}
