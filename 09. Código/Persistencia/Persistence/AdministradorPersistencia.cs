using Model.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistencia.Persistence
{
    public class AdministradorPersistencia
    {
        private static List<TipoUsuario> listaTipoUsuario;
        private static List<Administrador> listaAdministrador;

        static AdministradorPersistencia()
        {
            listaTipoUsuario = new List<TipoUsuario>()
            {
                new TipoUsuario(){ Id = 1, Descricao = "Usuário" },
                new TipoUsuario(){ Id = 2, Descricao = "Administrador" }
            };
            listaAdministrador = new List<Administrador>();
        }
        public Administrador Obter(Func<Administrador, bool> where)
        {
            return listaAdministrador.Where(where).FirstOrDefault();
        }
        public TipoUsuario ObterTipoUsuario(Func<TipoUsuario, bool> where)
        {
            return listaTipoUsuario.Where(where).FirstOrDefault();
        }
        public List<TipoUsuario> ObterTodosTipoUsuario()
        {
            return listaTipoUsuario;
        }
    }
}
