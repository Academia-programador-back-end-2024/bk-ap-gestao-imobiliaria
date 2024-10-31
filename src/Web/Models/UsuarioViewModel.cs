using Academia.Programador.Bk.Gestao.Imobiliaria.Dominio.ModuloUsuario;
using System.ComponentModel.DataAnnotations;

namespace Academia.Programador.Bk.Gestao.Imobiliaria.Web.Models
{
    public class CreateUsuarioViewModel
    {
        [Required]
        public string Nome { get; set; } = null!;

        [Required]
        [EmailAddress(ErrorMessage = "Email inválido")]
        public string Email { get; set; } = null!;

        [Required]
        [MinLength(6, ErrorMessage = "A senha deve ter no mínimo 6 caracteres.")]
        public string Senha { get; set; } = null!;

        public int? ClienteId { get; set; }

        public int? CorretorId { get; set; }
        public int? PerfilId { get; set; }
    }

    public class UsuarioViewModel
    {
        public int UsuarioId { get; set; }

        public string Nome { get; set; } = null!;

        public string Email { get; set; } = null!;

        public int PerfilId { get; set; }

        public int? ClienteId { get; set; }

        public int? CorretorId { get; set; }

        public DateTime DataCriacao { get; set; }
    }

    public static class UsuarioViewModelExtensions
    {
        public static Usuario ToUsuario(this CreateUsuarioViewModel usuarioViewModel, string senhaHash)
        {
            // Mapeamento para a entidade Usuario
            return new Usuario
            {
                Nome = usuarioViewModel.Nome,
                Email = usuarioViewModel.Email,
                SenhaHash = senhaHash,
                ClienteId = usuarioViewModel.ClienteId.Value,
                CorretorId = usuarioViewModel.CorretorId.Value,
                PerfilId = usuarioViewModel.ClienteId.HasValue ? 1 : (usuarioViewModel.CorretorId.HasValue ? 2 : 3) // 1: Cliente, 2: Corretor, 3: Administrador
            };
        }

        public static Usuario ToUsuario(this UsuarioViewModel usuarioViewModel)
        {
            // Mapeamento para a entidade Usuario
            return new Usuario
            {
                UsuarioId = usuarioViewModel.UsuarioId,
                Nome = usuarioViewModel.Nome,
                Email = usuarioViewModel.Email,
                PerfilId = usuarioViewModel.PerfilId,
                ClienteId = usuarioViewModel.ClienteId.Value,
                CorretorId = usuarioViewModel.CorretorId.Value,
                DataCriacao = usuarioViewModel.DataCriacao
            };
        }

        public static UsuarioViewModel ToUsuarioViewModel(this Usuario usuario)
        {
            // Mapeamento de Usuario para UsuarioViewModel
            return new UsuarioViewModel
            {
                UsuarioId = usuario.UsuarioId,
                Nome = usuario.Nome,
                Email = usuario.Email,
                PerfilId = usuario.PerfilId,
                ClienteId = usuario.ClienteId,
                CorretorId = usuario.CorretorId,
                DataCriacao = usuario.DataCriacao
            };
        }

        public static List<UsuarioViewModel> ToUsuariosViewModel(this List<Usuario> usuarios)
        {
            var usuarioViewModels = new List<UsuarioViewModel>();
            usuarios.ForEach(usuario => usuarioViewModels.Add(usuario.ToUsuarioViewModel()));
            return usuarioViewModels;
        }
    }
}
