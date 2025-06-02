    using AutoMapper;
using TestUCondo.Application.CrossCutting.DTO.Customers;

namespace TestUCondo.Application.Profiles
{
    public class CustomerProfile : Profile
    {
        public CustomerProfile()
        {
            CreateMap<CustomerApiModel, CustomerDTO>()
                .ForMember(dest => dest.Id, opt => opt.Ignore()); // Ajuste conforme necessário
        }   
    }
}