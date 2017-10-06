using System;
using System.Web.Mvc;
using Model.Models;
using Negocio.Business;
using System.Web.Security;
using SearchJobWeb.Util;
using Model.Models.Account;
using Model.Models.Exceptions;
using System.Collections.Generic;
namespace SearchJobWeb.Controllers
{
    public class UsuariosController : Controller
    {
        private GerenciadorUsuario gerenciador;

        public UsuariosController()
        {
            gerenciador = new GerenciadorUsuario();
        }

        [Authenticated]
        public ActionResult Logout()
        {
            if (User.Identity.IsAuthenticated)
            {
                FormsAuthentication.SignOut();
                Session.Abandon();
            }
            return RedirectToAction("Index", "Home");
        }

        [Authenticated]
        [CustomAuthorize(NivelAcesso = Util.TipoUsuario.USUARIO)]
        public ActionResult Index()
        {
            
            return View();
        }

        public ActionResult Cadastrar()
        {
            ViewBag.TipoUsuario = new SelectList(gerenciador.ObterTodosTipoUsuario(), "Id", "Descricao");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Cadastrar(Usuario usu)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    usu.Senha = Criptografia.GerarHashSenha(usu.Login + usu.Senha);
                    gerenciador.Adicionar(usu);
                    return RedirectToAction("Index", "Home");
                }
                return View(usu);
            }
            catch
            {
                return View(usu);
            }
        }

        public ActionResult Editar(int? id)
        {
            try
            {
                if (id.HasValue)
                {
                    Usuario usu = gerenciador.Obter(id);
                    if (usu != null)
                        return View(usu);
                }
            }
            catch (NegocioException e)
            {
                throw new ControllerException("Não foi possivel utilizar o objeto para edição");
            }
            return RedirectToAction("Index");
        }

        [HttpPost]
        public ActionResult Editar(FormCollection form)
        {
            try
            {
                Usuario usu = new Usuario();
                TryUpdateModel(usu, form.ToValueProvider());
                int idTipo = Convert.ToInt32(form["TipoUsuario"]);
                usu.TipoUsuario = gerenciador.ObterTipoUsuario(idTipo);
                gerenciador.Adicionar(usu);
                return RedirectToAction("Index");
            }
            catch
            {
                throw new ControllerException("Não foi possivel utilizar o objeto para edição");
            }
        }

        public ActionResult Deletar(int? id)
        {
            try
            {
                if (id.HasValue)
                {
                    Usuario usu = gerenciador.Obter(id);
                    if (usu != null)
                        return View(usu);
                }
            }
            catch (NegocioException e)
            {
                throw new ControllerException("Não foi possivel remover.");
            }
            return RedirectToAction("Index");
        }

        [HttpPost]
        public ActionResult Deletar(int id, Usuario usu)
        {
            try
            {   
                gerenciador.Remover(usu);                
                return RedirectToAction("Index","Home");
            }
            catch
            {
                throw new ControllerException("Não foi possivel remover");                
            }
        }

        public ActionResult ListarEmprego(string emprego)
        {
            if (emprego == null)
            {
                return RedirectToAction("Index", "Usuario");
            }
            else
            {
                return PartialView("ListarEmprego", gerenciador.ObterByPesquisaEmprego(emprego));
            }
        }
    }
}

