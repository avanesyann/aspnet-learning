using AutoMapper;
using NZWalks.API.Models.Domain;
using NZWalks.API.Models.DTO;

namespace NZWalks.API.Mappings
{
    public class AutoMapperProfiles : Profile
    {
        public AutoMapperProfiles()
        {
            CreateMap<Region, RegionReadDto>().ReverseMap();
            CreateMap<Region, RegionCreateDto>().ReverseMap();
            CreateMap<Region, RegionUpdateDto>().ReverseMap();

            CreateMap<Walk, WalkCreateDto>().ReverseMap();
            CreateMap<Walk, WalkReadDto>().ReverseMap();

            CreateMap<Difficulty, DifficultyReadDto>().ReverseMap();
        }
    }
}
