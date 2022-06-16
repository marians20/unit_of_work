using AutoMapper;
using Microsoft.AspNetCore.Http;
using Uow.Domain.Contracts;
using Uow.Domain.Dtos;
using Uow.Domain.Entities;
using Uow.Domain.Extensions;

namespace Uow.Domain.Services;

public sealed class UserService : IUserService
{
    private readonly IMapper _mapper;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IHttpContextAccessor _accessor;
    public UserService(IUnitOfWork unitOfWork, IMapper mapper, IHttpContextAccessor accessor)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
        _accessor = accessor;
    }

    public async Task<Guid> CreateAsync(UserDto user, CancellationToken cancellationToken)
    {
        user.Id = Guid.NewGuid();
        var requestId = _accessor.HttpContext.Request.Headers["x-request-id"];
        await _unitOfWork.UserRepository.CreateAsync(_mapper.Map<User>(user));
        await _unitOfWork.SaveChangesAsync(cancellationToken);
        return user.Id;
    }

    public async Task UpdateAsync(UserDto user, CancellationToken cancellationToken)
    {
        _unitOfWork.UserRepository.Update(_mapper.Map<User>(user));
        await _unitOfWork.SaveChangesAsync(cancellationToken);
    }

    public async Task DeleteAsync(Guid id, CancellationToken cancellationToken)
    {
        await _unitOfWork.UserRepository.DeleteAsync<User>(id, cancellationToken);
        await _unitOfWork.SaveChangesAsync(cancellationToken);
    }

    public async Task<IEnumerable<UserDto>> AllAsync(CancellationToken cancellationToken)
    {
        var requestId = _accessor.HttpContext.Request.GetRequestId();
        return _mapper.Map<IEnumerable<UserDto>>(await _unitOfWork.UserRepository.AllAsync<User>(cancellationToken));
    }

    public async Task<UserDto> GetByIdAsync(Guid id, CancellationToken cancellationToken) =>
        _mapper.Map<UserDto>(await _unitOfWork.UserRepository.GetByIdAsync<User>(id, cancellationToken));
}