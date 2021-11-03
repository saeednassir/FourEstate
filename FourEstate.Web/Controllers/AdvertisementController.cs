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

namespace FourEstate.Web.Controllers
{
    public class AdvertisementController : BaseController
    {

        private readonly IAdvertisementService _advertisementService;
        
        public AdvertisementController(IAdvertisementService advertisementService, IUserService userService) : base(userService)
        {
            _advertisementService = advertisementService;
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        public async Task<JsonResult> GetAdvertisementData(Pagination pagination,Query query)
        {
            var result = await _advertisementService.GetAll(pagination, query);
            return  Json(result);
        }



        public async Task<IActionResult> GetLog(int Id)
        {
            var logs = await _advertisementService.GetLog(Id);
            return View(logs);
        }





        [HttpGet]
        public async Task<IActionResult> Create()
        {
            ViewData["owners"] = new SelectList(await _advertisementService.GetAdvertisementOwners(),"Id","FullName");
            return View();
        }


        [HttpPost]
        public async Task<IActionResult> Create(CreateAdvertisementDto dto)
        {
            if (!string.IsNullOrWhiteSpace(dto.OwnerId))
            {
                ModelState.Remove("Owner.FullName");
                ModelState.Remove("Owner.Email");
                ModelState.Remove("Owner.PhoneNumber");
            }
         
            if (ModelState.IsValid)
            {
                await _advertisementService.Create(dto);
                return Ok(Results.AddSuccessResult());
            }
            return View(dto);
        }

        [HttpGet]
        public async Task<IActionResult> Update(int id)
        {
            var user = await _advertisementService.Get(id);
            return View(user);
        }

        [HttpPost]
        public async Task<IActionResult> Update(UpdateAdvertisementDto dto)
        {
            if (ModelState.IsValid)
            {
                await _advertisementService.Update(dto);
                return Ok(Results.EditSuccessResult());
            }
            return View(dto);
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            await _advertisementService.Delete(id);
            return Ok(Results.DeleteSuccessResult());
        }
        [HttpGet]
        public async Task<IActionResult> UpdateStatus(int id,ContentStatus status)
        {
            await _advertisementService.UpdateStatus(id, status);
            return Ok(Results.UpdateStatusResult());
        }

        [HttpGet]
        public async Task<IActionResult> ExportToExcel()
        {
            return File(await _advertisementService.ExportToExcel(), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "report_Advertisment.xlsx");
        }
    }
}
