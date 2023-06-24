using Infraestrutura.Entity;

namespace Domain.Interfaces;

public interface IUsuarioService
{
    public Usuario? GetById(int id);
    public Usuario? GetByCpf(string cpf);
    public List<Usuario> GetAllList();
    public IQueryable<Usuario> GetAllQuery();
    public void Cadastrar(Usuario usuarioEntity);
    public Usuario CadastrarComRetorno(Usuario usuarioEntity);
    public void CadastrarListaUsuario(List<Usuario> lUsuario);
    public void Editar(Usuario usuario);
    public void EditarListaUsuario(List<Usuario> lUsuario);
    public void DeleteById(int id);
    public Usuario GetByIdWithInclude(int id);
    public IQueryable<Usuario>? GetTarefaByUsuario(List<int> list);
    public IQueryable<Usuario>? GetUsuarioTarefa();
}