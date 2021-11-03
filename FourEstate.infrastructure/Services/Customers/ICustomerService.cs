using FourEstate.Core.Dtos;
using FourEstate.Core.ViewModel;
using FourEstate.Core.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FourEstate.Infrastructure.Services.Customers
{
    public interface ICustomerService
    {
        Task<ResponseDto> GetAll(Pagination pagination, Query query);

        Task<List<CustomerViewModel>> GetAllAPI(string serachKey);
        Task<List<CustomerViewModel>> GetCustomerName();

        Task<int> Create(CreateCustomerDto dto);

        Task<int> Update(UpdateCustomerDto dto);

        Task<UpdateCustomerDto> Get(int Id);

        Task<int> Delete(int Id);
        Task<byte[]> ExportToExcel();
    }
}
