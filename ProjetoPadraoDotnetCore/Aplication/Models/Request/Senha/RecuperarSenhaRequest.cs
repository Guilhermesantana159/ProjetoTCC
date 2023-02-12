namespace Aplication.Models.Request.Senha;

public class RecuperarSenhaRequest
{
    public string EmailRecover { get; set; } = null!;
    public string CpfRecover { get; set; } = null!;
}