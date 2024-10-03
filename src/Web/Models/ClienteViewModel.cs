using Academia.Programador.Bk.Gestao.Imobiliaria.Dominio.ModuloCliente;
using System.ComponentModel.DataAnnotations;

namespace Academia.Programador.Bk.Gestao.Imobiliaria.Web.Models
{
    public class CreateClienteViewModel
    {
        public string Nome { get; set; } = null!;

        [Required]
        [MinLength(11, ErrorMessage = "Minimo 11")]
        [MaxLength(11, ErrorMessage = "Máximo 11")]
        public string Cpf { get; set; } = null!;

        public string? Telefone { get; set; }

        [Required]
        [EmailAddress(ErrorMessage = "Email invalido")]
        public string? Email { get; set; }
    }

    public class ClienteViewModel
    {
        public int ClienteId { get; set; }

        public string Nome { get; set; } = null!;

        public string Cpf { get; set; } = null!;

        public string? Telefone { get; set; }

        public string? Email { get; set; }
    }

    public static class CLienteViewModelExtensions
    {
        public static Cliente ToClienteVo(this CreateClienteViewModel cliente)
        {
            //Mapeamento
            return new Cliente
            {
                Cpf = cliente.Cpf,
                Email = cliente.Email,
                Nome = cliente.Nome,
                Telefone = cliente.Telefone
            };
        }
        public static Cliente ToClienteVo(this ClienteViewModel cliente)
        {
            //Mapeamento
            return new Cliente
            {
                ClienteId = cliente.ClienteId,
                Cpf = cliente.Cpf,
                Email = cliente.Email,
                Nome = cliente.Nome,
                Telefone = cliente.Telefone
            };
        }

        public static CreateClienteViewModel ToCreateClienteViewModel(this Cliente cliente)
        {
            //Mapeamento
            return new CreateClienteViewModel
            {
                Cpf = cliente.Cpf,
                Email = cliente.Email,
                Nome = cliente.Nome,
                Telefone = cliente.Telefone
            };
        }

        public static ClienteViewModel ToClienteViewModel(this Cliente cliente)
        {
            //Mapeamento -  AutoMapper
            return new ClienteViewModel
            {
                ClienteId = cliente.ClienteId,
                Cpf = cliente.Cpf,
                Email = cliente.Email,
                Nome = cliente.Nome,
                Telefone = cliente.Telefone
            };
        }

        public static List<ClienteViewModel> ToClientesViewModel(this List<Cliente> clientes)
        {
            var clienteViewModels = new List<ClienteViewModel>();
            clientes.ForEach(cliente => clienteViewModels.Add(cliente.ToClienteViewModel()));
            return clienteViewModels;
        }

    }
}
