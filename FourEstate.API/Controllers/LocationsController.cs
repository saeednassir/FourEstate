using FourEstate.Core.Constants;
using FourEstate.Core.Dtos;
using FourEstate.Core.Enums;
using FourEstate.Infrastructure.Services.Categories;
using FourEstate.Infrastructure.Services.LocationsService;
using FourEstate.Infrastructure.Services.Users;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FourEstate.API.Controllers
{
    public class LocationsController :BaseController
    {

        private readonly ILocationService _locationService;

        public LocationsController(ILocationService locationService, IUserService userService) : base(userService)
        {
            _locationService = locationService;
        }

        [HttpGet]
        public IActionResult GetAll(string serachKey)
        {
            var location = _locationService.GetAllAPI(serachKey);
            return Ok(GetRespons(location, Results.GetSuccessResult()));
        }


        [HttpGet]
        public async Task<IActionResult> GetLog(int Id)
        {
            var logs = await _locationService.GetLog(Id);
            return Ok(GetRespons(logs, Results.GetSuccessResult()));
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromForm]CreateLocationDto dto)
        {
            await _locationService.Create(dto);
            return Ok(GetRespons(Results.AddSuccessResult()));
        }

        [HttpPut]
        public async Task<IActionResult> Update([FromForm] UpdateLocationDto dto)
        {
            await _locationService.Update(dto);
            return Ok(GetRespons(Results.EditSuccessResult()));
        }

        [HttpDelete]
        public async Task<IActionResult> Delete(int id)
        {
            await _locationService.Delete(id);
            return Ok(GetRespons(Results.DeleteSuccessResult()));
        }

        [HttpGet]
        public async Task<IActionResult> UpdateStatus(int id, ContentStatus status)
        {
            await _locationService.UpdateStatus(id, status);
            return Ok(GetRespons(Results.UpdateStatusResult()));
        }


        ////[HttpGet]
        ////public async Task<IActionResult> ExportToExcel()
        ////{
        ////    return File(await _locationService.ExportToExcel(), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "report_Location.xlsx");
        //}
    }
}
