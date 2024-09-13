namespace Academia.Programador.Bk.Gestao.Imobiliaria.Dominio.ModuloCliente;

public class ClienteExistenteException : Exception
{
    public ClienteExistenteException() : base("Cliente com essas informações já existe.")
    {
    }
}