using AutoMapper;
using Uow.Domain.Cryptography;
using Uow.Domain.Dtos;
using Uow.Domain.Entities;

namespace Uow.Domain.Mappers;

public sealed class UowMapperProfile : Profile
{
    public UowMapperProfile()
    {
        CreateMap<User, UserDto>().ReverseMap();
        CreateMap<UserCreateDto, User>()
            .ForMember(e => e.ExpirationDate, f => f.MapFrom(dto => DateTime.Now.AddYears(1)))
            .AfterMap((dto, entity) =>
            {
                entity.Salt = PasswordHasher.GenerateSalt();
                entity.Password = PasswordHasher.HashPassword(dto.Password ?? string.Empty, entity.Salt);
            });
    }
}