using AutoMapper;
using EfModels.Models.ProductosFinancieros;

namespace lab2.Infrastructure.Automapper
{
    public class CheckingAccountProfile : Profile
    {
        public CheckingAccountProfile()
        {
            CreateMap<CuentaCorriente, Domain.Models.CuentaCorriente>().ReverseMap();
        }
    }
}