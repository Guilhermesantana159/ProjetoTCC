using Domain.DTO.Correios;
using Infraestrutura.Entity;

namespace Domain.Interfaces;

public interface IUtilsService
{
    public Task<EnderecoExternalReponse> ConsultarEnderecoCep(string cep);
    public IQueryable<Profissao> ConsultarProfissoes();
    public void CadastrarProfissao(Profissao profissao);
    public void EditarProfissao(Profissao profissao);
    public Profissao? GetProfissaoById(int id);
    public void DeletarProfissaoPorId(int id);
}