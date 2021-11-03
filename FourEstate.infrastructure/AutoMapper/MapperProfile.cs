using AutoMapper;
using FourEstate.Core.Dtos;
using FourEstate.Core.ViewModel;
using FourEstate.Core.ViewModels;
using FourEstate.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FourEstate.Infrastructure.AutoMapper
{

    public class MapperProfile : Profile
    {
        public MapperProfile()
        {
            CreateMap<User, UserViewModel>().ForMember(x => x.UserType,x => x.MapFrom(x => x.UserType.ToString()));
            CreateMap<CreateUserDto, User>().ForMember(x => x.ImageUrl, x => x.Ignore());
            CreateMap<UpdateUserDto, User>().ForMember(x => x.ImageUrl, x => x.Ignore());
            CreateMap<User, UpdateUserDto>().ForMember(x => x.Image, x => x.Ignore());

            CreateMap<Category, CategoryViewModel>();
            CreateMap<CreateCategoryDto, Category>();
            CreateMap<UpdateCategoryDto, Category>();
            CreateMap<Category, UpdateCategoryDto>();
            CreateMap<CategoryViewModel, paginationViewModel>();

            CreateMap<Location, LocationViewModel>().ForMember(x => x.Status, x => x.MapFrom(x => x.Stauts.ToString()));
            CreateMap<CreateLocationDto, Location>();
            CreateMap<UpdateLocationDto, Location>();
            CreateMap<Location, UpdateLocationDto>();

            CreateMap<Customer, CustomerViewModel>();
            CreateMap<CreateCustomerDto, Customer>().ForMember(x => x.ImageUrl, x => x.Ignore());
            CreateMap<UpdateCustomerDto, Customer>().ForMember(x => x.ImageUrl, x => x.Ignore());
            CreateMap<Customer, UpdateCustomerDto>().ForMember(x => x.ImageUrl, x => x.Ignore());
           
            
            CreateMap<RealEstate, RealEstateViewModel>().ForMember(x => x.Status, x => x.MapFrom(x => x.Stauts.ToString()));
            CreateMap<CreateRealEstateDto, RealEstate>().ForMember(x => x.Attachments, x => x.Ignore());
            CreateMap<UpdateRealEstateDto, RealEstate>().ForMember(x => x.Attachments, x => x.Ignore());
            CreateMap<RealEstate, UpdateRealEstateDto>().ForMember(x => x.Attachments, x => x.Ignore()).ForMember(x => x.RealEstateAttachments, x => x.Ignore());
            CreateMap<RealEstatetAttachment, RealEstateAttachmentViewModel>();


            CreateMap<Advertisement, AdvertisementViewModel>().ForMember(x => x.StartDate, x => x.MapFrom(x => x.StartDate.ToString("yyyy:MM:dd"))).ForMember(x => x.EndDate, x => x.MapFrom(x => x.EndDate.ToString("yyyy:MM:dd"))).ForMember(x => x.Status, x => x.MapFrom(x => x.Stauts.ToString())); ;
            CreateMap<CreateAdvertisementDto, Advertisement>().ForMember(x => x.ImageUrl, x => x.Ignore()).ForMember(x => x.Owner, x => x.Ignore());
            CreateMap<UpdateAdvertisementDto, Advertisement>().ForMember(x => x.ImageUrl, x => x.Ignore()).ForMember(x => x.Owner, x => x.Ignore());
            CreateMap<Advertisement, UpdateAdvertisementDto>().ForMember(x => x.Image, x => x.Ignore());


            CreateMap<Contract, ContractViewModel>().ForMember(x => x.Status, x => x.MapFrom(x => x.Stauts.ToString())); ;
            CreateMap<CreateContractDto, Contract>();
            CreateMap<UpdateContractDto, Contract>();
            CreateMap<Contract, UpdateContractDto>();



            CreateMap<ContentChangeLog, ContentChangeLogViewModel>();

        }
    }
}
