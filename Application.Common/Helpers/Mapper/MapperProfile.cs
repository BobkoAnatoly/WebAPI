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
        }
    }
}
