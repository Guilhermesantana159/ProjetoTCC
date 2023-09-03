using Aplication.Models.Request.ModuloMenu;
using Aplication.Models.Response.Menu;
using Aplication.Utils.Objeto;
using Infraestrutura.Entity;

namespace Aplication.Interfaces;

public interface IEstruturaMenuApp
{
    public ValidationResult IntegrarSubModulo(SubModuloRequest request);
    public ValidationResult IntegrarMenu(MenuRequest request);
    public EstrututuraMenuResponse ConsultarEstruturaMenus(int idUsuario);
    public AutoCompleteMenuResponse ConsultarAutoCompleteMenu(int idUsuario);
    public ValidationResult IntegrarModulo(ModuloRequest request);
    public List<Menu> ConsultarMenus();
    public List<Modulo> ConsultarModulo();
    public List<SubModulo> ConsultarSubModulo();
}