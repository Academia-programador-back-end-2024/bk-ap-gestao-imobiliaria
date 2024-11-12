using Academia.Programador.Bk.Gestao.Imobiliaria.Dominio.Compartilhada;

namespace Academia.Programador.Bk.Gestao.Imobiliaria.Dominio.ModuloImovel;

public interface IImovelRepositorio : IRepositorio<Imovel>
{
    List<Imovel> TragaOsMaisRecentes(int NumeroDeImoveis);
}