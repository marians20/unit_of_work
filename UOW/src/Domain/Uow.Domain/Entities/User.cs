namespace Uow.Domain.Entities;

public class User : EntityWithTracking
{
    public string Email { get; set; } = default!;

    public string Password { get; set; } = default!;

    public string Salt { get; set; } = default!;

    public DateTime? ExpirationDate { get; set; }

    public bool IsLocked { get; set; } = false;

    public bool IsDeleted { get; set; } = false;

    public bool IsEmailConfirmed { get; set; } = false;
}