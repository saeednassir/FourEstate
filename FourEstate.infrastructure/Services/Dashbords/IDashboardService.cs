using FourEstate.Core.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FourEstate.infrastructure.Services.Dashbords
{
    public interface IDashboardService
    {
        Task<DashboardViewModel> GetData();
        Task<List<PieChartViewModel>> GetUsersDataChart();
        Task<List<PieChartViewModel>> GetContractDataChart();
        Task<List<PieChartViewModel>> GetRealEstateDataChart();
    }
}
