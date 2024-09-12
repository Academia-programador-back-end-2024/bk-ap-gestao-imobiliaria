using Academia.Programador.Bk.Gestao.Imobiliaria.Web;

namespace Academia.Programador.Bk.Gestao.Imobiliaria.Dominio.ModuloCliente
{
    public interface IServiceCliente
    {
        void CriarCliente(Cliente cliente);
    }

    public class ServiceCliente : IServiceCliente
    {
        private readonly IClienteRepositorio _clienteRepositorio;

        public ServiceCliente(IClienteRepositorio clienteRepositorio)
        {
            _clienteRepositorio = clienteRepositorio;
        }

        public void CriarCliente(Cliente cliente)
        {
            //Validar duplicidade

            //

            _clienteRepositorio.CriarCliente(cliente);
        }
    }
}
