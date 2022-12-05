using AutoMapper;
using Pharmacy.Domain.Core;
using Pharmacy.Models;

namespace Pharmacy.Profiles
{
    internal class ProductMappingProfile : Profile
    {
        public ProductMappingProfile() 
        {
            CreateMap<Product, ProductModel>()
                .ReverseMap();

            CreateMap<ProductType, ProductTypeModel>()
                .ReverseMap();

            CreateMap<ProductAmount, ProductAmountModel>()
                .ReverseMap();

            CreateMap<SalesInfo, SalesInfoModel>()
                .ReverseMap();

            CreateMap<Warehouse, WarehouseModel>()
                .ReverseMap();
        }
    }
}
