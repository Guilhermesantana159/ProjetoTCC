using Domain.Interfaces;
using Infraestrutura.Entity;
using Infraestrutura.Repository.Interface.Menu;
using Infraestrutura.Repository.Interface.Modulo;
using Infraestrutura.Repository.Interface.SubModulo;

namespace Domain.Services;

public class EstruturaMenuService : IEstruturaMenuService
{
    protected readonly ISubModuloReadRepository SubModuloReadRepository;
    protected readonly ISubModuloWriteRepository SubModuloWriteRepository;
    protected readonly IModuloReadRepository ModuloReadRepository;
    protected readonly IModuloWriteRepository ModuloWriteRepository;
    protected readonly IMenuReadRepository MenuReadRepository;
    protected readonly IMenuWriteRepository MenuWriteRepository;

    public EstruturaMenuService(ISubModuloReadRepository subModuloReadRepository,ISubModuloWriteRepository subModuloWriteRepository
    ,IMenuReadRepository menuReadRepository,IMenuWriteRepository menuWriteRepository, IModuloWriteRepository moduloWriteRepository, IModuloReadRepository moduloReadRepository)
    {
        SubModuloReadRepository = subModuloReadRepository;
        SubModuloWriteRepository = subModuloWriteRepository;
        MenuReadRepository = menuReadRepository;
        MenuWriteRepository = menuWriteRepository;
        ModuloWriteRepository = moduloWriteRepository;
        ModuloReadRepository = moduloReadRepository;
    }
    
    public void CadastrarSubModulo(SubModulo subModulo)
    {
        SubModuloWriteRepository.Add(subModulo);
    }
    
    public void EditarSubModulo(SubModulo subModulo)
    {
        SubModuloWriteRepository.Update(subModulo);
    }
    
    public IQueryable<SubModulo> GetAllSubModulo()
    {
        return SubModuloReadRepository.GetAll();
    }
    
    public void CadastrarMenu(Menu menu)
    {
        MenuWriteRepository.Add(menu);
    }
    
    public void EditarMenu(Menu subModulo)
    {
        MenuWriteRepository.Update(subModulo);
    }
    
    public IQueryable<Menu> GetAllMenu()
    {
        return MenuReadRepository.GetAll();
    }
    
    public IQueryable<SubModulo> GetWithInclude()
    {
        return SubModuloReadRepository.GetWithInclude();
    }
    
    public IQueryable<Modulo> GetModuloWithInclude()
    {
        return ModuloReadRepository.GetWithInclude();
    }
    
    public void CadastrarModulo(Modulo modulo)
    {
        ModuloWriteRepository.Add(modulo);
    }
    
    public void EditarModulo(Modulo modulo)
    {
        ModuloWriteRepository.Update(modulo);
    }
    
    public IQueryable<Modulo> GetAllModulo()
    {
        return ModuloReadRepository.GetAll();
    }
}