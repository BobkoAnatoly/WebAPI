using Application.Common.Models;
using Application.Model.Models;
using AutoMapper;

namespace Application.Common.Helpers.Mapper
{
    public class MapperProfile : Profile
    {
        public MapperProfile()
        {
            CreateMap<RegisterModel, User>().ReverseMap();
            CreateMap<User, UserModel>().ReverseMap();
            CreateMap<Note, NoteModel>()
                .ForMember(x => x.ImagePath, src => src.MapFrom(src => src.ImagePath))
                .ForMember(x => x.Title, src => src.MapFrom(src => src.Title))
                .ForMember(x => x.Description, src => src.MapFrom(src => src.Description))
                .ReverseMap();
            CreateMap<List<Note>, List<NoteModel>>().ReverseMap();
        }
    }
}
