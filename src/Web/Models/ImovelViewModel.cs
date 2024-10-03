using Academia.Programador.Bk.Gestao.Imobiliaria.Dominio.ModuloImovel;

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
        public static ImovelViewModel ToImovelViewModel(this Imovel imovel)
        {
            var imovelViewModel = new ImovelViewModel
            {
                ImovelId = imovel.ImovelId,
                Area = imovel.Area,
                Valor = imovel.Valor,
                Disponivel = imovel.Disponivel,
                Endereco = imovel.Endereco,
                FotoDeCapa = "/imagens/not-found.png",
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
