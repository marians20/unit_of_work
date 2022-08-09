using System.ComponentModel.DataAnnotations;
namespace Uow.API.Auth.Models;

public class UserLogin
{
    [Required]
    public string UserName { get; set; } = default!;
    [Required]
    public string Password { get; set; } = default!;

    public UserLogin() { }
}