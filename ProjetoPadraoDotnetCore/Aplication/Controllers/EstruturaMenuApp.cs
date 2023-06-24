using Aplication.Interfaces;
using Aplication.Models.Request.ModuloMenu;
using Aplication.Models.Response.Menu;
using Aplication.Utils.Objeto;
using Aplication.Validators.EstruturaMenu;
using AutoMapper;
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
    
    public ValidationResult IntegrarSubModulo(SubModuloRequest request)
    {
        var validation = Validation.ValidaçãoIntegraçãoSubModulo(request);
        var lSubModulo = Service.GetAllSubModulo();

        if(validation.IsValid())
        {
            var subModulo = Mapper.Map<SubModuloRequest,SubModulo>(request);

            if (request.IdSubModulo.HasValue || request.IdSubModulo > 0)
            {
                if (!lSubModulo.Any(x => x.IdSubModulo == request.IdSubModulo))
                    validation.LErrors.Add("Id não identificado para edição do SubModulo!");
                else
                    Service.EditarSubModulo(subModulo);
            }
            else
                Service.CadastrarSubModulo(subModulo);
        }

        return validation;
    }

    public ValidationResult IntegrarMenu(MenuRequest request)
    {
        var validation = Validation.ValidaçãoIntegraçãoMenu(request);
        var lMenu = Service.GetAllMenu();
        var lSubModulo = Service.GetAllSubModulo();
        
        if (!lSubModulo.Any(x => x.IdSubModulo == request.IdSubModulo))
            validation.LErrors.Add("IdSubModulo não identificado para integração do SubModulo ao menu!");
        
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
        var estruturaInicial = Service.GetModuloWithInclude().ToList();
        var usuario = UsuarioService.GetById(idUsuario);

        if (usuario == null)
            throw new Exception("Usuário não encontrado!");
        
        var menuResponse = new EstrututuraMenuResponse
        {
            Menu = new List<ItemMenu>()
        };

        var count = 0;
        foreach (var menu in estruturaInicial)
        {
            var title = new ItemMenu()
            {
                Id = count++,
                Label = menu.Nome,
                IsTitle = true
            };
            
            menuResponse.Menu.Add(title);

            foreach (var modulo in menu.LSubModulo)
            {
                if (modulo.LMenus != null)
                {
                    var moduloResponse = new ItemMenu()
                    {
                        Id = count++,
                        Label = modulo.Nome,
                        Icon = modulo.Icone,
                        Collapseid = modulo.Nome + "collapse",
                        SubItems = new List<ItemMenu>()
                    };

                    if (usuario.PerfilAdministrador == false)
                    {
                        moduloResponse.SubItems = modulo.LMenus
                            .Where(x => x.OnlyAdmin == false)
                            .Select(x => new ItemMenu()
                            {
                                Id = count++,
                                Label = x.Nome,
                                Link = x.Link,
                                ParentId = count--
                            }).ToList();
                    }
                    else
                    {
                        moduloResponse.SubItems = modulo.LMenus
                            .Select(x => new ItemMenu()
                            {
                                Id = count++,
                                Label = x.Nome,
                                Link = x.Link,
                                ParentId = count--
                            }).ToList();
                    }

                    if (moduloResponse.SubItems == null || !moduloResponse.SubItems.Any())
                    {
                        count--;
                        continue;
                    }
                    
                    count++;
                    menuResponse.Menu.Add(moduloResponse);
                }
            }

            if (menuResponse.Menu.Count > 0)
            {
                var last = menuResponse.Menu.LastOrDefault();
                
                if (last != null && last.IsTitle)
                {
                    menuResponse.Menu.Remove(last);
                }
            }
        }

        if (menuResponse.Menu.Count == 0)
            throw new Exception("você não possui nenhum menu liberado!");

        return menuResponse;
    }

    public AutoCompleteMenuResponse ConsultarAutoCompleteMenu(int idUsuario)
    {
        var menus = ConsultarEstruturaMenus(idUsuario).Menu;
        var retorno = new AutoCompleteMenuResponse()
        {
            Pages = new List<PageMenu>()
        };

        foreach (var item in menus)
        {
            if (item.SubItems != null)
            {
                foreach (var menuItem in item.SubItems)
                {
                    retorno.Pages.Add(new PageMenu()
                    {
                        Nome = menuItem.Label,
                        Url = menuItem.Link
                    });    
                }
            }
        }

        return retorno;
    }

    public ValidationResult IntegrarModulo(ModuloRequest request)
    {
        var validation = Validation.ValidaçãoIntegraçãoModulo(request);
        var lSubModulo = Service.GetAllModulo();

        if(validation.IsValid())
        {
            var modulo = Mapper.Map<ModuloRequest,Modulo>(request);

            if (request.IdModulo.HasValue || request.IdModulo > 0)
            {
                if (!lSubModulo.Any(x => x.IdModulo == request.IdModulo))
                    validation.LErrors.Add("Id não identificado para edição do Modulo!");
                else
                    Service.EditarModulo(modulo);
            }
            else
                Service.CadastrarModulo(modulo);
        }

        return validation;
    }

    public List<Menu> ConsultarMenus()
    {
        return Service.GetAllMenu().ToList();
    }

    public List<Modulo> ConsultarModulo()
    {
        return Service.GetModuloWithInclude().ToList();
    }
    
    public List<SubModulo> ConsultarSubModulo()
    {
        return Service.GetWithInclude().ToList();
    }
}