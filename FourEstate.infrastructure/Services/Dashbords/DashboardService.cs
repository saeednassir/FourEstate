using FourEstate.Core.ViewModels;
using FourEstate.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FourEstate.infrastructure.Services.Dashbords
{
    public class DashboardService :IDashboardService
    {

        private readonly FourEstateDbContext _db ;

        public DashboardService(FourEstateDbContext db)
        {
            _db = db;
        }


        public async Task<DashboardViewModel> GetData()
        {

            var dash= new DashboardViewModel();
            dash.NoumberOfCategory = await _db.Categories.CountAsync(x=>!x.IsDelete);
            dash.NoumberOfAdvertisment = await _db.Advertisements.CountAsync(x => !x.IsDelete);
            dash.NoumberOfLocation = await _db.Locations.CountAsync(x => !x.IsDelete);
            dash.NoumberOfContracts = await _db.Contracts.CountAsync(x => !x.IsDelete);
            dash.NoumberOfCustomer = await _db.Customers.CountAsync(x => !x.IsDelete);
            dash.NoumberOfRealEstate = await _db.RealEstates.CountAsync(x => !x.IsDelete);
            dash.NoumberOfUsers = await _db.Users.CountAsync(x => !x.IsDelete);
            return dash;
        }







        public async Task<List<PieChartViewModel>> GetUsersDataChart()
        {

            var chart = new List<PieChartViewModel>();
            chart.Add(new PieChartViewModel()
            {
             Key = "Administrator",
             Value = await _db.Users.CountAsync(x=> !x.IsDelete && x.UserType ==Core.Enums.UserType.Administrator),
             Color = "Red",
             });
            chart.Add(new PieChartViewModel()
            {
                Key = "Customer",
                Value = await _db.Users.CountAsync(x => !x.IsDelete && x.UserType == Core.Enums.UserType.Customer),
                Color = "Yellow",
            });
            chart.Add(new PieChartViewModel()
            {
                Key = "Advertisement Owner",
                Value = await _db.Users.CountAsync(x => !x.IsDelete && x.UserType == Core.Enums.UserType.AdvertisementOwner),
                Color = "Orange",
            });
      
       

            return chart;
        }



        public async Task<List<PieChartViewModel>> GetRealEstateDataChart()
        {

            var chart = new List<PieChartViewModel>();
            chart.Add(new PieChartViewModel()
            {
                Key = "department",
                Value = await _db.RealEstates.CountAsync(x => !x.IsDelete && x.RealEstateType == Core.Enums.RealEstateType.department),
                Color = "rgb(255, 99, 132)",
            });
            chart.Add(new PieChartViewModel()
            {
                Key = "Villa",
                Value = await _db.RealEstates.CountAsync(x => !x.IsDelete && x.RealEstateType == Core.Enums.RealEstateType.Villa),
                Color = "rgb(54, 162, 235)",
            });
            chart.Add(new PieChartViewModel()
            {
                Key = " ground",
                Value = await _db.RealEstates.CountAsync(x => !x.IsDelete && x.RealEstateType == Core.Enums.RealEstateType.ground),
                Color = "rgb(255, 206, 86)",
            });

            chart.Add(new PieChartViewModel()
            {
                Key = " house",
                Value = await _db.RealEstates.CountAsync(x => !x.IsDelete && x.RealEstateType == Core.Enums.RealEstateType.house),
                Color = "rgb(153, 102, 255)",

            }); chart.Add(new PieChartViewModel()
            {
                Key = " Comapny",
                Value = await _db.RealEstates.CountAsync(x => !x.IsDelete && x.RealEstateType == Core.Enums.RealEstateType.Comapny),
                Color = "rgb(255, 159, 64)",
            });

            return chart;
        }



        public async Task<List<PieChartViewModel>> GetContractDataChart()
        {

            var chart = new List<PieChartViewModel>();
            chart.Add(new PieChartViewModel()
            {
                Key = "rental",
                Value = await _db.Contracts.CountAsync(x => !x.IsDelete && x.ContractType == Core.Enums.ContractType.rental),
                Color = "rgb(255, 99, 132)",
            });
            chart.Add(new PieChartViewModel()
            {
                Key = "sale",
                Value = await _db.Contracts.CountAsync(x => !x.IsDelete && x.ContractType == Core.Enums.ContractType.sale),
                Color = "rgb(54, 162, 235)",
            });
            chart.Add(new PieChartViewModel()
            {
                Key = "buy",
                Value = await _db.Contracts.CountAsync(x => !x.IsDelete && x.ContractType == Core.Enums.ContractType.buy),
                Color = "rgb(255, 206, 86)",
            });



            return chart;
        }


    }
}
