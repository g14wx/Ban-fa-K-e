using AutoMapper;
using lab2.Domain.Models;

namespace lab2.Infrastructure.Automapper
{
    public class SavingAccountProfile : Profile
    {
        public SavingAccountProfile()
        {
            CreateMap<EfModels.Models.ProductosFinancieros.CuentaAhorro, CuentaAhorro>().ReverseMap();
        }
    }
}