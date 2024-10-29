using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Academia.Programador.Bk.Gestao.Imobiliaria.Web.Controllers
{
    [Authorize]
    public class BaseController : Controller
    {
    }
}
