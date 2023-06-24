using Domain.Interfaces;
using Infraestrutura.Entity;
using Infraestrutura.Repository.Interface.Projeto;

namespace Domain.Services;

public class ProjetoService : IProjetoService
{
    protected readonly IProjetoReadRepository ReadRepository;
    protected readonly IProjetoWriteRepository WriteRepository;

    public ProjetoService(IProjetoReadRepository readRepository, IProjetoWriteRepository writeRepository)
    {
        ReadRepository = readRepository;
        WriteRepository = writeRepository;
    }

    public Projeto CadastrarComRetorno(Projeto projeto)
    {
        return WriteRepository.AddWithReturn(projeto);
    }
    
    public Projeto EditarComRetorno(Projeto projeto)
    {
        return WriteRepository.UpdateWithReturn(projeto);
    }
    
    public IQueryable<Projeto> GetAllQuery()
    {
        return ReadRepository.GetAll();
    }
    
    public IQueryable<Projeto> GetAllWithIncludeQuery()
    {
        return ReadRepository.GetAllWithIncludeQuery();
    }
    
    public Projeto? GetById(int id)
    {
        return ReadRepository.GetById(id);
    }
    
    public void Cadastrar(Projeto projeto)
    { 
        WriteRepository.Add(projeto);
    }
    
    public Projeto? GetByIdWithIncludes(int id)
    {
        return ReadRepository.GetByIdWithInclude(id);
    }
    
    public void Editar(Projeto projeto)
    {
        WriteRepository.Update(projeto);
    }
    
    public void DeleteById(int idProjeto)
    {
        WriteRepository.DeleteById(idProjeto);
    }
}