using Academia.Programador.Bk.Gestao.Imobiliaria.Dominio.ModuloImovel;
using Microsoft.EntityFrameworkCore;

namespace Academia.Programador.Bk.Gestao.Imobiliaria.DAO.Repositorios.EF.Modulo_Imovel
{
    public class ImovelRepositorio : IImovelRepositorio
    {
        private readonly ImobiliariaDbContext _dbContext;

        public ImovelRepositorio(ImobiliariaDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public void CriarImovel(Imovel imovel)
        {
            throw new NotImplementedException();
        }

        public List<Imovel> TragaTodosImoveles()
        {
            var imobiliariaDbContext =
                _dbContext.Imoveis;


            //imobiliariaDbContext.Include(i => i.ClienteDono)
            //    .Include(i => i.CorretorGestor)
            //    .Include(i => i.CorretorNegocio);

            return imobiliariaDbContext.ToList();
        }

        public void SalvarImovel(Imovel imovel)
        {
            throw new NotImplementedException();
        }

        public Imovel TragaImovelPorId(int? id)
        {
            return _dbContext.Imoveis
                .Include(i => i.ClienteDono)
                .Include(i => i.CorretorGestor)
                .Include(i => i.CorretorNegocio)
                .FirstOrDefault(m => m.ImovelId == id);
        }

        public void Remover(int id)
        {
            var imovel = TragaImovelPorId(id);
            if (imovel != null)
            {
                _dbContext.Imoveis.Remove(imovel);
            }

            _dbContext.SaveChanges();
        }
    }
}
