using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TableBookingSystem.Web.Controllers.Common;
using TableBookingSystem.Application.Repository;
using TableBookingSystem.Application.Features.RestaurantCompany.Commands;
using System.Threading.Tasks;
using TableBookingSystem.Application.DTOs.Response;
using TableBookingSystem.Application.DTOs.RestaurantCompany;
using System;
using Microsoft.AspNetCore.Authorization;
using TableBookingSystem.Application.Features.RestaurantCompany.Queries;
using TableBookingSystem.Web.Authorization.Attributes;
using TableBookingSystem.Web.Models;

namespace TableBookingSystem.Web.Controllers.Restaurant
{
    [SuperAdminRole]
    public class RestaurantCompanyController : BaseController
    {
        public RestaurantCompanyController()
        {

        }
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var res = await Mediator.Send(new GetAllRestaurantCompanies());
            return View("Index", res.Data);
        }

        [HttpGet("Create")]
        public async Task<IActionResult> Create()
        {
            return View("CreateOrUpdate");
        }

        [HttpPost("Create")]
        public async Task<IActionResult> Create([FromForm] RestaurantCompanyModel restaurantCompany)
        {
            if (ModelState.IsValid)
            {
                var createRestaurantCommand = Mapper.Map<CreateRestaurantCompanyCommand>(restaurantCompany);

                createRestaurantCommand.CreatedBy = Guid.Parse(GetAuthenticatedUserId());

                var response = await Mediator.Send<GenericResponse<RestaurantCompanyDto>>(createRestaurantCommand);

                return RedirectToActionPermanent("Index");
            }
            return View();
        }

        [HttpGet("Update/{RestaurantCompanyId}")]
        public async Task<IActionResult> Update([FromRoute] Guid RestaurantCompanyId)
        {
            var response = await Mediator.Send(new GetRestaurantCompanyQuery() { RestaurantCompanyId = RestaurantCompanyId });
            ViewData.Add("IsEditMode", true);

            return View("CreateOrUpdate", new RestaurantCompanyModel()
            {
                RestaurantCompanyName = response.Data.RestaurantCompanyName,
                Description = response.Data.Description,
                WebSite = response.Data.Website,
                Phone = response.Data.Phone
            });
        }

        [HttpPost("Update/{RestaurantCompanyId}")]
        public async Task<IActionResult> Update([FromRoute] Guid RestaurantCompanyId, [FromForm] RestaurantCompanyModel restaurantCompany)
        {
            var updateResturantCommand = Mapper.Map<UpdateRestaurantCompanyCommand>(restaurantCompany);
            updateResturantCommand.RestaurantCompanyId = RestaurantCompanyId;
            updateResturantCommand.UpdatedBy = Guid.Parse(GetAuthenticatedUserId());

            var response = await Mediator.Send<GenericResponse<RestaurantCompanyDto>>(updateResturantCommand);

            return RedirectToActionPermanent("Index");
        }

        [HttpGet("Delete/{RestaurantCompanyId}")]
        public async Task<IActionResult> Delete([FromRoute] Guid RestaurantCompanyId)
        {
            var deleteResturantCommand = new DeleteResturantCompanyCommand() { RestaurantCompanyId = RestaurantCompanyId };
            await Mediator.Send(deleteResturantCommand);
            return RedirectToActionPermanent("Index");
        }
    }
}

