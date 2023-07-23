using Aplication.Models.Request.Chat;
using Aplication.Models.Response.Chat;

namespace Aplication.Interfaces;

public interface IChatApp
{
    public ContatoResponse Cadastrar(ContatoRequest request);
    public void AlterarStatusContato(AlterarStatusContatoRequest request);
    public void DeletarContato(int id);
    public ContatoListaResponse ConsultarContatoPorIdPessoa(int id);
}