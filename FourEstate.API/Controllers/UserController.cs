using FourEstate.Core.Constants;
using FourEstate.Core.Dtos;
using FourEstate.Infrastructure.Services.Users;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FourEstate.API.Controllers
{
    public class UserController :BaseController
    {

        private readonly IUserService _userService;

        public UserController(IUserService userService) : base(userService)
        {
            _userService = userService;
        }

       [HttpGet]
       public IActionResult GetAll(string serachKey)

       {
           var user = _userService.GetAllAPI(serachKey);
           return Ok(GetRespons(user, Results.GetSuccessResult()));
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromForm] CreateUserDto dto)
        {
            await _userService.Create(dto);
            return Ok(GetRespons(Results.AddSuccessResult()));
        }

        [HttpPut]
        public async Task<IActionResult> Update([FromForm] UpdateUserDto dto)
        {
            await _userService.Update(dto);
            return Ok(GetRespons(Results.EditSuccessResult()));

        }

        [HttpDelete]
        public async Task<IActionResult> Delete(string id)
        {
            await _userService.Delete(id);
            return Ok(GetRespons(Results.DeleteSuccessResult()));
        }



        //[HttpGet]
        //public async Task<IActionResult> ExportToExcel()
        //{
        //    return File(await _userService.ExportToExcel(), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "report_User.xlsx");
        //}

    }
}
