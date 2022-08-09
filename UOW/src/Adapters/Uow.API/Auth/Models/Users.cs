namespace Uow.API.Auth.Models
{
    public class User
    {
        public Guid Id { get; set; }

        public string UserName { get; set; } = default!;

        public string EmailId { get; set; } = default!;

        public string Password { get; set; } = default!;
    }
}
