using AutoMapper;
using Practica02.Model;
using Practica02.Model.Dto;

namespace Practica02;

public class MappingConfig: Profile
{
    public MappingConfig()
    {
        CreateMap<UserModel, UserModelDTO>().ReverseMap();
    }
}