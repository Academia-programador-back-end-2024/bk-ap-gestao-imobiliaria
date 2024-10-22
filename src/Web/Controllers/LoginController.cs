using Academia.Programador.Bk.Gestao.Imobiliaria.Dominio.ModuloLogin;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Academia.Programador.Bk.Gestao.Imobiliaria.Web.Controllers
{
    public class LoginController : Controller
    {
        private readonly ILoginService _loginService;

        public LoginController()
        {
            _loginService = new LoginService();
        }

        public IActionResult Login(string returnUrl = "")
        {
            // Se já estiver autenticado redirecionar para controlar inicial
            if (User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Index", $"Clientes");
            }

            return View();
        }

        [HttpPost]
        public IActionResult Login(string email, string senha, string returnUrl = "")
        {
            if (ModelState.IsValid)
            {
                try
                {
                    Usuario user = _loginService.Autenticar(email, senha);

                    var claims = new List<Claim>
                    {
                        new Claim(ClaimTypes.Name, user.Email),
                        new Claim("FullName", user.Nome),
                        new Claim(ClaimTypes.Role, user.Perfil.Nome),
                    };

                    var claimsIdentity = new ClaimsIdentity(
                        claims, CookieAuthenticationDefaults.AuthenticationScheme);

                    var authProperties = new AuthenticationProperties
                    {
                        //AllowRefresh = <bool>,
                        // Refreshing the authentication session should be allowed.

                        //ExpiresUtc = DateTimeOffset.UtcNow.AddMinutes(10),
                        // The time at which the authentication ticket expires. A 
                        // value set here overrides the ExpireTimeSpan option of 
                        // CookieAuthenticationOptions set with AddCookie.

                        //IsPersistent = true,
                        // Whether the authentication session is persisted across 
                        // multiple requests. When used with cookies, controls
                        // whether the cookie's lifetime is absolute (matching the
                        // lifetime of the authentication ticket) or session-based.

                        //IssuedUtc = <DateTimeOffset>,
                        // The time at which the authentication ticket was issued.

                        //RedirectUri = <string>
                        // The full path or absolute URI to be used as an http 
                        // redirect response value.
                    };

                    HttpContext.SignInAsync(
                        CookieAuthenticationDefaults.AuthenticationScheme,
                        new ClaimsPrincipal(claimsIdentity),
                        authProperties).Wait();

                    //_logger.LogInformation("User {Email} logged in at {Time}.",
                    //    user.Email, DateTime.UtcNow);

                }
                catch (Exception e)
                {
                    ModelState.AddModelError(string.Empty, e.Message);

                    return View();
                }


                return RedirectToAction("Index", $"Clientes");
            }
            return View();
        }

        public IActionResult Logout()
        {
            HttpContext.SignOutAsync(
                CookieAuthenticationDefaults.AuthenticationScheme).Wait();

            return RedirectToAction("Index", $"Clientes");
        }
    }
}
