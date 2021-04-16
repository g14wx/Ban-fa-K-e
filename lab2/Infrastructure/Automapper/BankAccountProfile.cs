using AutoMapper;
using EfModels.Models;

namespace lab2.Infrastructure.Automapper
{
    public class BankAccountProfile : Profile
    {
        public BankAccountProfile()
        {
            CreateMap<CuentaBancaria, Domain.Models.CuentaBancaria>().ReverseMap();
        }
    }
}