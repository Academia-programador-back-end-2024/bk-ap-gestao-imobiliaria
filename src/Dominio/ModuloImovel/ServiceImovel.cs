namespace Academia.Programador.Bk.Gestao.Imobiliaria.Dominio.ModuloImovel;

public class ServiceImovel : IServiceImovel
{
    private readonly IImovelRepositorio _imovelRepositorio;

    public ServiceImovel(IImovelRepositorio imovelRepositorio)
    {
        _imovelRepositorio = imovelRepositorio;
    }

    public int CriarImovel(Imovel imovel)
    {
        //TODO: Validação de duplicidade

        _imovelRepositorio.Criar(imovel);
        return imovel.ImovelId;
    }

    public List<Imovel> TragaTodosImoveis()
    {
        return _imovelRepositorio.TragaTodos();
    }

    public void SalvarImovel(Imovel imovel)
    {
        //TODO: Validação de duplicidade
        _imovelRepositorio.Salvar(imovel);
    }

    public Imovel TragaImovelPorId(int id)
    {
        return _imovelRepositorio.TragaPorId(id);
    }

    public void Remover(int id)
    {
        _imovelRepositorio.Remover(id);
    }
}