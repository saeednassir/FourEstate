using FourEstate.Core.Dtos;
using FourEstate.Core.ViewModel;
using FourEstate.Core.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FourEstate.Infrastructure.Services.Categories
{
    public interface ICategoryService
    {
        Task<ResponseDto> GetAll(Pagination pagination, Query query);
        Task<List<CategoryViewModel>> GetAllAPI(string serachKey);
        

                 Task<List<CategoryViewModel>> GetCategoryName();
        Task<int> Create(CreateCategoryDto dto);

        Task<int> Update(UpdateCategoryDto dto);

        Task<UpdateCategoryDto> Get(int Id);

        Task<int> Delete(int Id);
        Task<byte[]> ExportToExcel();
    }
}
