using Infraestrutura.Entity;

namespace Domain.Interfaces;

public interface IEstruturaMenuService
{
   public void CadastrarSubModulo(SubModulo subModulo);
   public void EditarSubModulo(SubModulo subModulo);
   public IQueryable<SubModulo> GetAllSubModulo();
   public void CadastrarMenu(Menu subModulo);
   public void EditarMenu(Menu subModulo);
   public IQueryable<Menu> GetAllMenu();
   public IQueryable<SubModulo> GetWithInclude();
   public void CadastrarModulo(Modulo subModulo);
   public void EditarModulo(Modulo subModulo);
   public IQueryable<Modulo> GetAllModulo();
   public IQueryable<Modulo> GetModuloWithInclude();
}