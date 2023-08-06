using System.Net;
using System.Text.Json;
using Domain.DTO.Correios;
using Domain.Interfaces;
using Infraestrutura.Entity;
using Infraestrutura.Repository.External;
using Infraestrutura.Repository.Interface.Feedback;

namespace Domain.Services;

public class UtilService : IUtilsService
{
    protected readonly IExternalRepository External;
    protected readonly IFeedbackWriteRepository FeedbackWriteRepository;

    private readonly IConfiguration _configuration;
    public UtilService(IExternalRepository external,IConfiguration config, IFeedbackWriteRepository feedbackWriteRepository)
    {
        External = external;
        _configuration = config;
        this.FeedbackWriteRepository = feedbackWriteRepository;
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
}