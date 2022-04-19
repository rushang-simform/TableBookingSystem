using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace TableBookingSystem.Web.Controllers.Common
{
    [Route("[controller]")]
    public class BaseController : Controller
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
