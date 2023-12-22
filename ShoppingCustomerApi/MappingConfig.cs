using AutoMapper;
using ShoppingCustomerApi.Model;
using ShoppingCustomerApi.Model.Dto;

namespace ShoppingCustomerApi
{
    public class MappingConfig
    {
        public static MapperConfiguration RegisterMaps()
        {
            var mappingConfig = new MapperConfiguration(config =>
            {
                config.CreateMap<CustomerDto, Customer>().ReverseMap();
                config.CreateMap<AddressesDto, Addresses>().ReverseMap();
            });
            return mappingConfig;
        }
    }
}
