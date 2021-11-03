using FourEstate.Core.Dtos;
using FourEstate.Core.ViewModel;
using FourEstate.Core.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FourEstate.Infrastructure.Services.Users
{
    public interface IUserService
    {
        Task<ResponseDto> GetAll(Pagination pagination, Query query);
        Task<List<UserViewModel>> GetAllAPI(string serachKey);

        UserViewModel GetUserByName(string UserName);
        Task<string> Create(CreateUserDto dto);
        Task<string> Update(UpdateUserDto dto);
        Task<string> Delete(string Id);
        Task<UpdateUserDto> Get(string Id);
        Task<byte[]> ExportToExcel();

    }
}
