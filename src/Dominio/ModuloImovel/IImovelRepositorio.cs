namespace Academia.Programador.Bk.Gestao.Imobiliaria.Dominio.ModuloImovel;

public interface IImovelRepositorio
{
    void CriarImovel(Imovel imovel);
    List<Imovel> TragaTodosImoveles();
    void SalvarImovel(Imovel imovel);
    Imovel TragaImovelPorId(int? id);
    void Remover(int id);
}