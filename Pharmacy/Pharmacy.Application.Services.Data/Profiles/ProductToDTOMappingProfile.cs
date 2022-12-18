using AutoMapper;
using Pharmacy.Domain.Core;
using Pharmacy.Infrastructure.Data.DTO;

namespace Pharmacy.Profiles
{
    internal class ProductToDTOMappingProfile : Profile
    {
        public ProductToDTOMappingProfile() 
        {
            CreateMap<Product, ProductDTO>()
                .ReverseMap();

            CreateMap<ProductType, ProductTypeDTO>()
                .ReverseMap();

            CreateMap<ProductAmount, ProductAmountDTO>()
                .ReverseMap();

            CreateMap<SalesInfo, SalesInfoDTO>()
                .ReverseMap();

            CreateMap<Warehouse, WarehouseDTO>()
                .ReverseMap();
        }
    }
}
