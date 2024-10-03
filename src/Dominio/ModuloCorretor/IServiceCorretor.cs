namespace Academia.Programador.Bk.Gestao.Imobiliaria.Dominio.ModuloCorretor;

public interface IServiceCorretor
{
    void CriarCorretor(Corretor corretor);
    List<Corretor> TragaTodosCorretores();
    void SalvarCorretor(Corretor corretor);
    Corretor TragaCorretorPorId(int id);
    void Remover(int id);
}

public class CorretorExistenteException : Exception
{
    public CorretorExistenteException() : base("Dados invalidos para um corretor!")
    {

    }
}