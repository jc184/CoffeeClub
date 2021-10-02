using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Entities.DTOs;
using Entities.Models;

namespace CoffeeClub
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Coffee, CoffeeDTO>();
            CreateMap<Comments, CommentsDTO>();
            CreateMap<CoffeeForCreationDTO, Coffee>();
            CreateMap<CoffeeForUpdateDTO, Coffee>();
            CreateMap<CommentsForCreationDTO, Comments>();
            CreateMap<CommentsForUpdateDTO, Comments>();
        }
    }
}
