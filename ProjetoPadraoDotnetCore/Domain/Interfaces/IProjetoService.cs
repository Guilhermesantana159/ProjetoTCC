using Infraestrutura.Entity;

namespace Domain.Interfaces;

public interface IProjetoService
{
    public Projeto CadastrarComRetorno(Projeto projeto);
    public IQueryable<Projeto> GetAllQuery();
    public Projeto? GetById(int id);
    public void Cadastrar(Projeto projeto);
    public Projeto? GetByIdWithIncludes(int id);
    public void Editar(Projeto projeto);
    public void DeleteById(int idProjeto);
    public IQueryable<Projeto> GetAllWithIncludeQuery();
    public Projeto EditarComRetorno(Projeto projeto);
}