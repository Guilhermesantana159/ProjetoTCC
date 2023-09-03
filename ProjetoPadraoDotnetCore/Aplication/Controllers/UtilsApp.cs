using Aplication.Interfaces;
using Aplication.Models.Request.Utils;
using Aplication.Models.Response.Projeto;
using Aplication.Models.Response.Usuario;
using Aplication.Validators.Utils;
using AutoMapper;
using Domain.DTO.Correios;
using Domain.Interfaces;
using Infraestrutura.Entity;
using Infraestrutura.Repository.Interface.Feedback;

namespace Aplication.Controllers;

public class UtilsApp : IUtilsApp
{
    protected readonly IUtilsService UtilsService;
    protected readonly IUtilsValidator UtilsValidation;
    protected readonly IMapper Mapper;
    protected readonly IFeedbackReadRepository FeedbackReadRepository;

    public UtilsApp(IUtilsService utilsService,IUtilsValidator utilsValidation,IMapper mapper, IFeedbackReadRepository feedbackReadRepository)
    {
        UtilsService = utilsService;
        UtilsValidation = utilsValidation;
        Mapper = mapper;
        FeedbackReadRepository = feedbackReadRepository;
    }

    public EnderecoResponse ConsultarEnderecoCep(string cep)
    {
        var validation = UtilsValidation.ValidarCep(cep);

        if (!validation.IsValid())
            return Mapper.Map<EnderecoResponse>(validation);

        var retorno = UtilsService.ConsultarEnderecoCep(cep).Result;
        
        return Mapper.Map<EnderecoExternalReponse,EnderecoResponse>(retorno);
    }

    public FeedbackDataDashboard CadastrarFeedback(FeedbackRequest request)
    {
        UtilsService.SalvarFeedback(Mapper.Map<Feedback>(request));
        
        var feedback = FeedbackReadRepository.GetAll();

        return new FeedbackDataDashboard()
        {
            TotalFeedback = feedback.Count(),
            MediaFeedback = !feedback.Any() ? 0 : feedback.Sum(x => x.Rating) / feedback.Count(),
            Estrela1 = feedback.Count(x => x.Rating == 1),
            Estrela2 = feedback.Count(x => x.Rating == 2),
            Estrela3 = feedback.Count(x => x.Rating == 3),
            Estrela4 = feedback.Count(x => x.Rating == 4),
            Estrela5 = feedback.Count(x => x.Rating == 5)
        };
    }

    public void ContatoMensagem(ContatoMensagemRequest request)
    {
        UtilsService.ContatoMensagem(Mapper.Map<ContatoMensagem>(request));
    }

    public List<ContatoMensagem> ConsultarContatoMensagem()
    {
        return UtilsService.GetAllContatoMensagem();
    }
}