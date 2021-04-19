using AutoMapper;
using EfModels.Models;
using lab2.Domain.DTOs;

namespace lab2.Infrastructure.Automapper
{
    public class NotificacionProfile : Profile
    {
        public NotificacionProfile()
        {
            CreateMap<Notificacion,NotificacionDTO>().ReverseMap();
        }
    }
}