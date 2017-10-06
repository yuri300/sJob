using Model.Models;
using Model.Models.Account;
using Model.Models.Exceptions;
using Negocio.Business;
using SearchJobWeb.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace SearchJobMVC.Controllers
{
    public class AdministradoresController : Controller
    {

        private GerenciadorAdministrador gerenciador;

        public AdministradoresController()
        {
            gerenciador = new GerenciadorAdministrador();
        }

        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(LoginModel dadosLogin)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    // Obtendo o usuário.
                    dadosLogin.Senha = Criptografia.GerarHashSenha(dadosLogin.Login + dadosLogin.Senha);
                    Administrador admin = gerenciador.ObterByLoginSenha(dadosLogin.Login, dadosLogin.Senha);

                    // Autenticando.
                    if (admin != null)
                    {
                        FormsAuthentication.SetAuthCookie(admin.Login, dadosLogin.LembrarMe);
                        SessionHelper.Set(SessionKeys.ADMINISTRADOR, admin);
                        if (admin.TipoUsuario.Id == ((int)SearchJobWeb.Util.TipoUsuario.ADMINISTRADOR) + 1)
                            return RedirectToAction("Index", "Administradores");
                        else
                            return RedirectToAction("Index");
                    }
                }
                ModelState.AddModelError("", "Login do Administrador e/ou senha inválidos.");
            }
            catch
            {
                ModelState.AddModelError("", "A autenticação falhou. Forneça informações válidas e tente novamente.");
            }
            // Se ocorrer algum erro, reexibe o formulário.
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
        [CustomAuthorize(NivelAcesso = SearchJobWeb.Util.TipoUsuario.ADMINISTRADOR)]
        public ActionResult Index()
        {

            return View();
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
                throw new ControllerException("Não foi possivel remover o usuário");
            }
            return RedirectToAction("Index");
        }

        [HttpPost]
        public ActionResult Deletar(FormCollection form)
        {
            try
            {
                Usuario usu = new Usuario();
                TryUpdateModel(usu, form.ToValueProvider());
                int idTipo = Convert.ToInt32(form["TipoUsuario"]);
                usu.TipoUsuario = gerenciador.ObterTipoUsuario(idTipo);
                gerenciador.Remover(usu);
                return RedirectToAction("Index");
            }
            catch
            {
                throw new ControllerException("Não foi possivel remover o usuário");
            }
        }

        public ActionResult MostrarTodos()
        {
            List<Usuario> model = null;
            try
            {
                model = gerenciador.ObterTodos();
                if (model == null || model.Count == 0)
                    model = null;
            }
            catch (NegocioException e)
            {
                model = null;
            }
            return View(model);
        }

    }
}

