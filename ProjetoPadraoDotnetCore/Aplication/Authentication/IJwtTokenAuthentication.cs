namespace Aplication.Authentication;

public interface IJwtTokenAuthentication
{
    public object GerarToken(string cpf);
}