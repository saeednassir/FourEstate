using FourEstate.Core.Constants;
using FourEstate.Core.Dtos;
using FourEstate.Core.Enums;
using FourEstate.infrastructure.Services.ContractSS;
using FourEstate.Infrastructure.Services.Customers;
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
    public class ContractController :BaseController
    {
        private readonly IContractService _contractService;
        //private readonly ICustomerService _customerService;
        //private readonly IRealEstateService _realEstateService;

        public ContractController(IContractService contractService, IUserService userService) : base(userService)/*, ICustomerService customerService, IRealEstateService realEstateService)/*, IUserSer*//*vice userService) : base(userService)*/
        {
            _contractService = contractService;
            //_customerService = customerService;
            //_realEstateService = realEstateService;
        }


        [HttpGet]
        public async Task<IActionResult> GetLog(int Id)
        {
            var logs = await _contractService.GetLog(Id);
            return Ok(GetRespons(logs,Results.GetSuccessResult()));
        }

        [HttpGet]
        public IActionResult GetAll(string searchKey)
        {
            var contract = _contractService.GetAllAPI(searchKey);
            return Ok(GetRespons(contract, Results.GetSuccessResult()));
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromForm] CreateContractDto dto)
        {
            await _contractService.Create(dto);
            return Ok(GetRespons(Results.AddSuccessResult()));
        }

        [HttpPut]
        public async Task<IActionResult> Update([FromForm] UpdateContractDto dto)
        {
            await _contractService.Update(dto);
            return Ok(GetRespons(Results.EditSuccessResult()));
        }

        [HttpDelete]
        public async Task<IActionResult> Delete(int id)
        {
            await _contractService.Delete(id);
            return Ok(GetRespons(Results.DeleteSuccessResult()));
        }


        [HttpGet]
        public async Task<IActionResult> UpdateStatus(int id, ContentStatus status)
        {
            await _contractService.UpdateStatus(id, status);
            return Ok(GetRespons(Results.UpdateStatusResult()));
        }

        //[HttpGet]
        //public async Task<IActionResult> ExportToExcel()
        //    {
        //        return File(await _contractService.ExportToExcel(), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "report_Contract.xlsx");
        //}
    }
}
