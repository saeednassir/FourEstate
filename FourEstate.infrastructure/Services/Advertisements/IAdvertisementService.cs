using FourEstate.Core.Dtos;
using FourEstate.Core.Enums;
using FourEstate.Core.ViewModel;
using FourEstate.Core.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FourEstate.Infrastructure.Services.Advertisements
{
    public interface IAdvertisementService
    {
        Task<ResponseDto> GetAll(Pagination pagination, Query query);
        Task<List<AdvertisementViewModel>> GetAllAPI(string serachKey);
        Task<int> Delete(int id);
        Task<int> Create(CreateAdvertisementDto dto);
        Task<List<UserViewModel>> GetAdvertisementOwners();

        Task<UpdateAdvertisementDto> Get(int id);
        Task<int> Update(UpdateAdvertisementDto dto);
        Task<int> UpdateStatus(int id, ContentStatus status);
        Task<List<ContentChangeLogViewModel>> GetLog(int id);
        Task<byte[]> ExportToExcel();

    }
}
