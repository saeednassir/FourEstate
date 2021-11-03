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

namespace FourEstate.API.Controllers
{
    public class CustomerController : BaseController
    {

        private readonly ICustomerService _customerService;
        ////private readonly ILocationService _locationService;

        public CustomerController(ICustomerService customerService, IUserService userService) : base(userService)/*, ILocationService locationService), IUserService userService) : base(userService)*/
        {
            _customerService = customerService;
            //_locationService = locationService;
        }

        [HttpGet]
        public IActionResult GetAll(string serachKey)
        {
            var cusotmer = _customerService.GetAllAPI(serachKey);
            return Ok(GetRespons(cusotmer, Results.GetSuccessResult()));
        }


        [HttpPost]
        public async Task<IActionResult> Create([FromForm] CreateCustomerDto dto)
        {
        await _customerService.Create(dto);
        return Ok(GetRespons(Results.AddSuccessResult()));
        }


    [HttpPut]
    public async Task<IActionResult> Update([FromForm] UpdateCustomerDto dto)
    {
        await _customerService.Update(dto);
        return Ok(GetRespons(Results.EditSuccessResult()));
    }


    [HttpDelete]
    public async Task<IActionResult> Delete(int id)
    {
        await _customerService.Delete(id);
        return Ok(GetRespons(Results.DeleteSuccessResult()));
    }

    //    [HttpGet]
    //    public async Task<IActionResult> ExportToExcel()
    //    {
    //        return File(await _customerService.ExportToExcel(), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "report_Customer.xlsx");
    //    }
 //}
}
}