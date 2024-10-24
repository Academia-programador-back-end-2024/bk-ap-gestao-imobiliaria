using Academia.Programador.Bk.Gestao.Imobiliaria.Dominio.Compartilhada;

namespace Academia.Programador.Bk.Gestao.Imobiliaria.Dominio.ModuloUsuario
{
    public interface IServiceUsuario : IServiceModel<Usuario> { }

    public interface IUsuarioRepositorio : IRepositorio<Usuario> { }

    public class ServiceUsuario : IServiceUsuario
    {
        private readonly IUsuarioRepositorio _usuarioRepositorio;

        public ServiceUsuario(IUsuarioRepositorio usuarioRepositorio)
        {
            _usuarioRepositorio = usuarioRepositorio;
        }

        public void Criar(Usuario model)
        {
            _usuarioRepositorio.Criar(model);
        }

        public List<Usuario> TragaTodos()
        {
            return _usuarioRepositorio.TragaTodos();
        }

        public void Salvar(Usuario model)
        {
            _usuarioRepositorio.Salvar(model);
        }

        public Usuario TragaPorId(int id)
        {
            return _usuarioRepositorio.TragaPorId(id);
        }

        public void Remover(int id)
        {
            _usuarioRepositorio.Remover(id);
        }
    }
}
