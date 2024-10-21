using AutoMapper;
using Backend.DTOs;
using Backend.Models;
using Microsoft.EntityFrameworkCore.Migrations.Operations;

namespace Backend.Automappers
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<BeerInsertDto, Beer>();
            CreateMap<Beer, BeerDto>()
                .ForMember(dto => dto.Id,
                            m => m.MapFrom(b => b.BeerId));

            CreateMap<BeerUpdateDto, Beer>();
        }
    }
}
