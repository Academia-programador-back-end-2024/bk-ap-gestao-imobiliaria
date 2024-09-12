using Academia.Programador.Bk.Gestao.Imobiliaria.Dominio.ModuloCliente;
using Academia.Programador.Bk.Gestao.Imobiliaria.Web;

namespace Academia.Programador.Bk.Gestao.Imobiliaria.DAO.Repositorios.EF
{
    public class ClienteRepositorio : IClienteRepositorio
    {
        private readonly ImobiliariaDbContext _context;

        public ClienteRepositorio(ImobiliariaDbContext context)
        {
            _context = context;
        }

        public void CriarCliente(Cliente cliente)
        {
            _context.Clientes.Add(cliente);
            _context.SaveChanges();
        }
    }
}
