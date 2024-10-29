using Microsoft.AspNetCore.Identity;
using System.Security.Authentication;

namespace Academia.Programador.Bk.Gestao.Imobiliaria.Dominio.ModuloLogin;

public interface ILoginService
{
    Usuario Autenticar(string email, string senha);

}

public class LoginService : ILoginService
{
    private readonly IPasswordHasher<Usuario> _passwordHasher;
    public LoginService()
    {
        _passwordHasher = new PasswordHasher<Usuario>();
    }
    public Usuario Autenticar(string email, string senha)
    {
        var usuarioTemporario = new Usuario();
        //var hashDaSenha = _passwordHasher.HashPassword(usuarioTemporario, senha);
        Usuario usuario = null;

        //TODO: Buscar usuario no banco
        usuario = new()
        {
            Email = "john@wick.com",
            Nome = "John",
            SenhaHash = "123123",
            Perfil = new Perfil() { Nome = "Administrador" }
        };

        if (usuario != null && string.Equals(senha, usuario.SenhaHash))
        {
            return usuario;
        }

        throw new AuthenticationException("Dados incorretos");
    }
}