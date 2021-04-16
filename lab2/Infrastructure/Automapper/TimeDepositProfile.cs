using AutoMapper;
using EfModels.Models.ProductosFinancieros;

namespace lab2.Infrastructure.Automapper
{
    public class TimeDepositProfile : Profile
    {
        public TimeDepositProfile()
        {
            CreateMap<DepositoAPlazo, Domain.Models.DepositoAPlazo>().ReverseMap();
        }
    }
}