using AutoMapper;
using ToDoList.API.Model;

namespace ToDoList.API.Mappings
{
    public class AutoMapperProfiles : Profile
    {
        public AutoMapperProfiles()
        {
            CreateMap<ToDoCreateDto, ToDo>();
            CreateMap<ToDo, ToDoReadDto>().ReverseMap();
        }
    }
}
