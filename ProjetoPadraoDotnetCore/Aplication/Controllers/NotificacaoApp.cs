using Aplication.Interfaces;
using Aplication.Models.Request.Notificao;
using Domain.Interfaces;
using Infraestrutura.Entity;
using Infraestrutura.Enum;

namespace Aplication.Controllers;
public class NotificacaoApp : INotificacaoApp
{
    protected readonly INotificacaoService Service;

    public NotificacaoApp(INotificacaoService service)
    {
        Service = service;
    }
    
    public List<Notificacao> GetNotificaçõesByUser(int id)
    {
        var listNotificacao = Service.GetAllQuery().Where(x => x.IdUsuario == id);
        var listNotificacaoAntigas = listNotificacao.Where(x =>
            x.Lido == ESimNao.Sim && x.DataVisualização!.Value.AddDays(10) > DateTime.Now);
        
        if (listNotificacaoAntigas.Any())
            Service.DeleteList(listNotificacaoAntigas.ToList());
        
        return listNotificacao.ToList();
    }
    
    public void NotificacaoLida(NotificacaolidaRequest request)
    {
        var entityNotificacao = Service.GetById(request.IdNotificaoLida);

        if (entityNotificacao != null)
        {
            //Atribuição de lida
            entityNotificacao.Lido = ESimNao.Sim;
            Service.Editar(entityNotificacao);
        }
    }
    
}

