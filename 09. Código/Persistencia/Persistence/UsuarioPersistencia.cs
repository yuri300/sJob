using System;
using System.Collections.Generic;
using System.Linq;
using Model.Models;

namespace Persistencia.Persistence
{
    public class UsuarioPersistencia
    {
        private static List<TipoUsuario> listaTipoUsuario;
        private static List<Usuario> listaUsuario;

        static UsuarioPersistencia()
        {
            listaTipoUsuario = new List<TipoUsuario>()
            {
                new TipoUsuario(){ Id = 1, Descricao = "Usuário" },
                new TipoUsuario(){ Id = 2, Descricao = "Administrador" }
            };
            listaUsuario = new List<Usuario>();
        }

        public void Gravar(Usuario usuario)
        {
            if (!usuario.Id.HasValue)
            {
                usuario.Id = listaUsuario.Count + 1;
                listaUsuario.Add(usuario);
            }
            else
            {
                int posicao = listaUsuario.FindIndex(u => u.Id == usuario.Id);
                listaUsuario[posicao] = usuario;
            }
        }

        public void Remover(int? id)
        {
            int posicao = listaUsuario.FindIndex(u => u.Id == id);
            listaUsuario.RemoveAt(posicao);
        }

        public Usuario Obter(Func<Usuario, bool> where)
        {
            return listaUsuario.Where(where).FirstOrDefault();
        }

        public List<Usuario> ObterTodos()
        {
            return listaUsuario;
        }

        public List<TipoUsuario> ObterTodosTipoUsuario()
        {
            return listaTipoUsuario;
        }

        public TipoUsuario ObterTipoUsuario(Func<TipoUsuario, bool> where)
        {
            return listaTipoUsuario.Where(where).FirstOrDefault();
        }
    }
}
