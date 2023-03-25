using Aplication.Models.Grid;
using Aplication.Models.Request.Senha;
using Aplication.Models.Request.Usuario;
using Aplication.Models.Response.Usuario;
using Infraestrutura.Entity;
using Infraestrutura.Reports.Usuario.Objeto;
using ValidationResult = Aplication.Utils.Objeto.ValidationResult;

namespace Aplication.Interfaces;

public interface IUsuarioApp
{
    public List<Usuario> GetAll();
    public Usuario? GetByCpf(string cpf);
    public UsuarioCrudResponse GetById(int id);
    public ValidationResult Cadastrar(UsuarioRequest request);
    public UsuarioCadastroInicialResponse CadastroInicial(UsuarioRegistroInicialRequest request);
    public void CadastrarListaUsuario(List<Usuario> lUsuario);
    public ValidationResult Editar(UsuarioRequest request);
    public void EditarListaUsuario(List<Usuario> lUsuario);
    public void DeleteById(int id);
    public BaseGridResponse ConsultarGridUsuario(BaseGridRequest request);
    public List<UsuarioGridReportObj> ConsultarRelatorioUsuario(UsuarioRelatorioRequest request);
    public Usuario? GetByCpfEmail(string cpf, string email);
    public ValidationResult AlterarSenha(UsuarioAlterarSenhaRequest request);
    public ValidationResult ValidarCodigo(Usuario? usuario, ValidarCodigoRequest request);
    public Usuario? GetById(int? id);

}