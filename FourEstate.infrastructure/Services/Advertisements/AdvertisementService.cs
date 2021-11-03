using AutoMapper;
using FourEstate.Core.Dtos;
using FourEstate.Core.Enums;
using FourEstate.Core.Exceptions;
using FourEstate.Core.ViewModel;
using FourEstate.Core.ViewModels;
using FourEstate.Data;
using FourEstate.Data.Models;
using FourEstate.Infrastructure.Helpers;
using FourEstate.Infrastructure.Services.Users;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FourEstate.Infrastructure.Services.Advertisements
{
    public class AdvertisementService : IAdvertisementService
    {

        private readonly FourEstateDbContext _db;
        private readonly IMapper _mapper;
        private readonly IUserService  _userService;
        private readonly IFileService _fileService;

        public AdvertisementService(IFileService fileService, FourEstateDbContext db, IMapper mapper, IUserService userService)
        {
            _db = db;
            _mapper = mapper;
            _fileService = fileService;
            _userService = userService;
        }

        
        public async Task<List<UserViewModel>> GetAdvertisementOwners()
        {
            var users = await _db.Users.Where(x => !x.IsDelete && x.UserType == UserType.AdvertisementOwner).ToListAsync();
            return _mapper.Map<List<UserViewModel>>(users);
        }

        public async Task<ResponseDto> GetAll(Pagination pagination, Query query)
        {
            var queryString = _db.Advertisements.Include(x => x.Owner).Where(x => !x.IsDelete).AsQueryable();

            var dataCount = queryString.Count();
            var skipValue = pagination.GetSkipValue();
            var dataList = await queryString.Skip(skipValue).Take(pagination.PerPage).ToListAsync();
            var advertisements = _mapper.Map<List<AdvertisementViewModel>>(dataList);
            var pages = pagination.GetPages(dataCount);
            var result = new ResponseDto
            {
                data = advertisements,
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

        public async Task<List<AdvertisementViewModel>> GetAllAPI(string serachKey)
        {
            var advertisment = await _db.Advertisements.Include(x => x.Owner).Where(x => x.Title.Contains(serachKey) || string.IsNullOrWhiteSpace(serachKey)).ToListAsync();
            return _mapper.Map<List<AdvertisementViewModel>>(advertisment);

        }
            //public paginationViewModel GetAllAPI(int page)
            //{

            //    var pages = Math.Ceiling(_db.Advertisements.Count() / 10.0);


            //    if (page < 1 || page > pages)
            //    {
            //        page = 1;
            //    }

            //    var skip = (page - 1) * 10;

            //    var Advertisment = _db.Advertisements.Include(x => x.Owner).Where(x => !x.IsDelete).Select(x => new AdvertisementViewModel()
            //    {
            //        Id = x.Id,
            //        Title = x.Title,
            //        StartDate = x.StartDate.ToString(),
            //        EndDate =x.EndDate.ToString(),
            //        Price =x.Price,
            //        WebsiteUrl =x.WebsiteUrl,
            //        Owner =new UserViewModel() { FullName = x.Owner.FullName },
            //        ImageUrl =x.ImageUrl,
            //        Status = x.Stauts.ToString()

            //    }).Skip(skip).Take(10).ToList();
            //    var pagingResult = new paginationViewModel();
            //    pagingResult.Data = Advertisment;
            //    pagingResult.NumberOfPages = (int)pages;
            //    pagingResult.currentPage = page;

            //    return pagingResult;
            //}


            public async Task<List<ContentChangeLogViewModel>> GetLog(int id)
        {
            var changes = await _db.ContentChangeLogs.Where(x => x.ContentId == id && x.Type == ContentType.Advertisment).ToListAsync();
            return _mapper.Map<List<ContentChangeLogViewModel>>(changes);
        }

        public async Task<int> Delete(int id)
        {
            var advertisement = await _db.Advertisements.SingleOrDefaultAsync(x => x.Id == id && !x.IsDelete);
            if(advertisement == null)
            {
                throw new EntityNotFoundException();
            }
            advertisement.IsDelete = true;
            _db.Advertisements.Update(advertisement);
            await _db.SaveChangesAsync();
            return advertisement.Id;
        }

        public async Task<UpdateAdvertisementDto> Get(int id)
        {
            var advertisement = await _db.Advertisements.SingleOrDefaultAsync(x => x.Id == id && !x.IsDelete);
            if (advertisement == null)
            {
                throw new EntityNotFoundException();
            }
            return _mapper.Map<UpdateAdvertisementDto>(advertisement);
        }


        public async Task<int> Create(CreateAdvertisementDto dto)
        {

            if(dto.StartDate >= dto.EndDate)
            {
                throw new InvalidDateException();
            }

            var advertisement = _mapper.Map<Advertisement>(dto);
            if(dto.Image != null)
            {
                advertisement.ImageUrl = await _fileService.SaveFile(dto.Image, "Images");
            }

            if (!string.IsNullOrWhiteSpace(dto.OwnerId))
            {
                advertisement.OwnerId = dto.OwnerId;
            }

            await _db.Advertisements.AddAsync(advertisement);
            await _db.SaveChangesAsync();

            if (advertisement.OwnerId == null)
            {
                var userId = await _userService.Create(dto.Owner);
                advertisement.OwnerId = userId;

                _db.Advertisements.Update(advertisement);
                await _db.SaveChangesAsync();

            }

            return advertisement.Id;
        }


        public async Task<int> Update(UpdateAdvertisementDto dto)
        {

            if (dto.StartDate >= dto.EndDate)
            {
                throw new InvalidDateException();
            }

            var advertisement = await _db.Advertisements.SingleOrDefaultAsync(x => x.Id == dto.Id && !x.IsDelete);
            if(advertisement == null)
            {
                throw new EntityNotFoundException();
            }

            var updatedAdvertisement = _mapper.Map(dto, advertisement);

            if (dto.Image != null)
            {
                updatedAdvertisement.ImageUrl = await _fileService.SaveFile(dto.Image, "Images");
            }

             _db.Advertisements.Update(updatedAdvertisement);
             await _db.SaveChangesAsync();

            return advertisement.Id;
        }



        public async Task<int> UpdateStatus(int id, ContentStatus status)
        {
            var advertisement = await _db.Advertisements.SingleOrDefaultAsync(x => x.Id == id && !x.IsDelete);
            if (advertisement == null)
            {
                throw new EntityNotFoundException();
            }


            var changeLog = new ContentChangeLog();
            changeLog.ContentId = advertisement.Id;
            changeLog.Type = ContentType.Advertisment;
            changeLog.Old = advertisement.Stauts;
            changeLog.New = status;
            changeLog.ChangeAt = DateTime.Now;

            await _db.ContentChangeLogs.AddAsync(changeLog);
            await _db.SaveChangesAsync();

            advertisement.Stauts = status;
            _db.Advertisements.Update(advertisement);
            await _db.SaveChangesAsync();

            //await _emailService.Send(post.Author.Email, "UPDATE POST STATUS !", $"YOUR POST NOW IS {status.ToString()}");

            return advertisement.Id;
        }


        public async Task<byte[]> ExportToExcel()
        {
            var users = await _db.Advertisements.Include(x => x.Owner).Where(x => !x.IsDelete).ToListAsync();

            return ExcelHelpers.ToExcel(new Dictionary<string, ExcelColumn>
            {
                {"Title", new ExcelColumn("Title", 0)},
                {"Price", new ExcelColumn("Price", 1)},
                {"StartDate", new ExcelColumn("StartDate", 2)},
                {"EndDate", new ExcelColumn("EndDate", 3)},
                {"Owner", new ExcelColumn("Owner", 3)}
            }, new List<ExcelRow>(users.Select(e => new ExcelRow
            {
                Values = new Dictionary<string, string>
                {
                    {"Price", e.Price.ToString()},
                    {"Title", e.Title.ToString()},
                    {"StartDate", e.StartDate.ToString()},
                    {"EndDate", e.EndDate.ToString()},
                    {"Owner", e.Owner.FullName}
                }
            })));
        }
    }
}
