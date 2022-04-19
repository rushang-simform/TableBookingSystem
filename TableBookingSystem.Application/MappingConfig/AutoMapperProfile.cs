using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using TableBookingSystem.Application.DTOs;
using TableBookingSystem.Application.DTOs.Restaurant;
using TableBookingSystem.Application.DTOs.RestaurantCompany;
using TableBookingSystem.Application.DTOs.User;
using TableBookingSystem.Application.Features.User.Commands;
using TableBookingSystem.Domain.Entities.Restaurant;
using TableBookingSystem.Entities.Domain.RestaurantCompany;

namespace TableBookingSystem.Application.MappingConfig
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            MapUserDtos();
            MapRestaurantCompanyDtos();
            MapRestaurantDtos();
        }
        public void MapUserDtos()
        {
            CreateMap<TableBookingSystem.Domain.Entities.User.UserInfo, BasicUserInfoDto>()
                .ForMember(x => x.UserType, x => x.MapFrom(x => x.UserRoleId))
                .ReverseMap();
        }

        public void MapRestaurantCompanyDtos()
        {
            CreateMap<RestaurantCompany, RestaurantCompanyDto>()
                .ReverseMap();
        }
        public void MapRestaurantDtos()
        {
            CreateMap<Restaurant, RestaurantDto>()
                .ReverseMap();
        }
    }
}
