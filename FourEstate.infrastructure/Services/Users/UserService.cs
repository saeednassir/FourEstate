using AutoMapper;
using FourEstate.Core.Constants;
using FourEstate.Core.Dtos;
using FourEstate.Core.Exceptions;
using FourEstate.Core.ViewModel;
using FourEstate.Core.ViewModels;
using FourEstate.Data;
using FourEstate.Data.Models;
using FourEstate.Infrastructure.Helpers;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FourEstate.Infrastructure.Services.Users
{
    public class UserService : IUserService
    {
        private readonly FourEstateDbContext _db;
        private readonly IMapper _mapper;
        private readonly IFileService _fileService;
        private readonly UserManager<User> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        private readonly IEmailService _emailService;

        public UserService(IEmailService emailService, FourEstateDbContext db,IMapper mapper,UserManager<User> userManager, IFileService fileService, RoleManager<IdentityRole> roleManager)
        {
            _db = db;
            _mapper = mapper;
            _userManager = userManager;
            _fileService = fileService;
            _emailService = emailService;
            _roleManager = roleManager;
        }


        // mrthods to retub All Ptoparites in Base Controller to View In Cpanel
        public UserViewModel GetUserByName(string UserName)
        {

            var users = _userManager.Users.SingleOrDefault(x => x.UserName == UserName && !x.IsDelete);
            if (users == null) {
                throw new EntityNotFoundException();
            }
          return  _mapper.Map<UserViewModel>(users);
          
        }


        public async Task<ResponseDto> GetAll(Pagination pagination, Query query)
        {
            var queryString = _db.Users.Where(x => !x.IsDelete && (x.FullName.Contains(query.GeneralSearch) || string.IsNullOrWhiteSpace(query.GeneralSearch) || x.Email.Contains(query.GeneralSearch) || x.PhoneNumber.Contains(query.GeneralSearch))).AsQueryable();

            var dataCount = queryString.Count();
            var skipValue = pagination.GetSkipValue();
            var dataList = await queryString.Skip(skipValue).Take(pagination.PerPage).ToListAsync();
            var users = _mapper.Map<List<UserViewModel>>(dataList);
            var pages = pagination.GetPages(dataCount);
            var result = new ResponseDto
            {
                data = users,
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

        public async Task<List<UserViewModel>> GetAllAPI(string serachKey)
        {
            var users = await _db.Users.Where(x => x.FullName.Contains(serachKey) || x.PhoneNumber.Contains(serachKey) || string.IsNullOrWhiteSpace(serachKey)).ToListAsync();
            return _mapper.Map<List<UserViewModel>>(users);



        }
    


        public async Task<string> Create(CreateUserDto dto)
        {
            var emailOrPhoneIsExist = _db.Users.Any(x => !x.IsDelete && (x.Email == dto.Email || x.PhoneNumber == dto.PhoneNumber));

            if (emailOrPhoneIsExist)
            {
                throw new DuplicateEmailOrPhoneException();
            }

            var user = _mapper.Map<User>(dto);
            user.UserName = dto.Email;
            
            if(dto.Image != null)
            {
                user.ImageUrl =  await _fileService.SaveFile(dto.Image, FolderNames.ImagesFolder);
            }

            var password = GenratePassword();

            try
            {
                var result = await _userManager.CreateAsync(user, password);

                if (!result.Succeeded)
                {
                    throw new OperationFailedException();
                }

                //var oldrole = roleManager.FindById(olduser.Roles.FirstOrDefault().RoleId);
                //var role = roleManager.FindById(RoleId);
                //userManager.RemoveFromRole(UserId, oldrole.Name);
                //var result = userManager.AddToRole(UserId, role.Name);


                // Add Role To User
                await _userManager.AddToRoleAsync(user, user.UserType.ToString());
               

                return user.Id;

            }
            catch(Exception e)
            {
            }
          

            await _emailService.Send(user.Email, "New Account !", $"Username is : {user.Email} and Password is { password }");

            return user.Id;
        }

        public async Task<string> Update(UpdateUserDto dto)
        {
            var emailOrPhoneIsExist = _db.Users.Any(x => !x.IsDelete && (x.Email == dto.Email || x.PhoneNumber == dto.PhoneNumber && x.Id == dto.Id) && x.Id != dto.Id);
            if (emailOrPhoneIsExist)
            {
                throw new DuplicateEmailOrPhoneException();
            }
            var user = await _db.Users.FindAsync(dto.Id);
            var updatedUser = _mapper.Map<UpdateUserDto, User>(dto,user);
            if (dto.Image != null)
            {
                updatedUser.ImageUrl = await _fileService.SaveFile(dto.Image, FolderNames.ImagesFolder);
            }
            _db.Users.Update(updatedUser);
            await _db.SaveChangesAsync();

            // Add Role To User
        //    await _userManager.RemoveFromRoleAsync(user, user.UserType.ToString());
            await _userManager.AddToRoleAsync(user, user.UserType.ToString());
            return user.Id;
        }

        public async Task<string> Delete(string Id)
        {
            var user = await _db.Users.SingleOrDefaultAsync(x => x.Id == Id && !x.IsDelete);
            if(user == null)
            {
                throw new EntityNotFoundException();
            }
            user.IsDelete = true;
            _db.Users.Update(user);
            await _db.SaveChangesAsync();
            return user.Id;
        }


        public async Task<UpdateUserDto> Get(string Id)
        {
            var user = await _db.Users.SingleOrDefaultAsync(x => x.Id == Id && !x.IsDelete);
            if (user == null)
            {
                throw new EntityNotFoundException();
            }
            return _mapper.Map<UpdateUserDto>(user);
        }


        public async Task<byte[]> ExportToExcel()
        {
            var users = await _db.Users.Where(x => !x.IsDelete).ToListAsync();

            return ExcelHelpers.ToExcel(new Dictionary<string, ExcelColumn>
            {
                {"FullName", new ExcelColumn("FullName", 0)},
                {"Email", new ExcelColumn("Email", 1)},
                {"Phone", new ExcelColumn("Phone", 2)}
            }, new List<ExcelRow>(users.Select(e => new ExcelRow
            {
                Values = new Dictionary<string, string>
                {
                    {"FullName", e.FullName},
                    {"Email", e.Email},
                    {"Phone", e.PhoneNumber}
                }
            })));
        }


        private string GenratePassword()
        {
            return Guid.NewGuid().ToString().Substring(1, 8);
        }



    }
}
