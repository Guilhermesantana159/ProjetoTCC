using Aplication.Models.Request.ModuloMenu;
using Aplication.Models.Request.Projeto;
using Aplication.Models.Request.Usuario;
using Aplication.Models.Response.Menu;
using Aplication.Models.Response.Usuario;
using Aplication.Utils.HashCripytograph;
using AutoMapper;
using Domain.DTO.Correios;
using Infraestrutura.Entity;
using Infraestrutura.Enum;
using Infraestrutura.Reports.Usuario.Objeto;

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

        #endregion

        #region Projeto

        CreateMap<ProjetoRequest, Projeto>()
            .ForMember(dst => dst.DataCadastro,
                map => map.MapFrom(src => DateTime.Now))
            .ForMember(dst => dst.Status,
                map => map.MapFrom(src => EStatusProjeto.Aberto));

        CreateMap<AtividadeRequest, Atividade>()
            .ForMember(dst => dst.Titulo,
                map => map.MapFrom(src => src.Atividade));

        CreateMap<TarefaRequest, Tarefa>();

        #endregion
    }
}