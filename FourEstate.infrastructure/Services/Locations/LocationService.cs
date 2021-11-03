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

namespace FourEstate.Infrastructure.Services.LocationsService
{
    public class LocationService : ILocationService
    {
        private readonly FourEstateDbContext _db;
        private readonly IMapper _mapper;

        public LocationService(FourEstateDbContext db, IMapper mapper)
        {
            _db = db;
            _mapper = mapper;
        }


        public async Task<ResponseDto> GetAll(Pagination pagination, Query query)
        {
            var queryString = _db.Locations.Where(x => !x.IsDelete && (x.Country.Contains(query.GeneralSearch) || string.IsNullOrWhiteSpace(query.GeneralSearch))).AsQueryable();

            var dataCount = queryString.Count();
            var skipValue = pagination.GetSkipValue();
            var dataList = await queryString.Skip(skipValue).Take(pagination.PerPage).ToListAsync();
            var location = _mapper.Map<List<LocationViewModel>>(dataList);
            var pages = pagination.GetPages(dataCount);
            var result = new ResponseDto
            {
                data = location,
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

        public async Task<List<LocationViewModel>> GetAllAPI(string serachKey)
        {
            var location = await _db.Locations.Where(x => x.Country.Contains(serachKey) || x.City.Contains(serachKey) || x.Street.Contains(serachKey) || string.IsNullOrWhiteSpace(serachKey)).ToListAsync();
            return _mapper.Map<List<LocationViewModel>>(location);
        }
        //public paginationViewModel GetAllAPI(int page)
        //{

        //    var pages = Math.Ceiling(_db.Locations.Count() / 10.0);


        //    if (page < 1 || page > pages)
        //    {
        //        page = 1;
        //    }

        //    var skip = (page - 1) * 10;

        //    var location = _db.Locations.Select(x => new LocationViewModel()
        //    {
        //        Id = x.Id,
        //        Country =x.Country,
        //        City= x.City,
        //        Street =x.Street,
        //        StreetNumber =x.StreetNumber,
        //        Status =x.Stauts.ToString()

        //    }).Skip(skip).Take(10).ToList();
        //    var pagingResult = new paginationViewModel();
        //    pagingResult.Data = location;
        //    pagingResult.NumberOfPages = (int)pages;
        //    pagingResult.currentPage = page;

        //    return pagingResult;
        //}




        public async Task<List<ContentChangeLogViewModel>> GetLog(int id)
        {
            var changes = await _db.ContentChangeLogs.Where(x => x.ContentId == id && x.Type == ContentType.Location).ToListAsync();
            return _mapper.Map<List<ContentChangeLogViewModel>>(changes);
        }



        public async Task<List<LocationViewModel>> GetLocationCountry()
        {
            var location = await _db.Locations.Where(x => !x.IsDelete).ToListAsync();
            return _mapper.Map<List<LocationViewModel>>(location);
        }

        public async Task<int> Create(CreateLocationDto dto)
        {
            var location = _mapper.Map<Location>(dto);
            await _db.Locations.AddAsync(location);
            await _db.SaveChangesAsync();
            return location.Id;
        }

        public async Task<int> Update(UpdateLocationDto dto)
        {
            var location= await _db.Locations.SingleOrDefaultAsync(x => !x.IsDelete && x.Id == dto.Id);
            if (location == null)
            {
                throw new EntityNotFoundException();
            }
            var updatedLocation = _mapper.Map<UpdateLocationDto, Location>(dto, location);


            _db.Locations.Update(updatedLocation);
            await _db.SaveChangesAsync();
            return updatedLocation.Id;
        }


        public async Task<UpdateLocationDto> Get(int Id)
        {
            var location = await _db.Locations.SingleOrDefaultAsync(x => x.Id == Id && !x.IsDelete);
            if (location == null)
            {
                throw new EntityNotFoundException();
            }
            return _mapper.Map<UpdateLocationDto>(location);
        }

              public async Task<int>Delete(int Id) {
            var location = await _db.Locations.SingleOrDefaultAsync(x => x.Id == Id && !x.IsDelete);
            if (location == null)
            {
                throw new EntityNotFoundException();
            }
            location.IsDelete = true;
            _db.Locations.Update(location);
            await _db.SaveChangesAsync();
            return location.Id;
        }

        public async Task<int> UpdateStatus(int id, ContentStatus status)
        {
            var location = await _db.Locations.SingleOrDefaultAsync(x => x.Id == id && !x.IsDelete);
            if (location == null)
            {
                throw new EntityNotFoundException();
            }

            var changeLog = new ContentChangeLog();
            changeLog.ContentId = location.Id;
            changeLog.Type = ContentType.Location;
            changeLog.Old = location.Stauts;
            changeLog.New = status;
            changeLog.ChangeAt = DateTime.Now;

            await _db.ContentChangeLogs.AddAsync(changeLog);
            await _db.SaveChangesAsync();

            location.Stauts = status;
            _db.Locations.Update(location);
            await _db.SaveChangesAsync();

            //await _emailService.Send(post.Author.Email, "UPDATE POST STATUS !", $"YOUR POST NOW IS {status.ToString()}");

            return location.Id;
        }



        public async Task<byte[]> ExportToExcel()
        {
            var users = await _db.Locations.Where(x => !x.IsDelete).ToListAsync();

            return ExcelHelpers.ToExcel(new Dictionary<string, ExcelColumn>
            {
                {"Country", new ExcelColumn("Country", 0)},
                {"City", new ExcelColumn("City", 1)},
                {"Street", new ExcelColumn("Street", 2)}
            }, new List<ExcelRow>(users.Select(e => new ExcelRow
            {
                Values = new Dictionary<string, string>
                {
                    {"Country", e.Country},
                    {"City", e.City},
                     {"Street", e.Street}
                }
            })));
        }


    }
}
