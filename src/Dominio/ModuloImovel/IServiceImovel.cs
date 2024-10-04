namespace Academia.Programador.Bk.Gestao.Imobiliaria.Dominio.ModuloImovel;

public interface IServiceImovel
{
    void CriarImovel(Imovel imovel);
    List<Imovel> TragaTodosImoveis();
    void SalvarImovel(Imovel imovel);
    Imovel TragaImovelPorId(int id);
    void Remover(int id);
}