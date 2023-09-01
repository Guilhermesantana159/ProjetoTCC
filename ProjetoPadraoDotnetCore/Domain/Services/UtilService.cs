using System.Net;
using System.Text.Json;
using Domain.DTO.Correios;
using Domain.Interfaces;
using Infraestrutura.Entity;
using Infraestrutura.Repository.External;
using Infraestrutura.Repository.Interface.ContatoMensagem;
using Infraestrutura.Repository.Interface.Feedback;

namespace Domain.Services;

public class UtilService : IUtilsService
{
    protected readonly IExternalRepository External;
    protected readonly IContatoMensagemWriteRepository ContatoMensagemWrite;
    protected readonly IContatoMensagemReadRepository ContatoMensagemRead;
    protected readonly IFeedbackWriteRepository FeedbackWriteRepository;

    private readonly IConfiguration _configuration;
    public UtilService(IExternalRepository external,IConfiguration config, IFeedbackWriteRepository feedbackWriteRepository, IContatoMensagemWriteRepository contatoMensagemWrite, IContatoMensagemReadRepository contatoMensagemRead)
    {
        External = external;
        _configuration = config;
        FeedbackWriteRepository = feedbackWriteRepository;
        ContatoMensagemWrite = contatoMensagemWrite;
        ContatoMensagemRead = contatoMensagemRead;
    }
    public async Task<EnderecoExternalReponse> ConsultarEnderecoCep(string cep)
    {
        EnderecoExternalReponse retorno;
        var url = _configuration.GetSection("ApiCorreios:Link");       
        var requisicao = await External.SendWebHttp(url.Value + cep +"/json");

        if (requisicao.StatusCode == HttpStatusCode.OK)
        {
            if (requisicao.ObjetoJson != null)
            { 
                retorno = JsonSerializer.Deserialize<EnderecoExternalReponse>(requisicao.ObjetoJson)
                        ?? new EnderecoExternalReponse() { StatusApi = false, StatusCode = requisicao.StatusCode };

                if (string.IsNullOrEmpty(retorno.bairro) || string.IsNullOrEmpty(retorno.localidade) || string.IsNullOrEmpty(retorno.uf) 
                    || string.IsNullOrEmpty(retorno.logradouro))
                {
                    retorno.StatusApi = false;
                }
                
                return retorno;
            }
            
        }
        
        retorno = JsonSerializer.Deserialize<EnderecoExternalReponse>(requisicao.ObjetoJson ?? "")
               ?? new EnderecoExternalReponse() { StatusApi = false, StatusCode = requisicao.StatusCode };
        
        if (string.IsNullOrEmpty(retorno.bairro) || string.IsNullOrEmpty(retorno.localidade) || string.IsNullOrEmpty(retorno.uf) 
            || string.IsNullOrEmpty(retorno.logradouro))
        {
            retorno.StatusApi = false;
        }

        return retorno;
    }

    public void SalvarFeedback(Feedback feedback)
    {
        FeedbackWriteRepository.Add(feedback);
    }

    public void ContatoMensagem(ContatoMensagem request)
    {
        ContatoMensagemWrite.Add(request);
    }

    public List<ContatoMensagem> GetAllContatoMensagem()
    {
        return ContatoMensagemRead.GetAll().OrderByDescending(x => x.DataCadastro).ToList();
    }
}