namespace Academia.Programador.Bk.Gestao.Imobiliaria.Dominio.ModuloCorretor;

public class ServiceCorretor : IServiceCorretor
{
    private readonly ICorretorRepositorio _corretorRepositorio;

    public ServiceCorretor(ICorretorRepositorio corretorRepositorio)
    {
        _corretorRepositorio = corretorRepositorio;
    }
    public void CriarCorretor(Corretor corretor)
    {
        ValidacaoDuplicacao(Corretor: corretor);
        _corretorRepositorio.CriarCorretor(corretor);
    }

    public List<Corretor> TragaTodosCorretores()
    {
        return _corretorRepositorio.TragaTodosCorretores();
    }

    public void SalvarCorretor(Corretor corretor)
    {
        ValidacaoDuplicacao(Corretor: corretor);
        _corretorRepositorio.SalvarCorretor(corretor);
    }

    public Corretor TragaCorretorPorId(int id)
    {
        return _corretorRepositorio.TragaCorretorPorId(id);
    }

    public void Remover(int id)
    {
        _corretorRepositorio.Remover(id);
    }

    private void ValidacaoDuplicacao(Corretor Corretor)
    {
        var exiteCorretorCpf = _corretorRepositorio.CorretorPorCpf(Corretor.Cpf, Corretor.CorretorId);

        if (exiteCorretorCpf is true)
        {
            throw new CorretorExistenteException();
        }

        var exiteCorretorEmail = _corretorRepositorio.CorretorPorEmail(Corretor.Email, Corretor.CorretorId);

        if (exiteCorretorEmail is true)
        {
            throw new CorretorExistenteException();
        }

        var exiteCorretorCreci = _corretorRepositorio.CorretorPorCreci(Corretor.Creci, Corretor.CorretorId);

        if (exiteCorretorCreci is true)
        {
            throw new CorretorExistenteException();
        }
    }

}