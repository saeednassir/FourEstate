using FourEstate.Core.Constants;
using FourEstate.Core.Dtos;
using FourEstate.Infrastructure.Services.Customers;
using FourEstate.Infrastructure.Services.LocationsService;
using FourEstate.Infrastructure.Services.Users;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FourEstate.Web.Controllers
{
    public class CustomerController : BaseController
    {

        private readonly ICustomerService _customerService;
        private readonly ILocationService _locationService;

        public CustomerController(ICustomerService customerService, ILocationService locationService, IUserService userService) : base(userService)
        {
            _customerService = customerService;
            _locationService = locationService;
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        public async Task<JsonResult> GetCustomerData(Pagination pagination,Query query)
        {
            var result = await _customerService.GetAll(pagination, query);
            return  Json(result);
        }

        [HttpGet]
        public async Task<IActionResult> CreateAsync()
        {
            ViewData["location"] = new SelectList(await _locationService.GetLocationCountry(), "Id", "Country");
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateCustomerDto dto)
        {
            if (ModelState.IsValid)
            {
                await _customerService.Create(dto);
                return Ok(Results.AddSuccessResult());
            }
            ViewData["location"] = new SelectList(await _locationService.GetLocationCountry(), "Id", "Country");

            return View(dto);
        }

        [HttpGet]
        public async Task<IActionResult> Update(int id)
        {
            ViewData["location"] = new SelectList(await _locationService.GetLocationCountry(), "Id", "Country");

            var user = await _customerService.Get(id);
            return View(user);
        }

        [HttpPost]
        public async Task<IActionResult> Update(UpdateCustomerDto dto)
        {
            if (ModelState.IsValid)
            {
                await _customerService.Update(dto);
                return Ok(Results.EditSuccessResult());
            }
            ViewData["location"] = new SelectList(await _locationService.GetLocationCountry(), "Id", "Country");

            return View(dto);
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            await _customerService.Delete(id);
            return Ok(Results.DeleteSuccessResult());
        }

        [HttpGet]
        public async Task<IActionResult> ExportToExcel()
        {
            return File(await _customerService.ExportToExcel(), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "report_Customer.xlsx");
        }
    }
}
