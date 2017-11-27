using SENAI.ExemploStartUp.Data.Context;
using SENAI.ExemploStartUp.Data.Repositorios;
using SENAI.ExemploStartUp.Dominio.Servicos;
using SENAI.ExemploStartUp.Models.Models;

using SENAI.ExemploStartUp.UI.Site.Models;
using SENAI.ExemploStartUp.UI.Site.ViewModels.Carrinho;
using SENAI.ExemploStartUp.UI.Site.ViewModels.Clientes;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace SENAI.ExemploStartUp.UI.Site.Controllers
{
    public class ClienteController : Controller
    {

        //////////////////////////////////////////
        ////////////PARA CLIENTES////////////////
        ////////////////////////////////////////

        [CustomAuthorize(Roles = "CLIENTE")]
        public ActionResult DetalheCliente(Guid? id)
        {

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Cliente cliente = db.Clientes.Find(id);
            if (cliente == null)
            {
                return HttpNotFound();
            }
            return View(cliente);

        }

        [CustomAuthorize(Roles = "CLIENTE")]
        public ActionResult Editar(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Cliente cliente = db.Clientes.Find(id);
            if (cliente == null)
            {
                return HttpNotFound();
            }
            return View(cliente);
        }



        [CustomAuthorize(Roles = "CLIENTE")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Editar([Bind(Include = "Id,Nome,CPF,Email,DataNascimento,Ativo,Excluido,Senha,Permissao")] Cliente cliente)
        {
            if (ModelState.IsValid)
            {
                db.Entry(cliente).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("MinhaConta");
            }
            return View(cliente);
        }

        [CustomAuthorize(Roles = "CLIENTE")]
        public ActionResult MinhaConta()
        {
            return View();
        }

        [CustomAuthorize(Roles = ("CLIENTE"))]
        public ActionResult MinhasCompras(Guid cliente)
        {
            CarrinhoServico carrinhoServico = new CarrinhoServico();

            var Compras = carrinhoServico.MinhasVendas(cliente);
            return RedirectToAction("MinhasCompras", Compras);
        }


        ExemploStartUpContext db = new ExemploStartUpContext();
        // GET: Cliente
        [CustomAuthorize(Roles = "CLIENTE")]
        public ActionResult Index()
        {
            if (Session["SessionCliente"] != null)
            {
                Cliente cliente = (Cliente)Session["SessionCliente"];
                return View(cliente);
                //CAST >>> CONVERSÃO
            }
            else
            {
                FormsAuthentication.SignOut(); //REMOVE TODAS AS PERMISSÕES E JOGA PARA FORA DA ÁREA 
                // LOGADA.
                return RedirectToAction("Index", "Home");
                
            }

           
        }

        ////////////////////////////////////////////
       ///////////////PARA ADMIN///////////////////
      ////////////////////////////////////////////

        [CustomAuthorize(Roles = "ADMIN")]
        public ActionResult TodosOsCliente()
        {
            var cli = db.Clientes;
            return View(cli.ToList());
            
        }

        public ActionResult NovoCliente()
        {
            return View();
        }


        [CustomAuthorize(Roles = "ADMIN")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult NovoCliente([Bind(Include = "Id,Nome,CPF,Email,DataNascimento,DataCadastro,Ativo,Excluido,Senha,Permissao")] Cliente cliente)
        {
            if (ModelState.IsValid)
            {
                cliente.ClienteId = Guid.NewGuid();
                db.Clientes.Add(cliente);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(cliente);
        }

        [CustomAuthorize(Roles = "ADMIN")]
        public ActionResult EditarCliente(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Cliente cliente = db.Clientes.Find(id);
            if (cliente == null)
            {
                return HttpNotFound();
            }
            return View(cliente);
        }

        [CustomAuthorize(Roles = "ADMIN")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditarCliente([Bind(Include = "Id,Nome,CPF,Email,DataNascimento,Ativo,Excluido,Senha,Permissao")] Cliente cliente)
        {
            if (ModelState.IsValid)
            {
                db.Entry(cliente).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("TodosOsClientes");
            }
            return View(cliente);
        }

        [CustomAuthorize(Roles = "ADMIN")]
        public ActionResult DeletarCliente(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Cliente cliente = db.Clientes.Find(id);
            if (cliente == null)
            {
                return HttpNotFound();
            }
            return View(cliente);
        }

        [CustomAuthorize(Roles = "ADMIN")]
        [HttpPost, ActionName("DeletarCliente")]
        [ValidateAntiForgeryToken]
        public ActionResult DeletarCliente(Guid id)
        {
            Cliente cliente = db.Clientes.Find(id);
            db.Clientes.Remove(cliente);
            db.SaveChanges();
            return RedirectToAction("TodosOsCliente");

        }

        public ActionResult LogOut()
        {
            FormsAuthentication.SignOut();
            Session.Clear();
            Session.Abandon();
            return RedirectToAction("Index", "Home");
        }
    }
}