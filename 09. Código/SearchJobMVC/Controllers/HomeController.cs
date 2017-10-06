using Model.Models;
using Model.Models.Account;
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
    public class HomeController : Controller
    {
        private GerenciadorUsuario gerenciador;

        public HomeController()
        {
            gerenciador = new GerenciadorUsuario();
        }

        public ActionResult Index()
        {
            return View();
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
                    Usuario usuario = gerenciador.ObterByLoginSenha(dadosLogin.Login, dadosLogin.Senha);

                    // Autenticando.
                    if (usuario != null)
                    {
                        FormsAuthentication.SetAuthCookie(usuario.Login, dadosLogin.LembrarMe);
                        SessionHelper.Set(SessionKeys.USUARIO, usuario);
                        if (usuario.TipoUsuario.Id == ((int)SearchJobWeb.Util.TipoUsuario.USUARIO) + 1)
                            return RedirectToAction("Index","Usuarios");
                        else
                            return RedirectToAction("Index");
                    }
                }
                ModelState.AddModelError("", "Usuário e/ou senha inválidos.");
            }
            catch
            {
                ModelState.AddModelError("", "A autenticação falhou. Forneça informações válidas e tente novamente.");
            }
            // Se ocorrer algum erro, reexibe o formulário.
            return View();
        }

        public ActionResult AreaRestrita()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AreaRestrita(LoginModel dadosLogin)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    // Obtendo o usuário.
                    dadosLogin.Senha = Criptografia.GerarHashSenha(dadosLogin.Login + dadosLogin.Senha);
                    Usuario usuario = gerenciador.ObterByLoginSenha(dadosLogin.Login, dadosLogin.Senha);

                    // Autenticando.
                    if (usuario != null)
                    {
                        FormsAuthentication.SetAuthCookie(usuario.Login, dadosLogin.LembrarMe);
                        SessionHelper.Set(SessionKeys.USUARIO, usuario);
                        if (usuario.TipoUsuario.Id == ((int)SearchJobWeb.Util.TipoUsuario.USUARIO) + 1)
                            return RedirectToAction("Index", "Usuarios");
                        else
                            return RedirectToAction("Index");
                    }
                }
                ModelState.AddModelError("", "Usuário e/ou senha inválidos.");
            }
            catch
            {
                ModelState.AddModelError("", "A autenticação falhou. Forneça informações válidas e tente novamente.");
            }
            // Se ocorrer algum erro, reexibe o formulário.
            return View();
        }

    }
}