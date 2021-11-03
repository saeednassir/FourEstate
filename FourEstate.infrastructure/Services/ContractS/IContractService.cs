using FourEstate.Core.Dtos;
using FourEstate.Core.Enums;
using FourEstate.Core.ViewModel;
using FourEstate.Core.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FourEstate.infrastructure.Services.ContractSS
{
 public interface IContractService
    {
        Task<ResponseDto> GetAll(Pagination pagination, Query query);
        Task<List<ContractViewModel>> GetAllAPI(string serachKey);
        Task<int> Delete(int Id);
        Task<UpdateContractDto> Get(int Id);
        Task<int> Update(UpdateContractDto dto);
        Task<int> Create(CreateContractDto dto);
        Task<List<ContractViewModel>> GetContractName();
        Task<int> UpdateStatus(int id, ContentStatus status);
        Task<List<ContentChangeLogViewModel>> GetLog(int id);
        Task<byte[]> ExportToExcel();
    }
}
