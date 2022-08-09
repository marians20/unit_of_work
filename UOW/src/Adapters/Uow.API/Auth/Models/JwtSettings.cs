namespace Uow.API.Auth.Models
{
    public class JwtSettings
    {
        public bool ValidateIssuerSigningKey { get; set; }

        public string IssuerSigningKey { get; set; } = default!;

        public bool ValidateIssuer { get; set; } = true;

        public string ValidIssuer { get; set; } = default!;

        public bool ValidateAudience { get; set; } = true;

        public string ValidAudience { get; set; } = default!;

        public bool RequireExpirationTime { get; set; }

        public bool ValidateLifetime { get; set; } = true;
    }
}
