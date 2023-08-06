using Infraestrutura.Entity;

namespace Domain.Interfaces;

public interface IAtividadeService
{
    public Atividade CadastrarComRetorno(Atividade atividade);
    public void DeleteRangeAtividades(List<Atividade> list);
    public Atividade EditarComRetorno(Atividade atividade);
    public Atividade? GetByIdWithInclude(int idAtividade);
    public List<Atividade> GetByIdProjeto(int idProjeto);
    public void DeleteById(Atividade atividade);
    public Atividade? GetById(int idAtividade);
    public IQueryable<Atividade> GetAllWithInclude();

}