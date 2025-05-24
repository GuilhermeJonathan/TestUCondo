using AutoMapper;
using TestUCondo.Domain.Entities;
using TestUCondo.Application.CrossCutting.DTO.Accounts;

namespace TestUCondo.Application.Profiles
{
    public class AccountProfile : Profile
    {
        public AccountProfile()
        {
            CreateMap<Account, AccountDTO>();
        }
    }
}