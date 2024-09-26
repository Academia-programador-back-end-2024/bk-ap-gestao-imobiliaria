namespace Academia.Programador.Bk.Gestao.Imobiliaria.Dominio.ModuloCorretor;

public interface ICorretorRepositorio
{
    void CriarCorretor(Corretor corretor);
    List<Corretor> TragaTodosCorretores();
    void SalvarCorretor(Corretor corretor);
    Corretor TragaCorretorPorId(int? id);
    void Remover(int id);
    bool CorretorPorCpf(string cpf, int corretorId);
    bool CorretorPorEmail(string? email, int corretorId);
    bool CorretorPorCreci(string creci, int corretorId);
}