using Aplication.Controllers;
using Aplication.Models.Request.Usuario;
using Aplication.Validators.Usuario;
using AutoMoq;
using Bogus;
using Domain.Interfaces;
using Domain.Services;
using Infraestrutura.Entity;
using Moq;
using Xunit;
using ValidationResult = Aplication.Utils.Objeto.ValidationResult;

namespace Tests.UsuarioTeste;

public class UsuarioTeste
{
    private AutoMoqer _mock;
    
    public UsuarioTeste()
    {
        _mock = new AutoMoqer();
    }

    [Fact]
    public void UsuarioValidoCadastro()
    {
        // Arrange
        var user = new Faker<UsuarioRequest>()
            .CustomInstantiator(f => new UsuarioRequest()
            {
                IdUsuario = null,
                Nome = f.Name.FirstName(),
                Bairro = f.Locale,
                DataNascimento = f.Date.Recent(),
                Cep = f.Locale,
                Cidade = f.Locale,
                Cpf = f.Name.FullName(),
                Email = f.Locale,
                Estado = f.Address.City(),
                Foto = f.Phone.PhoneNumber(),
                Rua = f.Locale
            });
            
        _mock.Create<UsuarioApp>();
        _mock.GetMock<IUsuarioService>()
            .Setup(x => x.GetAllList())
            .Returns(new List<Usuario>());
        
        _mock.GetMock<IUsuarioValidator>()
            .Setup(x => x.ValidacaoCadastro(It.IsAny<UsuarioRequest>()))
            .Returns(new ValidationResult());
        
        var customerService = _mock.Resolve<UsuarioApp>();

        // Act
        var result = customerService.Cadastrar(user);

        //Assert
        Assert.True(result.IsValid());

    }
}