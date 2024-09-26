using Microsoft.Extensions.Logging;

namespace Academia.Programador.Bk.Gestao.Imobiliaria.Dominio.ModuloCliente;

public class ServiceCliente : IServiceCliente
{
    private readonly IClienteRepositorio _clienteRepositorio;
    private readonly ILogger<ServiceCliente> _logger;

    public ServiceCliente(IClienteRepositorio clienteRepositorio, ILogger<ServiceCliente> logger)
    {
        _clienteRepositorio = clienteRepositorio;
        _logger = logger;
    }

    public void CriarCliente(Cliente cliente)
    {
        _logger.LogInformation("Criando um cliente");
        //Validar duplicidade
        ValidacaoDuplicacao(cliente);

        _clienteRepositorio.CriarCliente(cliente);
    }

    private void ValidacaoDuplicacao(Cliente cliente)
    {
        var exiteClienteCpf = _clienteRepositorio.ClientePorCpf(cliente.Cpf, cliente.ClienteId);

        if (exiteClienteCpf is true)
        {
            _logger.LogWarning("Tentativa de duplicar cpf");
            throw new ClienteExistenteException();
        }

        var exiteClienteEmail = _clienteRepositorio.ClientePorEmail(cliente.Email, cliente.ClienteId);

        if (exiteClienteEmail is true)
        {
            throw new ClienteExistenteException();
        }
    }

    public List<Cliente> TragaTodosClientes()
    {
        return _clienteRepositorio.TragaTodosClientes();
    }

    public void SalvarCliente(Cliente cliente)
    {
        //Validar duplicidade
        ValidacaoDuplicacao(cliente);

        _clienteRepositorio.SalvarCliente(cliente);
    }

    public Cliente TragaClientePorId(int id)
    {
        return _clienteRepositorio.TragaClientePorId(id);
    }

    public void Remover(int id)
    {
        _clienteRepositorio.Remover(id);
    }
}