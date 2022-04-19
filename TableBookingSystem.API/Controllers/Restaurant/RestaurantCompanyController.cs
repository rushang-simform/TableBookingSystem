using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TableBookingSystem.API.Controllers.Common;
using TableBookingSystem.Application.Repository;
using TableBookingSystem.Application.Features.RestaurantCompany.Commands;
using System.Threading.Tasks;
using TableBookingSystem.Application.DTOs.Response;
using TableBookingSystem.Application.DTOs.RestaurantCompany;
using System;
using Microsoft.AspNetCore.Authorization;
using TableBookingSystem.Application.Features.RestaurantCompany.Queries;
using TableBookingSystem.API.Authorization.Attributes;
using TableBookingSystem.Models;

namespace TableBookingSystem.API.Controllers.Restaurant
{
    [SuperAdminRole]
    public class RestaurantCompanyController : BaseController
    {
        public RestaurantCompanyController()
        {

        }

        [HttpGet("{RestaurantCompanyId}")]
        public async Task<IActionResult> Get([FromRoute] Guid RestaurantCompanyId)
        {
            var response = await Mediator.Send<GenericQueryResponse<RestaurantCompanyDto>>(new GetRestaurantCompanyQuery { RestaurantCompanyId = RestaurantCompanyId });

            if (response.IsSuccess)
            {
                if (response.Status == QueryReponseStatus.Found)
                {
                    return Ok(response.Data);
                }
                else
                {
                    return NotFound();
                }
            }
            else
            {
                return BadRequest(response.Message);
            }
        }

        [HttpGet("{RestaurantCompanyId}/restaurants")]
        public async Task<IActionResult> GetRestaurants([FromRoute]Guid RestaurantCompanyId) 
        {
            var response = await Mediator.Send(new GetRestaurantsByCompanyQuery(RestaurantCompanyId));

            if (response.IsSuccess)
            {
                return Ok(response.Data);
            }
            else
            {
                return BadRequest(response.Message);
            }

        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] RestaurantCompanyModel restaurantCompany)
        {
            var createRestaurantCommand = Mapper.Map<CreateRestaurantCompanyCommand>(restaurantCompany);

            createRestaurantCommand.CreatedBy = Guid.Parse(GetAuthenticatedUserId());

            var response = await Mediator.Send<GenericResponse<RestaurantCompanyDto>>(createRestaurantCommand);

            return response.IsSuccess ? Ok(response.Data) : BadRequest(response.Message);
        }

        [HttpPut("{RestaurantCompanyId}")]
        public async Task<IActionResult> Update([FromRoute] Guid RestaurantCompanyId, [FromBody] RestaurantCompanyModel restaurantCompany)
        {
            var updateResturantCommand = Mapper.Map<UpdateRestaurantCompanyCommand>(restaurantCompany);
            updateResturantCommand.RestaurantCompanyId = RestaurantCompanyId;
            updateResturantCommand.UpdatedBy = Guid.Parse(GetAuthenticatedUserId());

            var response = await Mediator.Send<GenericResponse<RestaurantCompanyDto>>(updateResturantCommand);


            return response.IsSuccess ? Ok(response.Data) : BadRequest(response.Message);
        }

        [HttpDelete("{RestaurantCompanyId}")]
        public async Task<IActionResult> Delete([FromRoute] Guid RestaurantCompanyId)
        {
            var deleteResturantCommand = new DeleteResturantCompanyCommand() { RestaurantCompanyId = RestaurantCompanyId };
            await Mediator.Send(deleteResturantCommand);
            return Ok();
        }
    }
}

