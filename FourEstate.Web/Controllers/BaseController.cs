using FourEstate.Infrastructure.Services.Users;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FourEstate.Web.Controllers
{

    [Authorize]
    public class BaseController : Controller
    {
        protected readonly IUserService _userService;

        public BaseController(IUserService userService)
        {
            _userService = userService;
        }

        public override void OnActionExecuting(ActionExecutingContext context)
        {
            base.OnActionExecuting(context);

            if (User.Identity.IsAuthenticated) {

                var userName = User.Identity.Name;
                var user = _userService.GetUserByName(userName);
                ViewBag.FullName = user.FullName;
                ViewBag.UserType = user.UserType;
                ViewBag.UserImg = user.ImageUrl;

            }

        }
    }
}

