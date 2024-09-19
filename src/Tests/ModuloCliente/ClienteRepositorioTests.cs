using Academia.Programador.Bk.Gestao.Imobiliaria.DAO.Repositorios.EF;
using Academia.Programador.Bk.Gestao.Imobiliaria.Dominio.ModuloCliente;
using Microsoft.EntityFrameworkCore;

namespace Academia.Programador.Bk.Gestao.Imobiliaria.Tests.ModuloCliente
{
    public class ClienteRepositorioTests
    {
        private ImobiliariaDbContext CreateDbContext()
        {
            var options = new DbContextOptionsBuilder<ImobiliariaDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString()) // Garantir que cada teste tenha um banco único
                .Options;
            var context = new ImobiliariaDbContext(options);

            SeedDatabase(context);
            return context;
        }

        private void SeedDatabase(ImobiliariaDbContext _context)
        {
            var clientes = new List<Cliente>
        {
            new Cliente { ClienteId = 1, Cpf = "12345678900", Email = "cliente1@email.com", Nome = "Cliente 1" },
            new Cliente { ClienteId = 2, Cpf = "12345678901", Email = "cliente2@email.com", Nome = "Cliente 2" }
        };

            _context.Clientes.AddRange(clientes);
            _context.SaveChanges();
        }

        [Fact]
        public void Dado_QueClienteNaoExiste_Quando_CriarCliente_Entao_AdicionaAoContexto()
        {
            // Arrange
            var _context = CreateDbContext();
            var _clienteRepositorio = new ClienteRepositorio(_context);
            var novoCliente = new Cliente
            {
                ClienteId = 3,
                Cpf = "12345678902",
                Email = "cliente3@email.com",
                Nome = "Cliente 3"
            };

            // Act
            _clienteRepositorio.CriarCliente(novoCliente);

            // Assert
            var clienteNoDb = _context.Clientes.FirstOrDefault(c => c.ClienteId == 3);
            Assert.NotNull(clienteNoDb);
            Assert.Equal("Cliente 3", clienteNoDb.Nome);
        }

        [Fact]
        public void Dado_QueClienteExiste_Quando_TragaClientePorId_Entao_RetornaCliente()
        {
            //Arrange
            var _context = CreateDbContext();
            var _clienteRepositorio = new ClienteRepositorio(_context);

            // Act
            var cliente = _clienteRepositorio.TragaClientePorId(1);

            // Assert
            Assert.NotNull(cliente);
            Assert.Equal(1, cliente.ClienteId);
            Assert.Equal("Cliente 1", cliente.Nome);
        }

        [Fact]
        public void Dado_QueClienteNaoExiste_Quando_TragaClientePorId_Entao_RetornaNull()
        {
            //Arrange
            var _context = CreateDbContext();
            var _clienteRepositorio = new ClienteRepositorio(_context);


            // Act
            var cliente = _clienteRepositorio.TragaClientePorId(999); // ID inexistente

            // Assert
            Assert.Null(cliente);
        }

        [Fact]
        public void Dado_QueClienteExiste_Quando_SalvarCliente_Entao_AtualizaInformacoes()
        {
            //Arrange
            var _context = CreateDbContext();
            var _clienteRepositorio = new ClienteRepositorio(_context);

            var cliente = _context.Clientes.First(c => c.ClienteId == 1);
            cliente.Nome = "Cliente 1 Atualizado";

            // Act
            _clienteRepositorio.SalvarCliente(cliente);

            // Assert
            var clienteAtualizado = _context.Clientes.First(c => c.ClienteId == 1);
            Assert.Equal("Cliente 1 Atualizado", clienteAtualizado.Nome);
        }

        [Fact]
        public void Dado_QueClienteExiste_Quando_RemoverCliente_Entao_RemoveDoContexto()
        {
            //Arrange
            var _context = CreateDbContext();
            var _clienteRepositorio = new ClienteRepositorio(_context);
            var clienteId = 1;

            // Act
            _clienteRepositorio.Remover(clienteId);

            // Assert
            var clienteRemovido = _context.Clientes.FirstOrDefault(c => c.ClienteId == clienteId);
            Assert.Null(clienteRemovido);
        }

        [Fact]
        public void Dado_QueCpfJaExiste_Quando_ValidarCpf_Entao_RetornaTrue()
        {
            //Arrange
            var _context = CreateDbContext();
            var _clienteRepositorio = new ClienteRepositorio(_context);
            var clienteCpf = "12345678900"; // CPF do Cliente 1

            // Act
            var existe = _clienteRepositorio.ClientePorCpf(clienteCpf, 999); // Simula um ID diferente para verificação

            // Assert
            Assert.True(existe);
        }

        [Fact]
        public void Dado_QueCpfNaoExiste_Quando_ValidarCpf_Entao_RetornaFalse()
        {
            //Arrange
            var _context = CreateDbContext();
            var _clienteRepositorio = new ClienteRepositorio(_context);
            var clienteCpf = "12345678999"; // CPF inexistente

            // Act
            var existe = _clienteRepositorio.ClientePorCpf(clienteCpf, 999);

            // Assert
            Assert.False(existe);
        }

        [Fact]
        public void Dado_QueEmailJaExiste_Quando_ValidarEmail_Entao_RetornaTrue()
        {
            //Arrange
            var _context = CreateDbContext();
            var _clienteRepositorio = new ClienteRepositorio(_context);
            var clienteEmail = "cliente1@email.com"; // Email do Cliente 1

            // Act
            var existe = _clienteRepositorio.ClientePorEmail(clienteEmail, 999); // Simula um ID diferente para verificação

            // Assert
            Assert.True(existe);
        }

        [Fact]
        public void Dado_QueEmailNaoExiste_Quando_ValidarEmail_Entao_RetornaFalse()
        {
            //Arrange
            var _context = CreateDbContext();
            var _clienteRepositorio = new ClienteRepositorio(_context);
            var clienteEmail = "novoemail@email.com"; // Email inexistente

            // Act
            var existe = _clienteRepositorio.ClientePorEmail(clienteEmail, 999);

            // Assert
            Assert.False(existe);
        }
    }

}
