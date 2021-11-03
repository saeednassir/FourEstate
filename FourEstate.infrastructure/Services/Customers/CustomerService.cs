using AutoMapper;
using FourEstate.Core.Constants;
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

namespace FourEstate.Infrastructure.Services.Customers
{
    public class CustomerService : ICustomerService
    {
        private readonly FourEstateDbContext _db;
        private readonly IMapper _mapper;
        private readonly IFileService _fileService;

        public CustomerService(FourEstateDbContext db, IMapper mapper, IFileService fileService)
        {
            _db = db;
            _mapper = mapper;
            _fileService = fileService;
        }


        public async Task<ResponseDto> GetAll(Pagination pagination, Query query)
        {
            var queryString = _db.Customers.Include(x=>x.Location).Where(x => !x.IsDelete && (x.FirstName.Contains(query.GeneralSearch) || x.LastName.Contains(query.GeneralSearch) || x.FullName.Contains(query.GeneralSearch) || string.IsNullOrWhiteSpace(query.GeneralSearch))).AsQueryable();

            var dataCount = queryString.Count();
            var skipValue = pagination.GetSkipValue();
            var dataList = await queryString.Skip(skipValue).Take(pagination.PerPage).ToListAsync();
            var customer = _mapper.Map<List<CustomerViewModel>>(dataList);
            var pages = pagination.GetPages(dataCount);
            var result = new ResponseDto
            {
                data = customer,
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


        public async Task<List<CustomerViewModel>> GetAllAPI(string serachKey)
        {
            var customer = await _db.Customers.Where(x => x.FirstName.Contains(serachKey) || x.FullName.Contains(serachKey) || x.LastName.Contains(serachKey) || string.IsNullOrWhiteSpace(serachKey)).ToListAsync();
            return _mapper.Map<List<CustomerViewModel>>(customer);
        }
        //public paginationViewModel GetAllAPI(int page)
        //{

        //    var pages = Math.Ceiling(_db.Customers.Count() / 10.0);


        //    if (page < 1 || page > pages)
        //    {
        //        page = 1;
        //    }

        //    var skip = (page - 1) * 10;

        //    var customer = _db.Customers.Select(x => new CustomerViewModel()
        //    {
        //        Id = x.Id,
        //        FirstName = x.FirstName,
        //        LastName = x.LastName,
        //        FullName = x.FullName,
        //        Phone = x.Phone
        //    }).Skip(skip).Take(10).ToList();
        //    var pagingResult = new paginationViewModel();
        //    pagingResult.Data = customer;
        //    pagingResult.NumberOfPages = (int)pages;
        //    pagingResult.currentPage = page;

        //    return pagingResult;
        //}

        public async Task<List<CustomerViewModel>> GetCustomerName()
        {
            var customer = await _db.Customers.Where(x => !x.IsDelete).ToListAsync();
            return _mapper.Map<List<CustomerViewModel>>(customer);
        }





        public async Task<int> Create(CreateCustomerDto dto)
        {
            var customer = _mapper.Map<Customer>(dto);
            if (dto.ImageUrl != null)
            {
                customer.ImageUrl = await _fileService.SaveFile(dto.ImageUrl, FolderNames.ImagesFolder);
            }
            await _db.Customers.AddAsync(customer);
            await _db.SaveChangesAsync();
            return customer.Id;
        }


        public async Task<int> Update(UpdateCustomerDto dto)
        {
            var customer = await _db.Customers.SingleOrDefaultAsync(x => !x.IsDelete && x.Id == dto.Id);
            if(customer == null)
            {
                throw new EntityNotFoundException();
            }
            var updatedCustomer = _mapper.Map<UpdateCustomerDto, Customer>(dto, customer);
            if (dto.ImageUrl != null)
            {
                customer.ImageUrl = await _fileService.SaveFile(dto.ImageUrl, FolderNames.ImagesFolder);
            }
            _db.Customers.Update(updatedCustomer);
            await _db.SaveChangesAsync();
            return updatedCustomer.Id;
        }


        public async Task<UpdateCustomerDto> Get(int Id)
        {
            var customer = await _db.Customers.SingleOrDefaultAsync(x => x.Id == Id && !x.IsDelete);
            if (customer == null)
            {
                throw new EntityNotFoundException();
            }
            return _mapper.Map<UpdateCustomerDto>(customer);
        }


        public async Task<int> Delete(int Id)
        {
            var customer = await _db.Customers.SingleOrDefaultAsync(x => x.Id == Id && !x.IsDelete);
            if (customer == null)
            {
                throw new EntityNotFoundException();
            }
            customer.IsDelete = true;
            _db.Customers.Update(customer);
            await _db.SaveChangesAsync();
            return customer.Id;
        }



        public async Task<byte[]> ExportToExcel()
        {
            var customers = await _db.Customers.Include(x=>x.Location).Where(x => !x.IsDelete).ToListAsync();

            return ExcelHelpers.ToExcel(new Dictionary<string, ExcelColumn>
            {
                {"FullName", new ExcelColumn("FullName", 0)},
                {"DOB", new ExcelColumn("DOB", 1)},
                {"Location", new ExcelColumn("Location", 2)}
            }, new List<ExcelRow>(customers.Select(e => new ExcelRow
            {
                Values = new Dictionary<string, string>
                {
                    {"FullName", e.FullName},
                    {"DOB", e.DOB.ToString()},
                     {"Location", e.Location.Country}
                }
            })));
        }

    }
}
