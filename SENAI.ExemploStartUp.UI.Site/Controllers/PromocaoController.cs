using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using SENAI.ExemploStartUp.Data.Context;
using SENAI.ExemploStartUp.Models.Models;
using SENAI.ExemploStartUp.Dominio.Servicos;

namespace SENAI.ExemploStartUp.UI.Site.Controllers
{
    public class PromocaoController : Controller
    {
        private ExemploStartUpContext db = new ExemploStartUpContext();
        private PromocaoServico PromocaoServico = new PromocaoServico();

        // GET: Promocao
        public ActionResult Index()
        {
            var promocoes = db.Promocoes.Include(p => p.Produto);
            return View(promocoes.ToList());
        }

        public ActionResult DoDia()
        {
            return View();
        }

        [HttpPost]
        public ActionResult DoDia(string id)
        {
           var prom = PromocaoServico.PromocaoDoDia(id);
            return View("Promocao", prom);
        }


        // GET: Promocao/Details/5
        public ActionResult Details(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Promocao promocao = db.Promocoes.Find(id);
            if (promocao == null)
            {
                return HttpNotFound();
            }
            return View(promocao);
        }

        // GET: Promocao/Create
        public ActionResult Create()
        {
            ViewBag.ProdutoID = new SelectList(db.Produtos, "ProdutoId", "Nome");
            return View();
        }

        // POST: Promocao/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "PromocaoID,Descricao,DiaSemana,Desconto,ProdutoID")] Promocao promocao)
        {
            if (ModelState.IsValid)
            {
                promocao.PromocaoID = Guid.NewGuid();
                db.Promocoes.Add(promocao);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.ProdutoID = new SelectList(db.Produtos, "ProdutoId", "Nome", promocao.ProdutoID);
            return View(promocao);
        }

        // GET: Promocao/Edit/5
        public ActionResult Edit(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Promocao promocao = db.Promocoes.Find(id);
            if (promocao == null)
            {
                return HttpNotFound();
            }
            ViewBag.ProdutoID = new SelectList(db.Produtos, "ProdutoId", "Nome", promocao.ProdutoID);
            return View(promocao);
        }

        // POST: Promocao/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "PromocaoID,Descricao,DiaSemana,Desconto,ProdutoID")] Promocao promocao)
        {
            if (ModelState.IsValid)
            {
                db.Entry(promocao).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ProdutoID = new SelectList(db.Produtos, "ProdutoId", "Nome", promocao.ProdutoID);
            return View(promocao);
        }

        // GET: Promocao/Delete/5
        public ActionResult Delete(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Promocao promocao = db.Promocoes.Find(id);
            if (promocao == null)
            {
                return HttpNotFound();
            }
            return View(promocao);
        }

        // POST: Promocao/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(Guid id)
        {
            Promocao promocao = db.Promocoes.Find(id);
            db.Promocoes.Remove(promocao);
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
