using Infraestrutura.Repository.Interface.Base;

namespace Infraestrutura.Repository.Interface.SkillUsuario;

public interface ISkillUsuarioWriteRepository : IBaseWriteRepository<Entity.SkillUsuario>
{
    public void RemoveSkillByUsuario(int id);
}