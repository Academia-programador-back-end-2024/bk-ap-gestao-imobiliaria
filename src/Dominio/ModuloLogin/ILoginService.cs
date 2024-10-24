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
    public List<Usuario> Usuarios { get; set; }
    public LoginService()
    {
        _passwordHasher = new PasswordHasher<Usuario>();
        Usuarios = new List<Usuario>();
        Usuarios.Add(new()
        {
            Email = "john@wick.com",
            Nome = "John",
            SenhaHash = "AQAAAAIAAYagAAAAEAcwH8ucYtATRdWjLP2Rz6CXgDRW7w6I2q15wZcuyWkPa2QwIEM43l6cCdfwx1edOw==",
            Perfil = new Perfil() { Nome = "Administrador" }
        });
        Usuarios.Add(new()
        {
            Email = "john2@wick.com",
            Nome = "John2",
            SenhaHash = "AQAAAAIAAYagAAAAEAcwH8ucYtATRdWjLP2Rz6CXgDRW7w6I2q15wZcuyWkPa2QwIEM43l6cCdfwx1edOw==",
            Perfil = new Perfil() { Nome = "Cliente" }
        });
        Usuarios.Add(new()
        {
            Email = "john3@wick.com",
            Nome = "John3",
            SenhaHash = "AQAAAAIAAYagAAAAEAcwH8ucYtATRdWjLP2Rz6CXgDRW7w6I2q15wZcuyWkPa2QwIEM43l6cCdfwx1edOw==",
            Perfil = new Perfil() { Nome = "Corretor" }
        });
    }
    public Usuario Autenticar(string email, string senha)
    {
        var usuarioTemporario = new Usuario();
        var hashDaSenha = _passwordHasher.HashPassword(usuarioTemporario, senha);
        Usuario usuario = Usuarios.Find(user => user.Email == email);

        //TODO: Buscar usuario no banco
        if (usuario != null)
        {
            if (_passwordHasher.VerifyHashedPassword(usuario, usuario.SenhaHash, senha) ==
                PasswordVerificationResult.Success)
            {
                return usuario;
            }
        }

        throw new AuthenticationException("Dados incorretos");
    }
}