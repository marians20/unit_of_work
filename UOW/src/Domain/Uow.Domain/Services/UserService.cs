using AutoMapper;
using Uow.Domain.Contracts;
using Uow.Domain.Dtos;
using Uow.Domain.Entities;

namespace Uow.Domain.Services;

public sealed class UserService : IUserService
{
    private readonly IGenericRepository _userRepository;
    private readonly IMapper _mapper;

    public UserService(IGenericRepository userRepository, IMapper mapper)
    {
        _userRepository = userRepository;
        _mapper = mapper;
    }

    public async Task<Guid> CreateAsync(UserDto user, CancellationToken cancellationToken)
    {
        user.Id = Guid.NewGuid();
        await _userRepository.CreateAsync(_mapper.Map<User>(user));
        await _userRepository.SaveChangesAsync(cancellationToken);
        return user.Id;
    }

    public async Task UpdateAsync(UserDto user, CancellationToken cancellationToken)
    {
        _userRepository.Update(_mapper.Map<User>(user));
        await _userRepository.SaveChangesAsync(cancellationToken);
    }

    public async Task DeleteAsync(Guid id, CancellationToken cancellationToken)
    {
        await _userRepository.DeleteAsync<User>(id, cancellationToken);
        await _userRepository.SaveChangesAsync(cancellationToken);
    }

    public async Task<IEnumerable<UserDto>> AllAsync(CancellationToken cancellationToken) =>
        _mapper.Map<IEnumerable<UserDto>>(await _userRepository.AllAsync<User>(cancellationToken));

    public async Task<UserDto> GetByIdAsync(Guid id, CancellationToken cancellationToken) =>
        _mapper.Map<UserDto>(await _userRepository.GetByIdAsync<User>(id, cancellationToken));
}