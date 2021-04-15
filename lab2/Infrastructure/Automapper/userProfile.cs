using System.Collections.Generic;
using AutoMapper;
using EfModels.Models;

namespace lab2.Infrastructure.Automapper
{
    public class userProfile : Profile
    {
        public userProfile()
        {
            CreateMap<User, Domain.Models.User>().ReverseMap();
        }

    }

    
}