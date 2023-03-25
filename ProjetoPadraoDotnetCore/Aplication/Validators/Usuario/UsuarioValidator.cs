using Aplication.Models.Request.Usuario;
using Aplication.Utils.Objeto;
using Aplication.Utils.ValidatorDocument;

namespace Aplication.Validators.Usuario;

public class UsuarioValidator : IUsuarioValidator
{
    protected readonly IValidatorDocument Util;
    public UsuarioValidator(IValidatorDocument utilDocument)
    {
        Util = utilDocument;
    }

    public ValidationResult ValidacaoCadastroInicial(UsuarioRegistroInicialRequest request)
    {
        var validation = new ValidationResult();
        
        if(string.IsNullOrEmpty(request.Email))
            validation.LErrors.Add("Campo email é obrigatório!");
        if(string.IsNullOrEmpty(request.Nome))
            validation.LErrors.Add("Campo nome é obrigatório!");
        if(string.IsNullOrEmpty(request.Senha))
            validation.LErrors.Add("Campo senha é obrigatório!");
        if(string.IsNullOrEmpty(request.CPF))
            validation.LErrors.Add("Campo CPF é obrigatório!");
        if(!Util.ValidatorCpf(request.CPF))
            validation.LErrors.Add("Campo CPF inválido!");
        
        return validation;    
    }
    
    public ValidationResult ValidacaoCadastro(UsuarioRequest request)
    {
        var validation = new ValidationResult();
        
        if(string.IsNullOrEmpty(request.Email))
            validation.LErrors.Add("Campo Email é obrigatório!");
        if(string.IsNullOrEmpty(request.Nome))
            validation.LErrors.Add("Campo nome é obrigatório!");
        if(string.IsNullOrEmpty(request.NomeMae))
            validation.LErrors.Add("Campo noma da mãe é obrigatório!");
        if(string.IsNullOrEmpty(request.Cpf))
            validation.LErrors.Add("Campo CPF é obrigatório!");
        if(!request.DataNascimento.HasValue)
            validation.LErrors.Add("Campo data de nascimento é obrigatório!");
        if(string.IsNullOrEmpty(request.Cep))
            validation.LErrors.Add("Campo cep obrigatório!");
        if(string.IsNullOrEmpty(request.Bairro))
            validation.LErrors.Add("Campo bairro obrigatório!");
        if(string.IsNullOrEmpty(request.Cidade))
            validation.LErrors.Add("Campo cidade obrigatório!");
        if(string.IsNullOrEmpty(request.Estado))
            validation.LErrors.Add("Campo estado obrigatório!");
        if(string.IsNullOrEmpty(request.Rua))
            validation.LErrors.Add("Campo rua obrigatório!");
        if(!Util.ValidatorCpf(request.Cpf ?? ""))
            validation.LErrors.Add("Campo CPF inválido!");

        return validation;    
    }
}