using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TableBookingSystem.Web.Authorization.Attributes;
using TableBookingSystem.Web.Controllers.Common;
using TableBookingSystem.Application.Features.Restaurant.Commands;
using TableBookingSystem.Application.Features.Restaurant.Queries;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TableBookingSystem.Web.Models;

namespace TableBookingSystem.Web.Controllers.Restaurant
{
    [SuperAdminRole]
    public class RestaurantController : BaseController
    {
        public RestaurantController()
        {
        }

        [HttpGet("{RestaurantId}")]
        public async Task<IActionResult> Get(Guid RestaurantId)
        {
            var restaurantQuery = new GetRestaurantQuery() { RestaurantId = RestaurantId };

            var response = await Mediator.Send(restaurantQuery);

            if (response.IsSuccess)
            {
                return response.Status == Application.DTOs.Response.QueryReponseStatus.Found
                    ? Ok(response.Data)
                    : NotFound();
            }
            else
            {
                return BadRequest(response.Message);
            }
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] RestaurantModel restaurant)
        {

            var restaurantCommand = Mapper.Map<CreateRestaurantCommand>(restaurant);
            restaurantCommand.CreatedBy = GetAuthenticatedUserIdGuid();

            var response = await Mediator.Send(restaurantCommand);

            return response.IsSuccess ? Ok(response.Data) : BadRequest(response.Message);
        }

        [HttpPut("{RestaurantId}")]
        public async Task<IActionResult> Update(Guid RestaurantId, [FromBody] RestaurantModel restaurant)
        {
            var restaurantCommand = Mapper.Map<UpdateRestaurantCommand>(restaurant);

            restaurantCommand.RestaurantId = RestaurantId;
            restaurantCommand.UpdatedBy = GetAuthenticatedUserIdGuid();

            var response = await Mediator.Send(restaurantCommand);

            return response.IsSuccess ? Ok(response.Data) : BadRequest(response.Message);
        }

        [HttpDelete("{RestaurantId}")]
        public async Task<IActionResult> Delete(Guid RestaurantId)
        {
            await Mediator.Send(new DeleteRestaurantCommand() { RestaurantId = RestaurantId });
            return NoContent();
        }

        [HttpGet("{RestaurantId}/users")]
        public async Task<IActionResult> GetUsers(Guid RestaurantId)
        {
            var response = await Mediator.Send(new GetRestaurantUsersCommand(RestaurantId));
            if (response.IsSuccess && response.Status == Application.DTOs.Response.QueryReponseStatus.Found)
            {
                return Ok(response.Data);
            }
            else
            {
                return BadRequest(response.Message);
            }

        }

        [HttpPost("{RestaurantId}/users")]
        public async Task<IActionResult> AssignUsers([FromRoute] Guid RestaurantId, [FromBody] List<Guid> users)
        {
            var response = await Mediator.Send(new AssignUsersCommand(RestaurantId, users, GetAuthenticatedUserIdGuid()));

            return response.IsSuccess ? Ok() : BadRequest(response.Message);
        }

        [HttpDelete("{RestaurantId}/users/{UserId}")]
        public async Task<IActionResult> RemoveUser([FromRoute] Guid RestaurantId, [FromRoute] Guid UserId)
        {
            var response = await Mediator.Send(new RemoveUserCommand(RestaurantId, UserId));
            return response.IsSuccess ? Ok() : BadRequest();
        }
    }
}
