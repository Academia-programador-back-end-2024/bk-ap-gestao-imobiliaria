using Academia.Programador.Bk.Gestao.Imobiliaria.Dominio.ModuloConvidado;
using Academia.Programador.Bk.Gestao.Imobiliaria.Web.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Academia.Programador.Bk.Gestao.Imobiliaria.Web.Controllers
{
    [AllowAnonymous]
    public class ConvidadoController : Controller
    {
        private readonly IServiceConvidado _serviceConvidado;

        public ConvidadoController(IServiceConvidado serviceConvidado)
        {
            _serviceConvidado = serviceConvidado;
        }

        public IActionResult Index()
        {
            return View(_serviceConvidado.TragaUltimosSeis().ToImoveisViewModel());
        }

        [HttpPost]
        public IActionResult Contato(string email, string telefone, string nome)
        {
            //TODO: Ação para armazenar contato para rotina enviar email ao corretor de plantão
            _serviceConvidado.CreateContact(email, telefone, nome);

            return RedirectToAction("ContatoConfirmado", new { nome });
        }

        public IActionResult ContatoConfirmado(string nome)
        {
            return View(nome);
        }
    }
}
