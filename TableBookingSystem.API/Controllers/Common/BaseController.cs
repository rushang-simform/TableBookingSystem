using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace TableBookingSystem.API.Controllers.Common
{
    [Route("api/[controller]")]
    [ApiController]
    public class BaseController : ControllerBase
    {
        protected IMapper Mapper => HttpContext.RequestServices.GetService<IMapper>();
        protected IMediator Mediator => HttpContext.RequestServices.GetService<IMediator>();

        protected string GetAuthenticatedUserId()
        {
            string userId = string.Empty;
            userId = User.FindFirst("UserId").Value.ToString();
            return userId ?? string.Empty;
        }
        protected Guid GetAuthenticatedUserIdGuid()
        {
            string userId = string.Empty;
            userId = User.FindFirst("UserId").Value.ToString();
            return Guid.Parse(userId);
        }
    }
}
