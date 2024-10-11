using Academia.Programador.Bk.Gestao.Imobiliaria.Dominio.ModuloImovel;
using Newtonsoft.Json;

namespace Academia.Programador.Bk.Gestao.Imobiliaria.Web.Models
{
    public enum TipoImovel
    {
        Casa = 0,
        Terreno = 1,
        Apartamento = 2
    }

    public enum TipoDeNegocio
    {
        Venda = 0,
        Aluguel = 1
    }

    public class DetailsImovelViewModel
    {
        public int ImovelId { get; set; }

        public string Endereco { get; set; } = null!;

        public TipoImovel Tipo { get; set; }

        public decimal Area { get; set; }

        public decimal Valor { get; set; }

        public string? Descricao { get; set; }

        public TipoDeNegocio Negocio { get; set; }

        public string? NomeCorretorNegocio { get; set; }

        public string NomeCorretorGestor { get; set; }

        public string NomeCliente { get; set; }

        public bool Disponivel { get; set; }

        public List<string> Fotos { get; set; }
    }

    public class CreateImovelViewModel
    {
        public string Endereco { get; set; } = null!;

        public TipoImovel Tipo { get; set; }

        public decimal Area { get; set; }

        public decimal Valor { get; set; }

        public string? Descricao { get; set; }

        public TipoDeNegocio Negocio { get; set; }

        public int? CorretorNegocioId { get; set; }

        public int CorretorGestorId { get; set; }

        public int ClienteDonoId { get; set; }

        public bool Disponivel { get; set; }

        public List<IFormFile>? Fotos { get; set; }//Guardar um json de todas as fotos

    }

    public class ImovelViewModel
    {
        public int ImovelId { get; set; }

        public string Endereco { get; set; } = null!;

        public TipoImovel Tipo { get; set; }

        public string Titulo { get; set; }

        public decimal Area { get; set; }

        public decimal Valor { get; set; }

        public TipoDeNegocio Negocio { get; set; }

        public bool Disponivel { get; set; }

        public string FotoDeCapa { get; set; }
    }

    public static class ImovelViewModelExtensions
    {
        public static Imovel ToImovel(this CreateImovelViewModel createImovelViewModel)
        {
            var imovel = new Imovel
            {
                Area = createImovelViewModel.Area,
                Valor = createImovelViewModel.Valor,
                Disponivel = createImovelViewModel.Disponivel,
                Endereco = createImovelViewModel.Endereco,
                Negocio = (int)createImovelViewModel.Negocio,
                Tipo = (int)createImovelViewModel.Tipo,
                ClienteDonoId = createImovelViewModel.ClienteDonoId,
                CorretorGestorId = createImovelViewModel.CorretorGestorId,
                CorretorNegocioId = createImovelViewModel.CorretorNegocioId,
                Descricao = createImovelViewModel.Descricao
            };
            return imovel;
        }

        public static DetailsImovelViewModel ToDetailImovelViewModel(this Imovel imovel)
        {
            var fotoDeCapa = "/imagens/not-found.png";
            List<string> fotos = new();
            if (string.IsNullOrEmpty(imovel.Fotos) is false)
            {
                fotos = JsonConvert.DeserializeObject<List<string>>(imovel.Fotos);
            }
            else
            {
                fotos.Add(fotoDeCapa);
            }

            var imovelViewModel = new DetailsImovelViewModel
            {
                ImovelId = imovel.ImovelId,
                Area = imovel.Area,
                Valor = imovel.Valor,
                Disponivel = imovel.Disponivel,
                Endereco = imovel.Endereco,
                Fotos = fotos,
                Negocio = (TipoDeNegocio)imovel.Negocio,
                Tipo = (TipoImovel)imovel.Tipo,
                Descricao = imovel.Descricao,
                NomeCliente = imovel.ClienteDono.Nome,
                NomeCorretorGestor = imovel.CorretorGestor.Nome,
                NomeCorretorNegocio = imovel.CorretorNegocio?.Nome
            };

            return imovelViewModel;
        }

        public static ImovelViewModel ToImovelViewModel(this Imovel imovel)
        {
            string fotoDeCapa = "/imagens/not-found.png";
            if (string.IsNullOrEmpty(imovel.Fotos) is false)
            {
                List<string> fotos = JsonConvert.DeserializeObject<List<string>>(imovel.Fotos);
                fotoDeCapa = fotos.First();
            }

            var imovelViewModel = new ImovelViewModel
            {
                ImovelId = imovel.ImovelId,
                Area = imovel.Area,
                Valor = imovel.Valor,
                Disponivel = imovel.Disponivel,
                Endereco = imovel.Endereco,
                FotoDeCapa = fotoDeCapa,
                Negocio = (TipoDeNegocio)imovel.Negocio,
                Tipo = (TipoImovel)imovel.Tipo
            };

            imovelViewModel.Titulo = Enum.GetName(imovelViewModel.Tipo);
            return imovelViewModel;
        }

        public static List<ImovelViewModel> ToImoveisViewModel(this List<Imovel> imoveis)
        {
            List<ImovelViewModel> imovelViewModels = new();

            imoveis.ForEach(imovel => imovelViewModels.Add(imovel.ToImovelViewModel()));

            return imovelViewModels;
        }
    }
}
