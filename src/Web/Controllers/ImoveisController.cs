using Academia.Programador.Bk.Gestao.Imobiliaria.Dominio.ModuloCliente;
using Academia.Programador.Bk.Gestao.Imobiliaria.Dominio.ModuloCorretor;
using Academia.Programador.Bk.Gestao.Imobiliaria.Dominio.ModuloImovel;
using Academia.Programador.Bk.Gestao.Imobiliaria.Web.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;

namespace Academia.Programador.Bk.Gestao.Imobiliaria.Web.Controllers
{
    public class ImoveisController : BaseController
    {
        private readonly IServiceImovel _serviceImovel;
        private readonly IServiceCliente _serviceCliente;
        private readonly IServiceCorretor _serviceCorretor;

        public ImoveisController(
            IServiceImovel serviceImovel,
            IServiceCliente serviceCliente,
            IServiceCorretor serviceCorretor)
        {
            _serviceImovel = serviceImovel;
            _serviceCliente = serviceCliente;
            _serviceCorretor = serviceCorretor;
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

            return View(imovel.ToDetailImovelViewModel());
        }

        // GET: Imovels/Create
        public IActionResult Create()
        {
            var clientes = _serviceCliente.TragaTodosClientes();
            var corretores = _serviceCorretor.TragaTodosCorretores();
            ViewData["ClienteDonoId"] = new SelectList(clientes, "ClienteId", "Cpf");
            ViewData["CorretorGestorId"] = new SelectList(corretores, "CorretorId", "Cpf");
            ViewData["CorretorNegocioId"] = new SelectList(corretores, "CorretorId", "Cpf");
            return View();
        }

        // POST: Imovels/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CreateImovelViewModel imovel)
        {
            if (ModelState.IsValid)
            {
                var imovelVo = imovel.ToImovel();

                var imovelId = _serviceImovel.CriarImovel(imovelVo);


                var fotosUrls = new List<string>();

                if (imovel.Fotos != null) //&& ArquivosFotos.Any())
                {
                    foreach (var foto in imovel.Fotos)
                    {
                        // Salvar o arquivo em um diretório no servidor ou na nuvem
                        var fileVirtualPath = $"fotos/{imovelId}/";
                        var directoryImovel = Path.Combine("wwwroot", fileVirtualPath);
                        if (Directory.Exists(directoryImovel) is false)
                        {
                            Directory.CreateDirectory(directoryImovel);
                        }

                        fileVirtualPath += "/" + foto.FileName;
                        var filePath = Path.Combine(directoryImovel, foto.FileName);

                        using (var stream = new FileStream(filePath, FileMode.Create))
                        {
                            await foto.CopyToAsync(stream);
                        }

                        // Adicionar o caminho salvo à lista de URLs
                        fotosUrls.Add("/" + fileVirtualPath); // Aqui você pode usar URLs reais
                    }
                }

                imovelVo = _serviceImovel.TragaImovelPorId(imovelId);

                imovelVo.Fotos = JsonConvert.SerializeObject(fotosUrls);

                _serviceImovel.SalvarImovel(imovelVo);


                return RedirectToAction(nameof(Index));

            }

            var clientes = _serviceCliente.TragaTodosClientes();
            var corretores = _serviceCorretor.TragaTodosCorretores();
            ViewData["ClienteDonoId"] = new SelectList(clientes, "ClienteId", "Cpf");
            ViewData["CorretorGestorId"] = new SelectList(corretores, "CorretorId", "Cpf");
            ViewData["CorretorNegocioId"] = new SelectList(corretores, "CorretorId", "Cpf");

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
