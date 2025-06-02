using AutoMapper;
using TestUCondo.Application.Commands.CustomerModule.Command;
using TestUCondo.Application.CrossCutting.DTO.Customers;

namespace TestUCondo.Application.Profiles
{
    public class CustomerProfile : Profile
    {
        public CustomerProfile()
        {
            CreateMap<CustomerApiModel, CustomerDTO>();                
            CreateMap<CreateCustomerCommand, CustomerDTO>().ReverseMap();                
        }
    }
}
