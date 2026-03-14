using AutoMapper;
using ContactMangerAPI.Models;
using ContactMangerAPI.Models.DTO;

namespace ContactMangerAPI.Mappings
{
    public class AutoMapperProfiles : Profile
    {
        public AutoMapperProfiles()
        {
            CreateMap<Contact, ContactReadDto>().ReverseMap();
            CreateMap<Contact, ContactCreateDto>().ReverseMap();
        }
    }
}
