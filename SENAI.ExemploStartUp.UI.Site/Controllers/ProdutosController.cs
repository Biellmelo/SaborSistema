using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using SENAI.ExemploStartUp.Models.Models;
using SENAI.ExemploStartUp.UI.Site.Models;
using SENAI.ExemploStartUp.UI.Site.ViewModels.Produtos;
using SENAI.ExemploStartUp.Data.Repositorios;
using SENAI.ExemploStartUp.Dominio.Servicos;

namespace SENAI.ExemploStartUp.UI.Site.Controllers
{
    //[CustomAuthorize(Roles ="ADMIN")]
    public class ProdutosController : Controller
    {
        private SENAIExemploStartUpUISiteContext db = new SENAIExemploStartUpUISiteContext();
        private ProdutoRepositorio pr = new ProdutoRepositorio();

        // GET: Produtos
        public ActionResult Index()
        {
            var produtoes = db.Produtoes.Include(p => p.Categoria);
            return View(produtoes.ToList());
        }


        public ActionResult Pesquisa()
        {
            
            
            return View();
        }


        [HttpPost]
        public ActionResult Pesquisa(string id)
        {
            
            var produtos = pr.Pesquisar(id);
            return View("Produtos", produtos);
        }


        // GET: Produtos/Details/5
        public ActionResult Details(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Produto produto = db.Produtoes.Find(id);
            if (produto == null)
            {
                return HttpNotFound();
            }
            return View(produto);
        }

        public ActionResult Details_Cliente(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Produto produto = db.Produtoes.Find(id);
            if (produto == null)
            {
                return HttpNotFound();
            }
            return View(produto);
        }

        // GET: Produtos/Create
        public ActionResult Create()
        {
            ViewBag.CategoriaId = new SelectList(db.Categorias, "CategoriaId", "Descricao");
            return View();
        }

        // POST: Produtos/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(ProdutoViewModel model)
        {
            if (ModelState.IsValid)
            {
                string nomeFoto = "SEMIMAGEM.JPG";
                string path = HttpContext.Server.MapPath("/Imagens/Produtos/");
                if (model.Imagem != null)
                     {
                    //UPLOAD IMAGEM
                    nomeFoto = Guid.NewGuid().ToString() +
                    model.Imagem.FileName.Substring(
                        model.Imagem.FileName.IndexOf("."));
                    model.Imagem.SaveAs(path + nomeFoto);
                }
                   

                //AutoMapper - DESAFIO - 
                Produto produto = new Produto()
                {
                    ProdutoId = model.ProdutoId,
                    Ativo = model.Ativo,
                    CategoriaId = model.CategoriaId,
                    Descricao = model.Descricao,
                    Imagem = nomeFoto,
                    Nome = model.Nome,
                    Preco = model.Preco,
                    Quantidade = model.Quantidade
                };

                db.Produtoes.Add(produto);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.CategoriaId = new SelectList(db.Categorias, "CategoriaId", "Descricao", model.CategoriaId);
            return View(model);
        }

        // GET: Produtos/Edit/5
        public ActionResult Edit(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Produto produto = db.Produtoes.Find(id);
            if (produto == null)
            {
                return HttpNotFound();
            }
            ViewBag.CategoriaId = new SelectList(db.Categorias, "CategoriaId", "Descricao", produto.CategoriaId);
            return View(produto);
        }

        // POST: Produtos/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ProdutoId,Nome,Preco,Quantidade,Imagem,Ativo,CategoriaId")] Produto produto)
        {
            if (ModelState.IsValid)
            {
                db.Entry(produto).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.CategoriaId = new SelectList(db.Categorias, "CategoriaId", "Descricao", produto.CategoriaId);
            return View(produto);
        }

        // GET: Produtos/Delete/5
        public ActionResult Delete(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Produto produto = db.Produtoes.Find(id);
            if (produto == null)
            {
                return HttpNotFound();
            }
            return View(produto);
        }

        // POST: Produtos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(Guid id)
        {
            Produto produto = db.Produtoes.Find(id);
            db.Produtoes.Remove(produto);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

 
    }
}
