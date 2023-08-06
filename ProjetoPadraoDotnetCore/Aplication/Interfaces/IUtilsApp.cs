using Aplication.Models.Request.Utils;
using Aplication.Models.Response.Projeto;
using Aplication.Models.Response.Usuario;

namespace Aplication.Interfaces;

public interface IUtilsApp
{
    public EnderecoResponse ConsultarEnderecoCep(string cep);
    public FeedbackDataDashboard CadastrarFeedback(FeedbackRequest request);
}