using AutoMapper;
using FourEstate.Core.Dtos;
using FourEstate.Core.Enums;
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

namespace FourEstate.infrastructure.Services.ContractSS
{
    public class ContractService : IContractService
    {

        private readonly FourEstateDbContext _db;
        private readonly IMapper _mapper;

        public ContractService(FourEstateDbContext db, IMapper mapper)
        {
            _db = db;
            _mapper = mapper;
        }


        public async Task<ResponseDto> GetAll(Pagination pagination, Query query)
        {
            var queryString = _db.Contracts.Include(x => x.RealEstate).Include(x => x.Customer)
                .Where(x => !x.IsDelete && (x.Customer.FullName.Contains(query.GeneralSearch) || string.IsNullOrWhiteSpace(query.GeneralSearch)))
                .AsQueryable();


            var dataCount = queryString.Count();
            var skipValue = pagination.GetSkipValue();
            var dataList = await queryString.Skip(skipValue).Take(pagination.PerPage).ToListAsync();
            var contracts = _mapper.Map<List<ContractViewModel>>(dataList);

            var pages = pagination.GetPages(dataCount);
            var result = new ResponseDto
            {
                data = contracts,
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

        public async Task<List<ContractViewModel>> GetAllAPI(string serachKey)
        {
            var contract = await _db.Contracts
                .Include(x => x.Customer)
                .Include(x=>  x.RealEstate).ThenInclude(x=>x.Location)
                .Include(x => x.RealEstate).ThenInclude(x => x.Category)
                .Where(x =>   x.Stauts.ToString().Contains(serachKey) || string.IsNullOrWhiteSpace(serachKey))
                .ToListAsync();
            return _mapper.Map<List<ContractViewModel>>(contract);
        }

        //public paginationViewModel GetAllAPI(int page)
        //{

        //    var pages = Math.Ceiling(_db.Locations.Count() / 10.0);


        //    if (page < 1 || page > pages)
        //    {
        //        page = 1;
        //    }

        //    var skip = (page - 1) * 10;

        //    var contract = _db.Contracts.Include(x=>x.RealEstate).Include(x=>x.Customer).Select(x => new ContractViewModel()
        //    {
        //        Id = x.Id,
        //        ContractType = x.ContractType.ToString(),
        //        Price = x.Price,
        //        Customer = new CustomerViewModel() {Id =x.Customer.Id,FullName =x.Customer.FullName},
        //        RealEstate = new RealEstateViewModel() { Id = x.RealEstate.Id,Name = x.RealEstate.Name},
        //        Status = x.Stauts.ToString()

        //    }).Skip(skip).Take(10).ToList();
        //    var pagingResult = new paginationViewModel();
        //    pagingResult.Data = contract;
        //    pagingResult.NumberOfPages = (int)pages;
        //    pagingResult.currentPage = page;

        //    return pagingResult;
    //}

        public async Task<List<ContentChangeLogViewModel>> GetLog(int id)
        {
            var changes = await _db.ContentChangeLogs.Where(x => x.ContentId == id && x.Type == ContentType.Contract).ToListAsync();
            return _mapper.Map<List<ContentChangeLogViewModel>>(changes);
        }



        public async Task<List<ContractViewModel>> GetContractName()
        {
            var contract = await _db.Contracts.Where(x => !x.IsDelete).ToListAsync();
            return _mapper.Map<List<ContractViewModel>>(contract);
        }



        //public async Task<int> Create(CreateContractDto dto)
        //{
        //    var contract = _mapper.Map<CreateContractDto, Contract>(dto);
        //    await _db.Contracts.AddAsync(contract);
        //    await _db.SaveChangesAsync();
        //    return contract.Id;
        //}
        public async Task<int> Create(CreateContractDto dto)
        {
            var contract = new Contract();
            contract.ContractType = dto.ContractType;
            contract.CustomerId = dto.CustomerId;
            contract.RealEstateId = dto.RealEstatedId;
            contract.Price = dto.Price;
            contract.CreatedAt = DateTime.Now;
            await _db.Contracts.AddAsync(contract);
            await _db.SaveChangesAsync();
            return contract.Id;
        }

        public async Task<int> Update(UpdateContractDto dto)
        {
            var contract = await _db.Contracts.SingleOrDefaultAsync(x => !x.IsDelete && x.Id == dto.Id);
            if (contract == null)
            {
                throw new EntityNotFoundException();
            }
            var updatedcontract = _mapper.Map<UpdateContractDto, Contract>(dto, contract);
            _db.Contracts.Update(updatedcontract);
            await _db.SaveChangesAsync();
            return updatedcontract.Id;
        }


        public async Task<UpdateContractDto> Get(int Id)
        {
            var contract = await _db.Contracts.SingleOrDefaultAsync(x => x.Id == Id && !x.IsDelete);
            if (contract == null)
            {
                throw new EntityNotFoundException();
            }
            return _mapper.Map<UpdateContractDto>(contract);
        }


        public async Task<int> Delete(int Id)
        {
            var contract = await _db.Contracts.SingleOrDefaultAsync(x => x.Id == Id && !x.IsDelete);
            if (contract == null)
            {
                throw new EntityNotFoundException();
            }
            contract.IsDelete = true;
            _db.Contracts.Update(contract);
            await _db.SaveChangesAsync();
            return contract.Id;
        }




        public async Task<int> UpdateStatus(int id, ContentStatus status)
        {
            var contract = await _db.Contracts.SingleOrDefaultAsync(x => x.Id == id && !x.IsDelete);
            if (contract == null)
            {
                throw new EntityNotFoundException();
            }

            var changeLog = new ContentChangeLog();
            changeLog.ContentId = contract.Id;
            changeLog.Type = ContentType.Contract;
            changeLog.Old = contract.Stauts;
            changeLog.New = status;
            changeLog.ChangeAt = DateTime.Now;

            await _db.ContentChangeLogs.AddAsync(changeLog);
            await _db.SaveChangesAsync();


            contract.Stauts = status;
            _db.Contracts.Update(contract);
            await _db.SaveChangesAsync();

            //await _emailService.Send(post.Author.Email, "UPDATE POST STATUS !", $"YOUR POST NOW IS {status.ToString()}");

            return contract.Id;
        }

        public async Task<byte[]> ExportToExcel()
        {
            var users = await _db.Contracts.Include(x=>x.Customer).Include(x=>x.RealEstate).Where(x => !x.IsDelete).ToListAsync();

            return ExcelHelpers.ToExcel(new Dictionary<string, ExcelColumn>
            {
                {"Price", new ExcelColumn("Price", 0)},
                {"ContractType", new ExcelColumn("ContractType", 1)},
                {"RealEstate", new ExcelColumn("RealEstate", 2)},
                {"Customer", new ExcelColumn("Customer", 3)}
            }, new List<ExcelRow>(users.Select(e => new ExcelRow
            {
                Values = new Dictionary<string, string>
                {
                    {"Price", e.Price.ToString()},
                    {"ContractType", e.ContractType.ToString()},
                    {"RealEstate", e.RealEstate.Name},
                    {"Customer", e.Customer.FullName}
                }
            })));
        }

    }
}
