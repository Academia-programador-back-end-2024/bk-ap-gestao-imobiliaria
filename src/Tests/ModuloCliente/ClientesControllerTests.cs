using Academia.Programador.Bk.Gestao.Imobiliaria.Dominio.ModuloCliente;
using Academia.Programador.Bk.Gestao.Imobiliaria.Web.Models;
using Academia.Programador.Bk.Gestao.Imobiliaria.Web.Views;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace Academia.Programador.Bk.Gestao.Imobiliaria.Tests.ModuloCliente;

public class ClientesControllerTests
{
    private readonly ClientesController _controller;
    private readonly Mock<IServiceCliente> _mockService;

    public ClientesControllerTests()
    {
        _mockService = new Mock<IServiceCliente>();
        _controller = new ClientesController(_mockService.Object);
    }

    [Fact]
    public async Task Dado_QueHaClientes_Quando_ObterTodosClientes_Entao_RetornaViewComClientesViewModels()
    {
        // Arrange
        var clientes = new List<Cliente>
        {
            new Cliente { ClienteId = 1, Nome = "Cliente 1" },
            new Cliente { ClienteId = 2, Nome = "Cliente 2" }
        };
        _mockService.Setup(s => s.TragaTodosClientes()).Returns(clientes);

        // Act
        var result = await _controller.Index() as ViewResult;

        // Assert
        Assert.NotNull(result);
        Assert.IsType<List<ClienteViewModel>>(result.Model);
        Assert.Equal(2, (result.Model as List<ClienteViewModel>).Count);
    }

    [Fact]
    public async Task Dado_QueIdEhNulo_Quando_ObterDetalhes_Entao_RetornaNotFound()
    {
        // Act
        var result = await _controller.Details(null);

        // Assert
        Assert.IsType<NotFoundResult>(result);
    }

    [Fact]
    public async Task Dado_QueIdEhValido_Quando_ObterDetalhes_Entao_RetornaViewComClienteViewModel()
    {
        // Arrange
        var cliente = new Cliente { ClienteId = 1, Nome = "Cliente 1" };
        _mockService.Setup(s => s.TragaClientePorId(1)).Returns(cliente);

        // Act
        var result = await _controller.Details(1) as ViewResult;

        // Assert
        Assert.NotNull(result);
        Assert.IsType<ClienteViewModel>(result.Model);
    }

    [Fact]
    public async Task Dado_QueIdEhInvalido_Quando_ObterDetalhes_Entao_RetornaNotFound()
    {
        // Arrange
        _mockService.Setup(s => s.TragaClientePorId(999)).Returns((Cliente)null);

        // Act
        var result = await _controller.Details(999);

        // Assert
        Assert.IsType<NotFoundResult>(result);
    }

    [Fact]
    public async Task Dado_ModeloValido_Quando_CriarCliente_Entao_RedirecionaParaIndex()
    {
        // Arrange
        var clienteViewModel = new CreateClienteViewModel { Nome = "Novo Cliente" };

        // Act
        var result = await _controller.Create(clienteViewModel) as RedirectToActionResult;

        // Assert
        Assert.NotNull(result);
        Assert.Equal("Index", result.ActionName);
    }

    [Fact]
    public async Task Dado_ModeloInvalido_Quando_CriarCliente_Entao_RetornaViewComModelo()
    {
        // Arrange
        _controller.ModelState.AddModelError("Error", "Modelo inválido");
        var clienteViewModel = new CreateClienteViewModel { Nome = "Novo Cliente" };

        // Act
        var result = await _controller.Create(clienteViewModel) as ViewResult;

        // Assert
        Assert.NotNull(result);
        Assert.Equal(clienteViewModel, result.Model);
    }

