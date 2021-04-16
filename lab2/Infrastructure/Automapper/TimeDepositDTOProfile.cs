using AutoMapper;
using lab2.Domain.DTOs;
using lab2.Domain.Models;

namespace lab2.Infrastructure.Automapper
{
    public class TimeDepositDTOProfile : Profile
    {
        public TimeDepositDTOProfile()
        {
            CreateMap<DepositoAPlazo,DepositoAPlazoDTO>().ReverseMap();
        }
    }
}