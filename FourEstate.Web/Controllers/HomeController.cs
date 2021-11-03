using FourEstate.infrastructure.Services.Dashbords;
using FourEstate.Infrastructure.Services.Users;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace FourEstate.Web.Controllers
{
    public class HomeController : BaseController
    {
        private readonly IDashboardService _dashboardService;

        public HomeController(IDashboardService dashboardService, IUserService userService) : base(userService)
        {
            _dashboardService = dashboardService;
        }

        public async Task<IActionResult> Index()
        {
            var data =await  _dashboardService.GetData();
            return View(data);
        }

        public async Task<IActionResult> GetUserChartData()
        {
            var data = await _dashboardService.GetUsersDataChart();
            return Ok(data);
        }

        public async Task<IActionResult> GetContractCharData()
        {
            var data = await _dashboardService.GetContractDataChart();
            return Ok(data);
        }


        public async Task<IActionResult> GetUserRealEstateData()
        {
            var data = await _dashboardService.GetRealEstateDataChart();
            return Ok(data);
        }

    }
}