    [Fact]
    public async Task Dado_QueIdEhValido_Quando_ObterClienteParaEditar_Entao_RetornaViewComClienteViewModel()
    {
        // Arrange
        var clientes = new List<Cliente>
        {
            new Cliente { ClienteId = 1, Nome = "Cliente 1" },
            new Cliente { ClienteId = 2, Nome = "Cliente 2" }
        };
        _mockService.Setup(s => s.TragaTodosClientes()).Returns(clientes);

        // Act
        var result = await _controller.Edit(1) as ViewResult;

        // Assert
        Assert.NotNull(result);
        Assert.IsType<ClienteViewModel>(result.Model);
    }

    [Fact]
    public async Task Dado_QueIdEhInvalido_Quando_ObterClienteParaEditar_Entao_RetornaNotFound()
    {
        // Arrange
        _mockService.Setup(s => s.TragaTodosClientes()).Returns(new List<Cliente>());

        // Act
        var result = await _controller.Edit(999);

        // Assert
        Assert.IsType<NotFoundResult>(result);
    }

    [Fact]
    public async Task Dado_ModeloValido_Quando_EditarCliente_Entao_RedirecionaParaIndex()
    {
        // Arrange
        var clienteViewModel = new ClienteViewModel { ClienteId = 1, Nome = "Cliente Atualizado" };

        // Act
        var result = await _controller.Edit(1, clienteViewModel) as RedirectToActionResult;

        // Assert
        Assert.NotNull(result);
        Assert.Equal("Index", result.ActionName);
    }

    [Fact]
    public async Task Dado_ModeloInvalido_Quando_EditarCliente_Entao_RetornaViewComModelo()
    {
        // Arrange
        _controller.ModelState.AddModelError("Error", "Modelo inválido");
        var clienteViewModel = new ClienteViewModel { ClienteId = 1, Nome = "Cliente Atualizado" };

        // Act
        var result = await _controller.Edit(1, clienteViewModel) as ViewResult;

        // Assert
        Assert.NotNull(result);
        Assert.Equal(clienteViewModel, result.Model);
    }

    [Fact]
    public async Task Dado_QueIdEhValido_Quando_ObterClienteParaDeletar_Entao_RetornaViewComClienteViewModel()
    {
        // Arrange
        var cliente = new Cliente { ClienteId = 1, Nome = "Cliente 1" };
        _mockService.Setup(s => s.TragaClientePorId(1)).Returns(cliente);

        // Act
        var result = await _controller.Delete(1) as ViewResult;

        // Assert
        Assert.NotNull(result);
        Assert.IsType<ClienteViewModel>(result.Model);
    }

    [Fact]
    public async Task Dado_QueIdEhInvalido_Quando_ObterClienteParaDeletar_Entao_RetornaNotFound()
    {
        // Arrange
        _mockService.Setup(s => s.TragaClientePorId(999)).Returns((Cliente)null);

        // Act
        var result = await _controller.Delete(999);

        // Assert
        Assert.IsType<NotFoundResult>(result);
    }

    [Fact]
    public async Task Dado_QueIdEhValido_Quando_DeletarCliente_Entao_RedirecionaParaIndex()
    {
        // Act
        var result = await _controller.DeleteConfirmed(1) as RedirectToActionResult;

        // Assert
        Assert.NotNull(result);
        Assert.Equal("Index", result.ActionName);
    }

    [Fact]
    public async Task Dado_QueErroOcorrer_Quando_DeletarCliente_Entao_RetornaViewComErro()
    {
        // Arrange
        _mockService.Setup(s => s.Remover(1)).Throws(new Exception("Erro ao deletar"));
        var cliente = new Cliente { ClienteId = 1, Nome = "Cliente 1" };
        _mockService.Setup(s => s.TragaClientePorId(1)).Returns(cliente);

        // Act
        var result = await _controller.DeleteConfirmed(1) as ViewResult;

        // Assert
        Assert.NotNull(result);
        Assert.Equivalent(cliente.ToClienteViewModel(), result.Model);

        Assert.False(_controller.ModelState.IsValid);
        Assert.True(_controller.ModelState.ContainsKey(""));
        var modelStateEntry = _controller.ModelState[""];
        Assert.Contains($"Erro ao deletar", modelStateEntry.Errors.First().ErrorMessage);
    }
}
