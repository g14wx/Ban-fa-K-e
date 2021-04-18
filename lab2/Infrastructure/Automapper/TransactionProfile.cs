using AutoMapper;
using lab2.Domain.DTOs;
using lab2.Domain.Models;

namespace lab2.Infrastructure.Automapper
{
    public class TransactionProfile : Profile
    {
        public TransactionProfile()
        {
            CreateMap<EfModels.Models.Transaccion, Transaccion>().ReverseMap();
        }
    }
}
