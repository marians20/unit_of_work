// <copyright file="UowMapperProfile.cs" company="Microsoft">
//      Copyright (c) Microsoft Corporation.  All rights reserved.
// </copyright>
using AutoMapper;
using Uow.API.Services;
using Uow.ApplicationServices.Cryptography;
using Uow.Domain.Entities;
using Uow.PrimaryPorts.Dtos;

namespace Uow.ApplicationServices.Mappers;

public sealed class UowMapperProfile : Profile
{
    private readonly IUserResolverService? userResolver;
    public UowMapperProfile()
    {
        userResolver = ServiceLocator.GetService<IUserResolverService>();
        CreateUserMappings();
        CreateRoleMappings();
    }

    private void CreateUserMappings()
    {
        CreateMap<User, UserDto>();
        CreateMap<UserDto, User>()
            .ForMember(e => e.ModifiedBy, f => f.MapFrom(_ => userResolver.GetUserId()))
            .ForMember(e => e.ModifiedDate, f => f.MapFrom(_ => DateTime.UtcNow));

        CreateMap<UserCreateDto, User>()
            .ForMember(e => e.Id, f => f.MapFrom(_ => Guid.NewGuid()))
            .ForMember(e => e.ExpirationDate, f => f.MapFrom(_ => DateTime.Now.AddYears(1)))
            .ForMember(e => e.CreatedBy, f => f.MapFrom(_ => userResolver.GetUserId()))
            .ForMember(e => e.CreationDate, f => f.MapFrom(_ => DateTime.UtcNow))
            .AfterMap((dto, entity) =>
            {
                entity.Salt = PasswordHasher.GenerateSalt();
                entity.Password = PasswordHasher.HashPassword(dto.Password ?? string.Empty, entity.Salt);
            });
    }

    private void CreateRoleMappings()
    {
        CreateMap<Role, RoleDto>();
        CreateMap<RoleDto, Role>()
            .ForMember(e => e.ModifiedBy, f => f.MapFrom(_ => userResolver.GetUserId()))
            .ForMember(e => e.ModifiedDate, f => f.MapFrom(_ => DateTime.UtcNow));

        CreateMap<RoleCreateDto, Role>()
            .ForMember(e => e.Id, f => f.MapFrom(_ => Guid.NewGuid()))
            .ForMember(e => e.CreatedBy, f => f.MapFrom(_ => userResolver.GetUserId()))
            .ForMember(e => e.CreationDate, f => f.MapFrom(_ => DateTime.UtcNow));
    }
}