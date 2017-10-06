using System;
using System.Collections.Generic;
using Model.Models;
using Persistencia.Persistence;
using System.Linq;

namespace Negocio.Business
{
    public class GerenciadorUsuario
    {
        private UsuarioPersistencia persistencia;

        public GerenciadorUsuario()
        {
            persistencia = new UsuarioPersistencia();
        }

        public void Adicionar(Usuario usuario)
        {
            persistencia.Gravar(usuario);
        }

        public void Editar(Usuario usuario)
        {
            persistencia.Gravar(usuario);
        }

        public void Remover(Usuario usuario)
        {
            persistencia.Remover(usuario.Id);
        }

        public Usuario Obter(int? id)
        {
            return persistencia.Obter(e => e.Id == id);
        }

        public Usuario ObterByLoginSenha(string login, string senha)
        {
            return persistencia.Obter(e => e.Login.ToLowerInvariant().Equals(login.ToLowerInvariant()) &&
                e.Senha.ToLowerInvariant().Equals(senha.ToLowerInvariant()));
        }

        public List<Usuario> ObterTodos()
        {
            return persistencia.ObterTodos();
        }

        public List<TipoUsuario> ObterTodosTipoUsuario()
        {
            return persistencia.ObterTodosTipoUsuario();
        }

        public List<Usuario> ObterByPesquisaEmprego(string Emprego)
        {
            return persistencia.ObterTodos().Where(e => e.NomeEmp.ToLowerInvariant().Equals(Emprego.ToLowerInvariant()) && e.IsDisponibilidade.Equals(true)).ToList();                
        }

        public TipoUsuario ObterTipoUsuario(int id)
        {
            return persistencia.ObterTipoUsuario(e => e.Id == id);
        }
    }
}