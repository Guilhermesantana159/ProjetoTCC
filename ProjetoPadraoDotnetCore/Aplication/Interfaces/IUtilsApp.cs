using Aplication.Models.Request.Profissao;
using Aplication.Models.Response;
using Aplication.Models.Response.Base;
using Aplication.Models.Response.Usuario;

namespace Aplication.Interfaces;

public interface IUtilsApp
{
    public EnderecoResponse ConsultarEnderecoCep(string cep);
    public void CadastrarProfissao(ProfissaoCadastrarRequest request);
    public List<SelectBaseResponse> ConsultarProfissoes();
    public void EditarProfissao(ProfissaoEditarRequest profissao);
    public void DeletarProfissaoPorId(int id);
}