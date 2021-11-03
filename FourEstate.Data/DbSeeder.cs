using FourEstate.Core.Enums;
using FourEstate.Data.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FourEstate.Data
{
    public static class DbSeeder
    {
        public static IHost SeedDb(this IHost webHost)
        {
            using (var scope = webHost.Services.CreateScope())
            {
                try
                {

                    var userManager = scope.ServiceProvider.GetRequiredService<UserManager<User>>();
                    var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();
                    userManager.SeedUser().Wait();
                    roleManager.SeedRoles().Wait();

                    var context = scope.ServiceProvider.GetRequiredService<FourEstateDbContext>();
                    context.SeedLocation().Wait();
                    context.SeedCategory().Wait();
                    context.SeedCustomer().Wait();
                    context.SeedRealEstate().Wait();
                    context.SeedContract().Wait();
                    //   //context.SeedAdvertisement().Wait();

                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                  //  throw;
                }
            }
            return webHost;
        }

        public static async Task SeedUser(this UserManager<User> userManager)
        {
            if (await userManager.Users.AnyAsync())
            {
                return;
            }

            //var users = new List<User>();

            var user = new User()
            {
                Email = "FourEstateAdmin@portal.com",
                FullName = "FourEstate",
                UserName = "FourEstateAdministrator@portal.com",
                PhoneNumber = "0595555555",
                DOB = DateTime.Now,
                CreatedAt = DateTime.Now,
                // new DateTime(); to add my date
                UserType = UserType.Administrator,
                PhoneNumberConfirmed = true,
                EmailConfirmed = true,
                IsDelete = false
            };

            var user2 = new User()
            {
                Email = "FourEstateCustomer@portal.com",
                FullName = "FourEstate",
                UserName = "FourEstateCustomer@portal.com",
                PhoneNumber = "0595555555",
                DOB = DateTime.Now,
                CreatedAt = DateTime.Now,
                UserType = UserType.Customer,
                PhoneNumberConfirmed = true,
                EmailConfirmed = true,
                IsDelete = false
            };

            var user3 = new User()
            {
                Email = "FourEstateAdvertisementOwner@portal.com",
                FullName = "FourEstate",
                UserName = "FourEstateAdvertisementOwner@portal.com",
                PhoneNumber = "0595555555",
                DOB = DateTime.Now,
                CreatedAt = DateTime.Now,
                UserType = UserType.AdvertisementOwner,
                PhoneNumberConfirmed = true,
                EmailConfirmed = true,
                IsDelete = false
            };
            var user4 = new User()
            {
                Email = "FourEstateEmployee@portal.com",
                FullName = "FourEstate",
                UserName = "FourEstateEmployee@portal.com",
                PhoneNumber = "1231312321",
                DOB = DateTime.Now,
                CreatedAt = DateTime.Now,
                UserType = UserType.Employee,
                PhoneNumberConfirmed = true,
                EmailConfirmed = true,
                IsDelete = false
            };


            // User 1 Create
            await userManager.CreateAsync(user, "FourEstate2020$$");
            // User 2 Create
            await userManager.CreateAsync(user2, "FourEstate2020$$");
            // User 3 Create
            await userManager.CreateAsync(user3, "FourEstate2020$$");
            //// User 4 Create
            await userManager.CreateAsync(user4, "FourEstate2020$$");



            // user 1 Role
            await userManager.AddToRoleAsync(user, UserType.Administrator.ToString());
            // user 2 Role
            await userManager.AddToRoleAsync(user2, UserType.Customer.ToString());
            // user 3 Role
            await userManager.AddToRoleAsync(user3, UserType.AdvertisementOwner.ToString());
            //// user 4 Role
            await userManager.AddToRoleAsync(user4, UserType.Employee.ToString());

        }

        // Seed Roles for Enum 
        public static async Task SeedRoles(this RoleManager<IdentityRole> RoleManager)
        {

            if (!RoleManager.Roles.Any())
            {

                var roles = new List<string>();
                roles.Add(UserType.Administrator.ToString());
                roles.Add(UserType.Customer.ToString());
                roles.Add(UserType.Employee.ToString());
                roles.Add(UserType.AdvertisementOwner.ToString());


                foreach (var role in roles)
                {
                    await RoleManager.CreateAsync(new IdentityRole(role));
                }

            }

        }



        public static async Task SeedCategory(this FourEstateDbContext context)
        {

            if (await context.Categories.AnyAsync())
            {
                return;
            }
            var categoires = new List<Category>();

            var category = new Category();
            category.Name = "طبقة ثرية";
            category.CreatedAt = DateTime.Now;

            var category2 = new Category();
            category2.Name = "طبقة متوسطة";
            category2.CreatedAt = DateTime.Now;

            var category3 = new Category();
            category3.Name = "طبقة كوظفين";
            category3.CreatedAt = DateTime.Now;

            categoires.Add(category);
            categoires.Add(category2);
            categoires.Add(category3);

            await context.Categories.AddRangeAsync(categoires);
            await context.SaveChangesAsync();


        }

        public static async Task SeedLocation(this FourEstateDbContext context)
        {

            if (await context.Locations.AnyAsync())
            {
                return;
            }
            var locactions = new List<Location>();

            var location = new Location();
            location.Country = "فلسطين";
            location.City = "غزة";
            location.Street = "شارع فلسطين";
            location.StreetNumber = "43241";
            location.CreatedAt = DateTime.Now;
            location.IsDelete = false;
          
            var location2 = new Location();
            location2.Country = "فلسطين";
            location2.City = "خانيونس";
            location2.Street = "شارع القسام";
            location2.StreetNumber = "32133";
            location2.CreatedAt = DateTime.Now;
            location2.IsDelete = false;
           
            var location3 = new Location();
            location3.Country = "فلسطين";
            location3.City = "دير البلح";
            location3.Street = "شارع أبو الديب";
            location.StreetNumber = "23233";
            location3.CreatedAt = DateTime.Now;
            location3.IsDelete = false;


            var location4 = new Location();
            location4.Country = "فلسطين";
            location4.City = "الشمال";
            location4.Street = "شارع العامودي";
            location4.StreetNumber = "13223233";
            location4.CreatedAt = DateTime.Now;
            location4.IsDelete = false;

            locactions.Add(location);
            locactions.Add(location2);
            locactions.Add(location3);
            locactions.Add(location4);

            await context.Locations.AddRangeAsync(locactions);
            await context.SaveChangesAsync();


        }



        public static async Task SeedCustomer(this FourEstateDbContext context)
        {

            if (await context.Customers.AnyAsync())
            {
                return;
            }
            var customers = new List<Customer>();

            var customer = new Customer();
            customer.FirstName = "احمد";
            customer.LastName = "العامودي";
            customer.FullName = $"{customer.FirstName} {customer.LastName}";
            customer.DOB = DateTime.Now;
            customer.ImageUrl = "";
            customer.Phone = "0831213312";
            customer.LocationId = 1;
            customer.CreatedAt = DateTime.Now;
            customer.IsDelete = false;

            var customer2 = new Customer();
            customer2.FirstName = "هند";
            customer2.LastName = "مكي";
            customer2.FullName = $"{customer2.FirstName}  {customer2.LastName}";
            customer2.DOB = DateTime.Now;
            customer2.ImageUrl = "";
            customer.Phone = "31241242";
            customer2.LocationId = 2;
            customer2.CreatedAt = DateTime.Now;
            customer2.IsDelete = false;

            var customer3 = new Customer();
            customer3.FirstName = "روان";
            customer3.LastName = "بكر";
            customer3.FullName = $"{customer3.FirstName}  {customer3.LastName}";
            customer3.DOB = DateTime.Now;
            customer3.ImageUrl = "";
            customer3.Phone = "132424";
            customer3.LocationId = 3;
            customer3.CreatedAt = DateTime.Now;
            customer3.IsDelete = false;

            var customer4 = new Customer();
            customer4.FirstName = "حازم";
            customer4.LastName = "سرداح";
            customer4.FullName = $"{customer4.FirstName}  {customer4.LastName}";
            customer4.DOB = DateTime.Now;
            customer4.ImageUrl = "";
            customer4.Phone = "12312";
            customer4.LocationId = 4;
            customer4.CreatedAt = DateTime.Now;
            customer4.IsDelete = false;

            customers.Add(customer);
            customers.Add(customer2);
            customers.Add(customer3);
            customers.Add(customer4);

            await context.Customers.AddRangeAsync(customers);
            await context.SaveChangesAsync();


        }




        public static async Task SeedRealEstate(this FourEstateDbContext context)
        {

            if (await context.RealEstates.AnyAsync())
            {
                return;
            }
            var realEstates = new List<RealEstate>();

            var realEstate = new RealEstate();
            realEstate.Name = "العامودي";
            realEstate.Description = "عقار من 4 غرف";
            //  realEstate.Attachments = ;
            realEstate.CategoryId = 1;
            realEstate.RealEstateType = Core.Enums.RealEstateType.department;
            realEstate.LocationId = 1;
            realEstate.CreatedAt = DateTime.Now;
            realEstate.IsDelete = false;

            var realEstate2 = new RealEstate();
            realEstate2.Name = "مكي للعقارات";
            realEstate2.Description = "عقار من 4 طوابق";
            //  realEstate.Attachments = ;
            realEstate2.CategoryId = 2;
            realEstate2.RealEstateType = Core.Enums.RealEstateType.Comapny;
            realEstate2.LocationId = 2;
            realEstate2.CreatedAt = DateTime.Now;
            realEstate2.IsDelete = false;


            var realEstate3 = new RealEstate();
            realEstate3.Name = "بكر شاليه";
            realEstate3.Description = "عقار من 2 غرف ومسبح و صالة";
            //  realEstate.Attachments = ;
            realEstate3.CategoryId = 1;
            realEstate3.RealEstateType = Core.Enums.RealEstateType.ground;
            realEstate3.LocationId = 3;
            realEstate3.CreatedAt = DateTime.Now;
            realEstate3.IsDelete = false;


            var realEstate4 = new RealEstate();
            realEstate4.Name = "سرداح فيلا";
            realEstate4.Description = "عقار من 20 غرف ومسبح";
            //  realEstate.Attachments = ;
            realEstate4.CategoryId = 3;
            realEstate4.RealEstateType = Core.Enums.RealEstateType.Villa;
            realEstate4.LocationId = 4;
            realEstate4.CreatedAt = DateTime.Now;
            realEstate4.IsDelete = false;

            realEstates.Add(realEstate);
            realEstates.Add(realEstate2);
            realEstates.Add(realEstate3);
            realEstates.Add(realEstate4);



            await context.RealEstates.AddRangeAsync(realEstates);
            await context.SaveChangesAsync();


        }



        public static async Task SeedAdvertisement(this FourEstateDbContext context)
        {

            if (await context.Advertisements.AnyAsync())
            {
                return;
            }

            var advertisements = new Advertisement();
            advertisements.Title = "العامودي";
            advertisements.Price = 4444;
            advertisements.ImageUrl = "";
            advertisements.WebsiteUrl = "https://localhost:44345/Advertisement/Index";
            advertisements.OwnerId = UserType.AdvertisementOwner.ToString();
            advertisements.CreatedAt = DateTime.Now;
            advertisements.StartDate = DateTime.Now;
            advertisements.EndDate = DateTime.Now;
            advertisements.IsDelete = false;
            await context.Advertisements.AddAsync(advertisements);
            await context.SaveChangesAsync();


        }

        public static async Task SeedContract(this FourEstateDbContext context)
        {

            if (await context.Contracts.AnyAsync())
            {
                return;
            }
            var contracts = new List<Contract>();

            var contract = new Contract();
            contract.RealEstateId = 1;
            contract.Price = 4444;
            contract.CustomerId = 1;
            contract.ContractType = ContractType.sale;
            contract.CreatedAt = DateTime.Now;
            contract.IsDelete = false;

            var contract2 = new Contract();
            contract2.RealEstateId = 2;
            contract2.Price = 444432;
            contract2.CustomerId = 2;
            contract2.ContractType = Core.Enums.ContractType.rental;
            contract2.CreatedAt = DateTime.Now;
            contract2.IsDelete = false;

            var contract3 = new Contract();
            contract3.RealEstateId = 3;
            contract3.Price = 44141;
            contract3.CustomerId = 3;
            contract3.ContractType = Core.Enums.ContractType.buy;
            contract3.CreatedAt = DateTime.Now;
            contract3.IsDelete = false;

            var contract4 = new Contract();
            contract4.RealEstateId = 4;
            contract4.Price = 4314;
            contract4.CustomerId = 4;
            contract4.ContractType = Core.Enums.ContractType.sale;
            contract4.CreatedAt = DateTime.Now;
            contract4.IsDelete = false;

            contracts.Add(contract);
            contracts.Add(contract2);
            contracts.Add(contract3);
            contracts.Add(contract4);

            await context.Contracts.AddRangeAsync(contracts);
            await context.SaveChangesAsync();


        }


    }
}

