namespace Uow.Domain.Dtos;

[Serializable]
public sealed class UserDto: DtoWithTracking
{
    public string? Email { get; set; }

    public string? Password { get; set; }

    public string? Salt { get; set; }

    public DateTime? ExpirationDate { get; set; }

    public bool? IsLocked { get; set; }

    public bool? IsDeleted { get; set; }

    public bool? IsEmailConfirmed { get; set; }

    public IEnumerable<RoleDto>? Roles { get; set; }
}