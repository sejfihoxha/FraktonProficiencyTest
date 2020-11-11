using AutoMapper;
using FraktonProficiencyTest.Data.Entities;
using FraktonProficiencyTest.Models;

namespace FraktonProficiencyTest.Helpers
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<User, RegisterModel>().ReverseMap();

            CreateMap<UserCoinsFavourite, UserCoinsFavouriteCreateModel>().ReverseMap();
            CreateMap<UserCoinsFavourite, UserCoinsFavouriteModel>().ReverseMap();
        }

    }
}
