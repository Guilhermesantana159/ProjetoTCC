namespace Aplication.Utils.Email;

public interface IEmailHelper
{
    public bool EnviarEmail(List<string> email, string titulo, string corpo);
}