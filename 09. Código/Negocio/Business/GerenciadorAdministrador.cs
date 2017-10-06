using Model.Models;
using Persistencia.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Negocio.Business
{
    public class GerenciadorAdministrador
    {
        private AdministradorPersistencia persistenciaAdmin;
        private UsuarioPersistencia persistenciaUsuario;
        public GerenciadorAdministrador()
        {
            persistenciaUsuario = new UsuarioPersistencia();
            persistenciaAdmin = new AdministradorPersistencia();
        }

        public void Adicionar(Usuario usuario)
        {
            persistenciaUsuario.Gravar(usuario);
        }

        public void Editar(Usuario usuario)
        {
            persistenciaUsuario.Gravar(usuario);
        }

        public void Remover(Usuario usuario)
        {
            persistenciaUsuario.Remover(usuario.Id);
        }

        public Usuario Obter(int? id)
        {
            return persistenciaUsuario.Obter(e => e.Id == id);
        }

        public Administrador ObterByLoginSenha(string login, string senha)
        {
            return persistenciaAdmin.Obter(e => e.Login.ToLowerInvariant().Equals(login.ToLowerInvariant()) &&
                e.Senha.ToLowerInvariant().Equals(senha.ToLowerInvariant()));
        }

        public List<Usuario> ObterTodos()
        {
            return persistenciaUsuario.ObterTodos();
        }

        public List<TipoUsuario> ObterTodosTipoUsuario()
        {
            return persistenciaAdmin.ObterTodosTipoUsuario();
        }

        public TipoUsuario ObterTipoUsuario(int id)
        {
            return persistenciaUsuario.ObterTipoUsuario(e => e.Id == id);
        }
    }
}
