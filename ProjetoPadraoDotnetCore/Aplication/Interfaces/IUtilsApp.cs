using Aplication.Models.Request.Utils;
using Aplication.Models.Response.Projeto;
using Aplication.Models.Response.Usuario;
using Infraestrutura.Entity;

namespace Aplication.Interfaces;

public interface IUtilsApp
{
    public EnderecoResponse ConsultarEnderecoCep(string cep);
    public FeedbackDataDashboard CadastrarFeedback(FeedbackRequest request);
    public void ContatoMensagem(ContatoMensagemRequest request);
    public List<ContatoMensagem> ConsultarContatoMensagem();
}