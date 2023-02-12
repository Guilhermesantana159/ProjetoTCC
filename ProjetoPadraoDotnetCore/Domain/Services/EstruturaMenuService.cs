using Domain.Interfaces;
using Infraestrutura.Entity;
using Infraestrutura.Repository.Interface.Menu;
using Infraestrutura.Repository.Interface.Modulo;

namespace Domain.Services;

public class EstruturaMenuService : IEstruturaMenuService
{
    protected readonly IModuloReadRepository ModuloReadRepository;
    protected readonly IModuloWriteRepository ModuloWriteRepository;
    protected readonly IMenuReadRepository MenuReadRepository;
    protected readonly IMenuWriteRepository MenuWriteRepository;

    public EstruturaMenuService(IModuloReadRepository moduloReadRepository,IModuloWriteRepository moduloWriteRepository
    ,IMenuReadRepository menuReadRepository,IMenuWriteRepository menuWriteRepository)
    {
        ModuloReadRepository = moduloReadRepository;
        ModuloWriteRepository = moduloWriteRepository;
        MenuReadRepository = menuReadRepository;
        MenuWriteRepository = menuWriteRepository;
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
    
    public void CadastrarMenu(Menu modulo)
    {
        MenuWriteRepository.Add(modulo);
    }
    
    public void EditarMenu(Menu modulo)
    {
        MenuWriteRepository.Update(modulo);
    }
    
    public IQueryable<Menu> GetAllMenu()
    {
        return MenuReadRepository.GetAll();
    }
    
    public IQueryable<Modulo> GetWithInclude()
    {
        return ModuloReadRepository.GetWithInclude();
    }
}