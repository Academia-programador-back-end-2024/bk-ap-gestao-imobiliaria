using Academia.Programador.Bk.Gestao.Imobiliaria.Dominio.ModuloCorretor;

namespace Academia.Programador.Bk.Gestao.Imobiliaria.Dominio.ModuloConvidado;

public class Contact
{
    public int Id { get; set; }
    public string Email { get; set; }
    public string telefone { get; set; }
    public string nome { get; set; }
    public bool Pendente { get; set; }
    public int? CorretorId { get; set; }

    public DateTime DataPedido { get; set; }

    public virtual Corretor Corretor { get; set; }
}