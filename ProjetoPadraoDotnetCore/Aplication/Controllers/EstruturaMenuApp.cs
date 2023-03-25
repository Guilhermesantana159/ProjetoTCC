using Aplication.Interfaces;
using Aplication.Models.Request.ModuloMenu;
using Aplication.Models.Response.Menu;
using Aplication.Utils.Objeto;
using Aplication.Validators.EstruturaMenu;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Domain.Interfaces;
using Infraestrutura.Entity;

namespace Aplication.Controllers;

public class EstruturaMenuApp : IEstruturaMenuApp
{
    protected readonly IEstruturaMenuService Service;
    protected readonly IMapper Mapper;
    protected readonly IEstruturaMenuValidator Validation;
    protected readonly IUsuarioService UsuarioService;

    public EstruturaMenuApp(IEstruturaMenuService estruturaMenuService,IMapper mapper,IEstruturaMenuValidator validator, IUsuarioService usuarioService)
    {
        Service = estruturaMenuService;
        Mapper = mapper;
        Validation = validator;
        UsuarioService = usuarioService;
    }
    
    public ValidationResult IntegrarModulo(ModuloRequest request)
    {
        var validation = Validation.ValidaçãoIntegraçãoModulo(request);
        var lModulo = Service.GetAllModulo();

        if(validation.IsValid())
        {
            var modulo = Mapper.Map<ModuloRequest,Modulo>(request);

            if (request.Id.HasValue || request.Id > 0)
            {
                if (!lModulo.Any(x => x.IdModulo == request.Id))
                    validation.LErrors.Add("Id não identificado para edição do modulo!");
                else
                    Service.EditarModulo(modulo);
            }
            else
                Service.CadastrarModulo(modulo);
        }

        return validation;
    }

    public ValidationResult IntegrarMenu(MenuRequest request)
    {
        var validation = Validation.ValidaçãoIntegraçãoMenu(request);
        var lMenu = Service.GetAllMenu();
        var lModulo = Service.GetAllModulo();
        
        if (!lModulo.Any(x => x.IdModulo == request.IdModulo))
            validation.LErrors.Add("IdModulo não identificado para integração do modulo ao menu!");
        
        if(validation.IsValid())
        {
            var menu = Mapper.Map<MenuRequest,Menu>(request);

            if (request.IdMenu.HasValue || request.IdMenu > 0)
            {
                if (!lMenu.Any(x => x.IdMenu == request.IdMenu))
                    validation.LErrors.Add("Nenhum menu encontrado com este Id");
                else
                    Service.EditarMenu(menu);
            }
            else
                Service.CadastrarMenu(menu);
        }

        return validation;    
    }

    public EstrututuraMenuResponse ConsultarEstruturaMenus(int idUsuario)
    {
        var lMenus = Service.GetWithInclude()
            .ProjectTo<ModuloResponse>(Mapper.ConfigurationProvider).ToList();
        var usuario = UsuarioService.GetById(idUsuario);
        
        var retorno = new EstrututuraMenuResponse()
        {
            lModulos = new List<ModuloResponse>()
        };

        if (usuario == null)
            return retorno;
        
        foreach (var item in lMenus)
        {
            if (!usuario.PerfilAdministrador)
                item.lMenus = item.lMenus.AsQueryable().Where(x => x.OnlyAdmin == false).ToList();

            if(item.lMenus.Any())
                retorno.lModulos.Add(item);
        }            
        
        
        return retorno;
    }
}