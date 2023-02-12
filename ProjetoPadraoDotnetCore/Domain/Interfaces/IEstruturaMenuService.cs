using Infraestrutura.Entity;

namespace Domain.Interfaces;

public interface IEstruturaMenuService
{
   public void CadastrarModulo(Modulo modulo);
   public void EditarModulo(Modulo modulo);
   public IQueryable<Modulo> GetAllModulo();
   public void CadastrarMenu(Menu modulo);
   public void EditarMenu(Menu modulo);
   public IQueryable<Menu> GetAllMenu();
   public IQueryable<Modulo> GetWithInclude();
}