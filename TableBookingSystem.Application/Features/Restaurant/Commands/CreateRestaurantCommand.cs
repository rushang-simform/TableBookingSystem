using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using TableBookingSystem.Application.DTOs.Response;
using TableBookingSystem.Application.DTOs.Restaurant;
using TableBookingSystem.Application.Repository;
using TableBookingSystem.Domain.Enums;

namespace TableBookingSystem.Application.Features.Restaurant.Commands
{
    public class CreateRestaurantCommand : IRequest<GenericResponse<RestaurantDto>>
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
        public Decimal? Latitude { get; set; }
        public Decimal? Longitude { get; set; }
        public Guid CreatedBy { get; set; }
    }
    public class CreateRestaurantCommandHandler : IRequestHandler<CreateRestaurantCommand, GenericResponse<RestaurantDto>>
    {
        private readonly ILogger<CreateRestaurantCommandHandler> _logger;
        private readonly IMapper _mapper;
        private readonly IRestaurantRepository _restaurantRepository;

        public CreateRestaurantCommandHandler
        (
            ILogger<CreateRestaurantCommandHandler> logger,
            IMapper mapper,
            IRestaurantRepository restaurantRepository
        )
        {
            _logger = logger;
            _mapper = mapper;
            _restaurantRepository = restaurantRepository;
        }
        public async Task<GenericResponse<RestaurantDto>> Handle(CreateRestaurantCommand request, CancellationToken cancellationToken)
        {
            var response = new GenericResponse<RestaurantDto>() { IsSuccess = true };
            try
            {
                var restaurant = await _restaurantRepository.InsertAsync(new Domain.Entities.Restaurant.Restaurant()
                {
                    RestaurantCompanyId = request.RestaurantCompanyId,
                    RestaurantName = request.RestaurantName,
                    Description = request.Description,
                    Phone = request.Phone,
                    Website = request.Website,
                    StreetAddress = request.StreetAddress,
                    State = request.State,
                    Country = request.Country,
                    Latitude = request.Latitude,
                    Longitude = request.Longitude,
                    IsDeleted = false,
                    OpeningTime = request.OpeningTime,
                    ClosingTime = request.ClosingTime,
                    CurrentStatus = (int)RestaurantStatus.Closed,
                    CreatedBy = request.CreatedBy,
                    CreatedDate = DateTime.UtcNow,
                    UpdatedBy = request.CreatedBy,
                    UpdatedDate = DateTime.UtcNow
                });

                response.Data = _mapper.Map<RestaurantDto>(restaurant);
            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.Message = "Failed to create Restaurant";
                _logger.LogError(ex, "Error in creating records");
            }
            return response;
        }
    }
}
