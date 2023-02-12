using Aplication.Models.Response.Usuario;

namespace Aplication.Interfaces;

public interface IUtilsApp
{
    public EnderecoResponse ConsultarEnderecoCep(string cep);
}