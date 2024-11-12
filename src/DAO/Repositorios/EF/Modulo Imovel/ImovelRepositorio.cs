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

        public Imovel TragaImovelPorId(int? id)
        {
            return _dbContext.Imoveis
                .Include(i => i.ClienteDono)
                .Include(i => i.CorretorGestor)
                .Include(i => i.CorretorNegocio)
                .FirstOrDefault(m => m.ImovelId == id);
        }

        public void Criar(Imovel model)
        {
            _dbContext.Add(model);
            _dbContext.SaveChanges();
        }

        public List<Imovel> TragaTodos()
        {
            var imobiliariaDbContext =
                _dbContext.Imoveis.
                    Include(i => i.ClienteDono) // Carrega o cliente dono
                    .Include(i => i.CorretorGestor) // Carrega o corretor gestor
                    .Include(i => i.CorretorNegocio); // Carrega o corretor de negócio
            ;

            return imobiliariaDbContext.ToList();
        }

        public void Salvar(Imovel model)
        {
            _dbContext.Update(model);
            _dbContext.SaveChanges();
        }

        public Imovel TragaPorId(int id)
        {
            var imobiliariaDbContext =
                _dbContext.Imoveis
                    .Where(x => x.ImovelId == id)
                    .Include(i => i.ClienteDono) // Carrega o cliente dono
                    .Include(i => i.CorretorGestor) // Carrega o corretor gestor
                    .Include(i => i.CorretorNegocio); // Carrega o corretor de negócio


            return imobiliariaDbContext.FirstOrDefault();
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

        public List<Imovel> TragaOsMaisRecentes(int NumeroDeImoveis)
        {
            // Consulta para trazer somente imóveis disponíveis, ordenados por ID de forma decrescente
            var imobiliariaDbContext = _dbContext.Imoveis
                .Where(imovel => imovel.Disponivel) // Filtra apenas imóveis disponíveis
                .OrderByDescending(imovel => imovel.ImovelId) // Ordena pelos mais recentes
                .Take(NumeroDeImoveis) // Limita a quantidade pelo parâmetro fornecido
                .Include(i => i.ClienteDono) // Carrega o cliente dono
                .Include(i => i.CorretorGestor) // Carrega o corretor gestor
                .Include(i => i.CorretorNegocio); // Carrega o corretor de negócio

            return imobiliariaDbContext.ToList(); // Retorna a lista
        }

    }
}
