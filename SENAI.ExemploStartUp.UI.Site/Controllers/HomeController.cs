    using SENAI.ExemploStartUp.Models.Models;
using SENAI.ExemploStartUp.Dominio.Servicos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using SENAI.ExemploStartUp.UI.Site.ViewModels.Clientes;
using SENAI.ExemploStartUp.Util.Extensoes;
using SENAI.ExemploStartUp.Util.Seguranca;
using SENAI.ExemploStartUp.Data.Repositorios;

namespace SENAI.ExemploStartUp.UI.Site.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            //Cliente cliente = new Cliente();
            //cliente.Nome = "teste";
            //cliente.Email = "emailteste@gmail.com";
            //cliente.Ativo = true;
            //cliente.CPF = "12345678909";
            //cliente.DataNascimento = DateTime.Now;
            //cliente.Senha = "123";
            //cliente.Permissao = "Admins";

            //ClienteServico cs = new ClienteServico();
            //cs.InserirCliente(cliente);

            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        [HttpPost]
        public ActionResult EfetuarLogin(string email, string senha)
        {
            ClienteServico clienteServico = new ClienteServico();
            Cliente cliente = clienteServico.EfetuarLogin(email, senha);

            if (cliente != null)
            {
                FormsAuthentication.SetAuthCookie(cliente.Email, false);
                var authTicket = new FormsAuthenticationTicket(1, cliente.Email,
                DateTime.Now, DateTime.MaxValue, false, cliente.Permissao);
                string encryptedTicket = FormsAuthentication.Encrypt(authTicket);
                var authCookie = new HttpCookie(FormsAuthentication.FormsCookieName, encryptedTicket);
                HttpContext.Response.Cookies.Add(authCookie);
                Session.Add("SessionCliente", cliente);
                return View("Index"); 
            }
            return View("Index");
        }

        [HttpPost]
        public ActionResult CadastrarCliente(ClienteViewModel model)
        {

            if (!model.CPF.IsCpf())
                ModelState.AddModelError("CPF", "CPF INVÁLIDO !!!");

            if (!model.DataNascimento.ValidDate())
                ModelState.AddModelError("DataNascimento", "Data de Nascimento maior que data atual !!!");

            if (ModelState.IsValid)
            {
                Cliente cliente = new Cliente();
                ClienteServico cs = new ClienteServico();
                cliente.Ativo = true;
                var teste = cliente.CPF = model.CPF;
                cliente.DataNascimento = model.DataNascimento;
                cliente.Email = model.Email;
                cliente.Excluido = false;
                cliente.Nome = model.Nome;
                cliente.Senha = model.Senha;
                cs.InserirCliente(cliente);

                return RedirectToAction("Create", "Endereco");
            }

            return View("CadastrarCliente", model);


        }


        public ActionResult CadastrarCliente()
        {
            return View();
        }

        [HttpGet]
        public ActionResult Pesquisa()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Pesquisa(string texto)
        {
            ProdutoRepositorio pr = new ProdutoRepositorio();
            return View("Pesquisa", pr.Pesquisar(texto));
        }


    }
}