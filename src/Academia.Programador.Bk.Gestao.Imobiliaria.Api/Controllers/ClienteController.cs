using Academia.Programador.Bk.Gestao.Imobiliaria.Dominio.ModuloCliente;
using Microsoft.AspNetCore.Mvc;

namespace Academia.Programador.Bk.Gestao.Imobiliaria.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ClienteController : ControllerBase
{
    private readonly IServiceCliente _serviceCliente;

    public ClienteController(IServiceCliente serviceCliente)
    {
        _serviceCliente = serviceCliente;
    }


    [HttpGet]
    public IEnumerable<Cliente> Get()
    {
        var clientesVo = _serviceCliente.TragaTodosClientes();

        return clientesVo;
    }
}
