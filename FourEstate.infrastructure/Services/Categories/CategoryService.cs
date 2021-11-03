using AutoMapper;
using FourEstate.Core.Dtos;
using FourEstate.Core.Exceptions;
using FourEstate.Core.ViewModel;
using FourEstate.Core.ViewModels;
using FourEstate.Data;
using FourEstate.Data.Models;
using FourEstate.Infrastructure.Helpers;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FourEstate.Infrastructure.Services.Categories
{
    public class CategoryService : ICategoryService
    {
        private readonly FourEstateDbContext _db;
        private readonly IMapper _mapper;

        public CategoryService(FourEstateDbContext db, IMapper mapper)
        {
            _db = db;
            _mapper = mapper;
        }


        public async Task<ResponseDto> GetAll(Pagination pagination, Query query)
        {
            var queryString = _db.Categories.Where(x => !x.IsDelete && (x.Name.Contains(query.GeneralSearch) || string.IsNullOrWhiteSpace(query.GeneralSearch))).AsQueryable();

            var dataCount = queryString.Count();
            var skipValue = pagination.GetSkipValue();
            var dataList = await queryString.Skip(skipValue).Take(pagination.PerPage).ToListAsync();
            var categories = _mapper.Map<List<CategoryViewModel>>(dataList);
            var pages = pagination.GetPages(dataCount);
            var result = new ResponseDto
            {
                data = categories,
                meta = new Meta
                {
                    page = pagination.Page,
                    perpage = pagination.PerPage,
                    pages = pages,
                    total = dataCount,
                }
            };
            return result;
        }




        public async Task<List<CategoryViewModel>> GetAllAPI(string serachKey)
        {
            var category = await _db.Categories.Where(x => x.Name.Contains(serachKey) || string.IsNullOrWhiteSpace(serachKey)).ToListAsync();
            return _mapper.Map<List<CategoryViewModel>>(category);


        }
        //public paginationViewModel GetAllAPI(int page)
        //{

        //    var pages = Math.Ceiling(_db.Categories.Count() / 10.0);


        //    if (page < 1 || page > pages)
        //    {
        //        page = 1;
        //    }

        //    var skip = (page - 1) * 10;

        //    var category = _db.Categories.Select(x => new CategoryViewModel()
        //    {
        //        Id = x.Id,
        //        Name = x.Name

        //    }).Skip(skip).Take(10).ToList();
        //    var pagingResult = new paginationViewModel();
        //    pagingResult.Data = category;
        //    pagingResult.NumberOfPages = (int)pages;
        //    pagingResult.currentPage = page;

        //    return pagingResult;
        //}



        public async Task<List<CategoryViewModel>> GetCategoryName()
        {
            var category = await _db.Categories.Where(x => !x.IsDelete).ToListAsync();
            return _mapper.Map<List<CategoryViewModel>>(category);
        }


        public async Task<int> Create(CreateCategoryDto dto)
        {
            var category = _mapper.Map<Category>(dto);
            await _db.Categories.AddAsync(category);
            await _db.SaveChangesAsync();
            return category.Id;
        }


        public async Task<int> Update(UpdateCategoryDto dto)
        {
            var category = await _db.Categories.SingleOrDefaultAsync(x => !x.IsDelete && x.Id == dto.Id);
            if(category == null)
            {
                throw new EntityNotFoundException();
            }
            var updatedCategory = _mapper.Map<UpdateCategoryDto, Category>(dto, category);
            _db.Categories.Update(updatedCategory);
            await _db.SaveChangesAsync();
            return updatedCategory.Id;
        }


        public async Task<UpdateCategoryDto> Get(int Id)
        {
            var category = await _db.Categories.SingleOrDefaultAsync(x => x.Id == Id && !x.IsDelete);
            if (category == null)
            {
                throw new EntityNotFoundException();
            }
            return _mapper.Map<UpdateCategoryDto>(category);
        }


        public async Task<int> Delete(int Id)
        {
            var category = await _db.Categories.SingleOrDefaultAsync(x => x.Id == Id && !x.IsDelete);
            if (category == null)
            {
                throw new EntityNotFoundException();
            }
            category.IsDelete = true;
            _db.Categories.Update(category);
            await _db.SaveChangesAsync();
            return category.Id;
        }

        public async Task<byte[]> ExportToExcel()
        {
            var users = await _db.Categories.Where(x => !x.IsDelete).ToListAsync();

            return ExcelHelpers.ToExcel(new Dictionary<string, ExcelColumn>
            {
                {"Name", new ExcelColumn("Name", 0)}
               
            }, new List<ExcelRow>(users.Select(e => new ExcelRow
            {
                Values = new Dictionary<string, string>
                {
                    {"Name", e.Name},
                   
                }
            })));

        }
    }
}
