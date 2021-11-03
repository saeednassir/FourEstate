using FourEstate.Core.Constants;
using FourEstate.Core.Dtos;
using FourEstate.Infrastructure.Services.Categories;
using FourEstate.Infrastructure.Services.Users;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FourEstate.API.Controllers
{
    public class CategoryController : BaseController
    {

        private readonly ICategoryService _categoryService;
        
        public CategoryController(ICategoryService categoryService, IUserService userService) : base(userService)
        {
            _categoryService = categoryService;
        }

        [HttpGet]
        public IActionResult GetAll(string searchKey)
        {
            var category=  _categoryService.GetAllAPI(searchKey);
            return Ok(GetRespons(category,Results.GetSuccessResult()));
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromForm]CreateCategoryDto dto)
        {
            if (ModelState.IsValid)
            {
                await _categoryService.Create(dto);
                return Ok(GetRespons(Results.AddSuccessResult()));
            }
            return Ok(dto);
        }


        [HttpPut]
        public async Task<IActionResult> Update([FromForm]UpdateCategoryDto dto)
        {
            if (ModelState.IsValid)
            {
                await _categoryService.Update(dto);
                return Ok(GetRespons(Results.EditSuccessResult()));
            }
            return Ok(dto);
        }

        [HttpDelete]
        public async Task<IActionResult> Delete(int id)
        {
            await _categoryService.Delete(id);
            return Ok(GetRespons(Results.DeleteSuccessResult()));
        }

        //[HttpGet]
        //public async Task<IActionResult> ExportToExcel()
        //{
        //    //var r = File(await _categoryService.ExportToExcel(), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "report_Category.xlsx");
        //    return Ok(GetRespons(""),r);
        //}
    }
}
