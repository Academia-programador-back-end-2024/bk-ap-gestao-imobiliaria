using Academia.Programador.Bk.Gestao.Imobiliaria.Dominio.ModuloCliente;

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

        public List<Cliente> TragaTodosClientes()
        {
            return _context.Clientes.ToList();
        }

        public void SalvarCliente(Cliente cliente)
        {
            _context.Update(cliente);
            _context.SaveChanges();
        }

        public Cliente TragaClientePorId(int? id)
        {
            return _context.Clientes.FirstOrDefault(cliente => cliente.ClienteId == id);
        }

        public void Remover(int id)
        {
            var cliente = TragaClientePorId(id);
            _context.Clientes.Remove(cliente);
            _context.SaveChanges();
        }

        public bool ClientePorCpf(string clienteCpf, int clienteClienteId)
        {
            return _context.Clientes.FirstOrDefault(cliente => string.Compare(cliente.Cpf, clienteCpf) == 0 && cliente.ClienteId != clienteClienteId) != null;
        }

        public bool ClientePorEmail(string? clienteEmail, int clienteClienteId)
        {
            return _context.Clientes.FirstOrDefault(cliente => string.Compare(cliente.Email, clienteEmail) == 0 && cliente.ClienteId != clienteClienteId) != null;
        }
    }
}
