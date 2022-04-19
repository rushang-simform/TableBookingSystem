using System;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TableBookingSystem.Application.DTOs;
using MediatR;
using TableBookingSystem.Application.Features.User.Commands;
using System.Threading.Tasks;
using AutoMapper;
using TableBookingSystem.Application.Features.User.Queries;
using TableBookingSystem.Application.DTOs.Response;
using TableBookingSystem.Application.DTOs.User;
using TableBookingSystem.Web.Models;
using TableBookingSystem.Application.Intefaces.Auth;
using TableBookingSystem.Web.Controllers.Common;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Authorization;

namespace TableBookingSystem.Web.Controllers.User
{
    public class UsersController : BaseController
    {
        private readonly ILogger<UsersController> _logger;
        private readonly IAccountService _accountService;

        public UsersController(ILogger<UsersController> logger, IAccountService accountService)
        {
            _logger = logger;
            _accountService = accountService;
        }

        [HttpGet("{UserId}")]
        [Authorize("SuperAdmin")]
        public async Task<IActionResult> Get([FromRoute] string UserId)
        {
            try
            {
                var response = await Mediator.Send<GenericResponse<BasicUserInfoDto>>(new GetUserInfoQuery() { UserId = UserId });
                if (response.IsSuccess)
                {
                    return Ok(response.Data);
                }
                else
                {
                    return BadRequest(response.Message);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed to get user data");
                throw;
            }

        }


        /// <summary>
        /// Used for super admin to create system users
        /// </summary>
        /// <param name="userInfo"></param>
        /// <returns></returns>
        //[Authorize("SuperAdmin")]
        [HttpPost("/CreateUser")]
        public async Task<IActionResult> CreateUser([FromBody] UserInfoModel userInfo)
        {
            var userCommand = Mapper.Map<CreateUserCommand>(userInfo);

            userCommand.CreatedOrModifiedBy = "0EB91BFB-E130-4479-94FB-2964D0DF0CCB"; // GetAuthenticatedUserId();

            var result = await Mediator.Send<GenericResponse<BasicUserInfoDto>>(userCommand);

            if (result.IsSuccess)
            {
                return Ok(result.Data);
            }
            else
            {
                return BadRequest(result.Message);
            }
        }

        [HttpPost("/SignUp")]
        [AllowAnonymous]
        public async Task<IActionResult> Post([FromBody] SignUpModel signupUser)
        {

            var userCommand = Mapper.Map<CreateUserCommand>(signupUser);

            var result = await Mediator.Send<GenericResponse<BasicUserInfoDto>>(userCommand);

            if (result.IsSuccess)
            {
                return Ok(result.Data);
            }
            else
            {
                return BadRequest(result.Message);
            }

        }

        [HttpGet("/Login")]
        [AllowAnonymous]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost("/Login")]
        [AllowAnonymous]
        public async Task<IActionResult> Login([FromForm] LoginModel loginModel)
        {
            if (ModelState.IsValid)
            {
                var response = await _accountService.Authenticate(loginModel.EmailId, loginModel.Password);
                if (response.IsSuccess)
                {
                    HttpContext.Response.Cookies.Append("AuthToken", response.AuthToken, new CookieOptions() { HttpOnly = true });
                    return Redirect("/Welcome");
                }
                else
                {
                    ModelState.AddModelError("EmailId", response.Message);
                    return View();
                }
            }
            else
            {
                return View();
            }
        }
    }
}
