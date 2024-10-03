using Academia.Programador.Bk.Gestao.Imobiliaria.Dominio.ModuloCliente;
using Academia.Programador.Bk.Gestao.Imobiliaria.Dominio.ModuloCorretor;
using Academia.Programador.Bk.Gestao.Imobiliaria.Dominio.ModuloImovel;

namespace Academia.Programador.Bk.Gestao.Imobiliaria.Web;

public partial class MensagensContato
{
    public int MensagemId { get; set; }

    public int ImovelId { get; set; }

    public int ClienteId { get; set; }

    public int CorretorId { get; set; }

    public string Mensagem { get; set; } = null!;

    public DateTime DataEnvio { get; set; }

    public virtual Cliente Cliente { get; set; } = null!;

    public virtual Corretor Corretor { get; set; } = null!;

    public virtual Imovel Imovel { get; set; } = null!;
}
