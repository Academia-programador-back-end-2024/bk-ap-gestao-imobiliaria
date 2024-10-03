using Academia.Programador.Bk.Gestao.Imobiliaria.Dominio.ModuloImovel;
using Academia.Programador.Bk.Gestao.Imobiliaria.Web.Models;
using Microsoft.AspNetCore.Mvc;

namespace Academia.Programador.Bk.Gestao.Imobiliaria.Web.Controllers
{
    public class ImoveisController : Controller
    {
        private readonly IServiceImovel _serviceImovel;

        public ImoveisController(IServiceImovel serviceImovel)
        {
            _serviceImovel = serviceImovel;
        }

        // GET: Imovels
        public IActionResult Index()
        {
            var imoveisVo = _serviceImovel.TragaTodosImoveis();

            return View(imoveisVo.ToImoveisViewModel());
        }

        // GET: Imovels/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var imovel = _serviceImovel.TragaImovelPorId(id.Value);
            if (imovel == null)
            {
                return NotFound();
            }

            return View(imovel);
        }

        // GET: Imovels/Create
        public IActionResult Create()
        {
            //ViewData["ClienteDonoId"] = new SelectList(_context.Clientes, "ClienteId", "Cpf");
            //ViewData["CorretorGestorId"] = new SelectList(_context.Corretores, "CorretorId", "Cpf");
            //ViewData["CorretorNegocioId"] = new SelectList(_context.Corretores, "CorretorId", "Cpf");
            return View();
        }

        // POST: Imovels/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ImovelId,Endereco,Tipo,Area,Valor,Descricao,Negocio,CorretorNegocioId,CorretorGestorId,ClienteDonoId,Disponivel,Fotos")] Imovel imovel)
        {
            //if (ModelState.IsValid)
            //{
            //    _context.Add(imovel);
            //    await _context.SaveChangesAsync();
            //    return RedirectToAction(nameof(Index));
            //}
            //ViewData["ClienteDonoId"] = new SelectList(_context.Clientes, "ClienteId", "Cpf", imovel.ClienteDonoId);
            //ViewData["CorretorGestorId"] = new SelectList(_context.Corretores, "CorretorId", "Cpf", imovel.CorretorGestorId);
            //ViewData["CorretorNegocioId"] = new SelectList(_context.Corretores, "CorretorId", "Cpf", imovel.CorretorNegocioId);
            //
            return View(imovel);
        }

        // GET: Imovels/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var imovel = _serviceImovel.TragaImovelPorId(id.Value);
            if (imovel == null)
            {
                return NotFound();
            }
            //ViewData["ClienteDonoId"] = new SelectList(_context.Clientes, "ClienteId", "Cpf", imovel.ClienteDonoId);
            //ViewData["CorretorGestorId"] = new SelectList(_context.Corretores, "CorretorId", "Cpf", imovel.CorretorGestorId);
            //ViewData["CorretorNegocioId"] = new SelectList(_context.Corretores, "CorretorId", "Cpf", imovel.CorretorNegocioId);

            return View(imovel);
        }

        // POST: Imovels/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ImovelId,Endereco,Tipo,Area,Valor,Descricao,Negocio,CorretorNegocioId,CorretorGestorId,ClienteDonoId,Disponivel,Fotos")] Imovel imovel)
        {
            if (id != imovel.ImovelId)
            {
                return NotFound();
            }

            //if (ModelState.IsValid)
            //{
            //    try
            //    {
            //        _context.Update(imovel);
            //        await _context.SaveChangesAsync();
            //    }
            //    catch (DbUpdateConcurrencyException)
            //    {
            //        if (!ImovelExists(imovel.ImovelId))
            //        {
            //            return NotFound();
            //        }
            //        else
            //        {
            //            throw;
            //        }
            //    }
            //    return RedirectToAction(nameof(Index));
            //}
            //ViewData["ClienteDonoId"] = new SelectList(_context.Clientes, "ClienteId", "Cpf", imovel.ClienteDonoId);
            //ViewData["CorretorGestorId"] = new SelectList(_context.Corretores, "CorretorId", "Cpf", imovel.CorretorGestorId);
            //ViewData["CorretorNegocioId"] = new SelectList(_context.Corretores, "CorretorId", "Cpf", imovel.CorretorNegocioId);
            //
            return View(imovel);
        }

        // GET: Imovels/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var imovel = _serviceImovel.TragaImovelPorId(id.Value);
            if (imovel == null)
            {
                return NotFound();
            }

            return View(imovel);
        }

        // POST: Imovels/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            _serviceImovel.Remover(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
