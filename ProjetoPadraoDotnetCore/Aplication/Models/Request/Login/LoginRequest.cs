namespace Aplication.Models.Request.Login;

public class LoginRequest
{
    public string EmailLogin { get; set; } = null!;
    public string? SenhaLogin { get; set; } = null!;
}