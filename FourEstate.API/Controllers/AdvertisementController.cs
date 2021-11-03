using FourEstate.Core.Constants;
using FourEstate.Core.Dtos;
using FourEstate.Core.Enums;
using FourEstate.Infrastructure.Services.Advertisements;
using FourEstate.Infrastructure.Services.Categories;
using FourEstate.Infrastructure.Services.Users;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FourEstate.API.Controllers
{
    public class AdvertisementController :BaseController
    {

        private readonly IAdvertisementService _advertisementService;

        public AdvertisementController(IAdvertisementService advertisementService,IUserService userService) : base(userService)
        {
            _advertisementService = advertisementService;
        }

        [HttpGet]
        public IActionResult GetAll(string searchKey)
        {
            var category = _advertisementService.GetAllAPI(searchKey);
            return Ok(GetRespons(category, Results.GetSuccessResult()));
        }

        [HttpGet]
        public async Task<IActionResult> GetLog(int Id)
        {
            var logs = await _advertisementService.GetLog(Id);
            return Ok(GetRespons(logs,Results.GetSuccessResult()));
        }


        [HttpPost]
        public async Task<IActionResult> Create([FromForm]CreateAdvertisementDto dto)
        {
            if (!string.IsNullOrWhiteSpace(dto.OwnerId))
            {
                ModelState.Remove("Owner.FullName");
                ModelState.Remove("Owner.Email");
                ModelState.Remove("Owner.PhoneNumber");
            }
            await _advertisementService.Create(dto);
            return Ok(GetRespons(Results.AddSuccessResult()));
        }


        [HttpPut]
        public async Task<IActionResult> Update([FromForm] UpdateAdvertisementDto dto)
        {
            await _advertisementService.Update(dto);
            return Ok(GetRespons(Results.EditSuccessResult()));
        }

        [HttpDelete]
        public async Task<IActionResult> Delete(int id)
        {
            await _advertisementService.Delete(id);
            return Ok(GetRespons(Results.DeleteSuccessResult()));
        }

        [HttpGet]
        public async Task<IActionResult> UpdateStatus(int id, ContentStatus status)
        {
            await _advertisementService.UpdateStatus(id, status);
            return Ok(GetRespons(Results.UpdateStatusResult()));
        }


        //    //[HttpGet]
        //    //public async Task<IActionResult> ExportToExcel()
        //    //{
        //    //    return File(await _advertisementService.ExportToExcel(), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "report_Advertisment.xlsx");
        //    //}
        //}
    }
}