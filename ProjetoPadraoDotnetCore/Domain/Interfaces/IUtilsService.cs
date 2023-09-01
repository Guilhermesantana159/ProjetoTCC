using Domain.DTO.Correios;
using Infraestrutura.Entity;

namespace Domain.Interfaces;

public interface IUtilsService
{
    public Task<EnderecoExternalReponse> ConsultarEnderecoCep(string cep);
    public void SalvarFeedback(Feedback feedback);
    public void ContatoMensagem(ContatoMensagem request);
    public List<ContatoMensagem> GetAllContatoMensagem();
}