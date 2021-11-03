using FourEstate.Core.Constants;
using FourEstate.Core.Dtos;
using FourEstate.Core.Enums;
using FourEstate.Infrastructure.Services.Categories;
using FourEstate.Infrastructure.Services.LocationsService;
using FourEstate.Infrastructure.Services.REAlEstate;
using FourEstate.Infrastructure.Services.Users;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FourEstate.API.Controllers
{
    public class RealEstateController  :BaseController
    {

        private readonly IRealEstateService _realEstateService;

        public RealEstateController(IRealEstateService realEstateService, IUserService userService) : base(userService)
        {
            _realEstateService = realEstateService;

        }

        [HttpGet]
        public async Task<IActionResult> GetLog(int Id)
        {
            var logs = await _realEstateService.GetLog(Id);
            return Ok(GetRespons(logs, Results.GetSuccessResult()));
        }

        [HttpGet]
        public IActionResult GetAll(string serachKey)
        {
            var category = _realEstateService.GetAllAPI(serachKey);
            return Ok(GetRespons(category, Results.GetSuccessResult()));
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromForm] CreateRealEstateDto dto)
        {
            await _realEstateService.Create(dto);
            return Ok(GetRespons(Results.AddSuccessResult()));
        }


        [HttpPut]
        public async Task<IActionResult> Update([FromForm] UpdateRealEstateDto dto)
        {
            await _realEstateService.Update(dto);
            return Ok(GetRespons(Results.EditSuccessResult()));
        }

        [HttpDelete]
        public async Task<IActionResult> Delete(int id)
        {
            await _realEstateService.Delete(id);
            return Ok(GetRespons(Results.DeleteSuccessResult()));
        }


        [HttpGet]
        public async Task<IActionResult> UpdateStatus(int id, ContentStatus status)
        {
            await _realEstateService.UpdateStatus(id, status);
            return Ok(GetRespons(Results.UpdateStatusResult()));
        }


        //[HttpGet]
        //public async Task<IActionResult> ExportToExcel()
        //{
        //    return File(await _realEstateService.ExportToExcel(), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "report_RealEstate.xlsx");
        //}




        [HttpGet]
        public async Task<IActionResult> RemoveAttachment(int id)
        {
            await _realEstateService.RemoveAttachment(id);
            return Ok(GetRespons(Results.DeleteSuccessResult()));
        }

    }
}
