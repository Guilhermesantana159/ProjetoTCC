using Domain.DTO.Correios;
using Infraestrutura.Entity;

namespace Domain.Interfaces;

public interface IUtilsService
{
    public Task<EnderecoExternalReponse> ConsultarEnderecoCep(string cep);
}