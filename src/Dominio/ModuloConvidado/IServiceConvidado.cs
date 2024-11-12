using Academia.Programador.Bk.Gestao.Imobiliaria.Dominio.ModuloImovel;

namespace Academia.Programador.Bk.Gestao.Imobiliaria.Dominio.ModuloConvidado
{
    public interface IServiceConvidado
    {
        List<Imovel> TragaUltimosSeis();
        void CreateContact(string email, string telefone, string nome);
    }

    public class ServiceConvidado : IServiceConvidado
    {
        private readonly IImovelRepositorio _imovelRepositorio;
        private readonly IContactRepository _contactRepository;

        public ServiceConvidado(IImovelRepositorio imovelRepositorio)
        {
            _imovelRepositorio = imovelRepositorio;
        }

        public List<Imovel> TragaUltimosSeis()
        {
            return _imovelRepositorio.TragaOsMaisRecentes(6);
        }

        public void CreateContact(string email, string telefone, string nome)
        {
            //TODO: Armazenar no banco de dados para a rotina entrar em contato com os corretores

        }
    }


}
