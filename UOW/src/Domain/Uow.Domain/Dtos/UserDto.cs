namespace Uow.Domain.Dtos;

[Serializable]
public sealed class UserDto
{
    public Guid Id { get; set; }

    public string Email { get; set; } = default!;
}