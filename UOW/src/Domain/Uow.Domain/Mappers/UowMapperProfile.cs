using AutoMapper;
using Uow.Domain.Dtos;
using Uow.Domain.Entities;

namespace Uow.Domain.Mappers
{
    public sealed class UowMapperProfile : Profile
    {
        public UowMapperProfile()
        {
            CreateMap<User, UserDto>().ReverseMap();
        }
    }
}
