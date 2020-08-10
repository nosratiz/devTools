namespace DevTools.Application.Account.Auth.ModelDto
{
    public class AuthenticationResult : TokenDto
    {
        public bool IsSuccess { get; set; }

        public string Error { get; set; }
    }

    public class TokenDto
    {
        public string AccessToken { get; set; }

        public int RoleId { get; set; }
    }
}