namespace DevTools.Application.Account.Auth.ModelDto
{
    public class AuthenticationResult : TokenDto
    {
        public bool IsSuccess { get; set; }

        public string Error { get; set; }
    }
}