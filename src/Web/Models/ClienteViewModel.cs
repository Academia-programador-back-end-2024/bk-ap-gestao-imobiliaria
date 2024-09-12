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
}
