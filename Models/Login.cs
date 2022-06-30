namespace WebApiJwtAuth.Models
{
    public class Login
    {
        public string? UserName { get; set; }
        public string? Password { get; set; }
    }

    public class JwtTokenResponse
    {
        public string? Token { get; set; }
    }
}
