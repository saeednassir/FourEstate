using FourEstate.Core.Constants;
using FourEstate.Core.Dtos;
using FourEstate.Infrastructure.Services.Categories;
using FourEstate.Infrastructure.Services.Users;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FourEstate.Web.Controllers
{
    public class CategoryController : BaseController
    {

        private readonly ICategoryService _categoryService;
        
        public CategoryController(ICategoryService categoryService, IUserService userService) : base(userService)
        {
            _categoryService = categoryService;
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        public async Task<JsonResult> GetCategoryData(Pagination pagination,Query query)
        {
            var result = await _categoryService.GetAll(pagination, query);
            return  Json(result);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateCategoryDto dto)
        {
            if (ModelState.IsValid)
            {
                await _categoryService.Create(dto);
                return Ok(Results.AddSuccessResult());
            }
            return View(dto);
        }

        [HttpGet]
        public async Task<IActionResult> Update(int id)
        {
            var user = await _categoryService.Get(id);
            return View(user);
        }

        [HttpPost]
        public async Task<IActionResult> Update(UpdateCategoryDto dto)
        {
            if (ModelState.IsValid)
            {
                await _categoryService.Update(dto);
                return Ok(Results.EditSuccessResult());
            }
            return View(dto);
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            await _categoryService.Delete(id);
            return Ok(Results.DeleteSuccessResult());
        }

        [HttpGet]
        public async Task<IActionResult> ExportToExcel()
        {
            return File(await _categoryService.ExportToExcel(), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "report_Category.xlsx");
        }
    }
}
