using Aplication.Models.Request.ModuloMenu;
using Aplication.Models.Response;
using Aplication.Models.Response.Menu;
using Aplication.Utils.Objeto;
using Infraestrutura.Entity;

namespace Aplication.Interfaces;

public interface IEstruturaMenuApp
{
    public ValidationResult IntegrarModulo(ModuloRequest request);
    public ValidationResult IntegrarMenu(MenuRequest request);
    public EstrututuraMenuResponse ConsultarEstruturaMenus(int idUsuario);

}