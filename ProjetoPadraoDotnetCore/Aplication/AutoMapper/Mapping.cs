using Aplication.Models.Request.ModuloMenu;
using Aplication.Models.Request.Profissao;
using Aplication.Models.Request.Usuario;
using Aplication.Models.Response.Base;
using Aplication.Models.Response.Menu;
using Aplication.Models.Response.Usuario;
using Aplication.Utils.HashCripytograph;
using AutoMapper;
using Domain.DTO.Correios;
using Infraestrutura.Entity;
using Infraestrutura.Reports.Usuario.Obj;

namespace Aplication.AutoMapper;

public class Mapping : Profile
{
    public Mapping()
    {
        #region Usuario

        CreateMap<SkillRequest, SkillUsuario>()
            .ForMember(dst => dst.Descricao,
                map => map.MapFrom(src => src.Descricao))
            .ReverseMap();

        CreateMap<UsuarioRequest, Usuario>()
            .ForMember(dst => dst.LSkillUsuarios,
                map => map.MapFrom(src => src.lSkills));
        CreateMap<Usuario, UsuarioCrudResponse>()
            .ForMember(dst => dst.DataNascimento,
                map => map.MapFrom(src => src.DataNascimento!.Value.ToString("MM/dd/yyyy")))
            .ForMember(dst => dst.lSkills,
                map => map.MapFrom(src => src.LSkillUsuarios))
            .ForMember(dst => dst.Genero,
                map => map.MapFrom(src => src.Genero.GetHashCode().ToString()));

        CreateMap<UsuarioRegistroInicialRequest, Usuario>()
            .ForMember(dst => dst.Senha,
                map => map.MapFrom(src => new HashCripytograph().Hash(src.Senha)));

        CreateMap<Usuario, UsuarioGridReportObj>();
        #endregion

        #region ModuloMenu

        CreateMap<ModuloRequest, Modulo>()
            .ForMember(dst => dst.IdModulo,
                map => map.MapFrom(src => src.Id));

        CreateMap<MenuRequest, Menu>();

        CreateMap<Modulo, ModuloResponse>()
            .ForMember(dst => dst.lMenus,
                map => map.MapFrom(src => src.lMenus));

        CreateMap<Menu, LMenu>();

        #endregion

        #region Utils

        CreateMap<EnderecoExternalReponse, EnderecoResponse>()
            .ForMember(dst => dst.Bairro,
                map => map.MapFrom(src => src.bairro))
            .ForMember(dst => dst.Cidade,
                map => map.MapFrom(src => src.localidade))
            .ForMember(dst => dst.Estado,
                map => map.MapFrom(src => src.uf))
            .ForMember(dst => dst.Rua,
                map => map.MapFrom(src => src.logradouro));

        CreateMap<ProfissaoCadastrarRequest, Profissao>();

        CreateMap<ProfissaoEditarRequest, Profissao>();

        CreateMap<Profissao, SelectBaseResponse>()
            .ForMember(dst => dst.Description,
                map => map.MapFrom(src => src.Descricao))
            .ForMember(dst => dst.Value,
                map => map.MapFrom(src => src.IdProfissao));

        #endregion
    }
}