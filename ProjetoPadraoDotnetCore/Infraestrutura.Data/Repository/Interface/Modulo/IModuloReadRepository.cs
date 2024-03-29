﻿using Infraestrutura.Repository.Interface.Base;

namespace Infraestrutura.Repository.Interface.Modulo;

public interface IModuloReadRepository : IBaseReadRepository<Entity.Modulo>
{
    IQueryable<Entity.Modulo> GetWithInclude();
}