using System;
using AutoMapper;
using BusinnesLogic.Models;
using BusinnesLogic.Dto;

namespace BusinnesLogic.Mappers
{
    public class CityProfile : Profile
    {
        public CityProfile()
        {
            CreateMap<City, CityDto>();
            CreateMap<CityItem, CityItemDto>();

            CreateMap<CityDto, City>();
            CreateMap<CityItemDto, CityItem>();
        }
    }
}
