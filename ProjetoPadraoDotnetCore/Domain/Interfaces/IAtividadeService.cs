using Infraestrutura.Entity;

namespace Domain.Interfaces;

public interface IAtividadeService
{
    public Atividade CadastrarComRetorno(Atividade atividade);
    public void DeleteRangeAtividades(List<Atividade> list);
}