using Aplication.Models.Request;
using AutoMapper;
using Infraestrutura.Entity;

namespace Aplication.AutoMapper;

public class ViewToDomain : Profile
{
    public ViewToDomain()
    {
        #region Usuario
        CreateMap<UsuarioRequest, Usuario>();
        #endregion
    }
}