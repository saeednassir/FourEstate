using FourEstate.Core.Dtos;
using FourEstate.Core.Enums;
using FourEstate.Core.ViewModel;
using FourEstate.Core.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FourEstate.Infrastructure.Services.LocationsService
{
    public interface ILocationService
    {
        Task<ResponseDto> GetAll(Pagination pagination, Query query);
        Task<List<LocationViewModel>> GetLocationCountry();
        Task<List<LocationViewModel>> GetAllAPI(string serachKey);
        Task<int> Create(CreateLocationDto dto);

        Task<int> Update(UpdateLocationDto dto);

        Task<UpdateLocationDto> Get(int Id);

        Task<int> Delete(int Id);
        Task<int> UpdateStatus(int id, ContentStatus status);
        Task<List<ContentChangeLogViewModel>> GetLog(int id);
        Task<byte[]> ExportToExcel();
    }
}
