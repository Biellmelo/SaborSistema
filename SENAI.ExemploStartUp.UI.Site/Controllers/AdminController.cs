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
using System.Web.Security;
using SENAI.ExemploStartUp.Dominio.Servicos;
using SENAI.ExemploStartUp.Util.Seguranca;

namespace SENAI.ExemploStartUp.UI.Site.Controllers
{
    public class AdminController : Controller
    {
        private SENAIExemploStartUpUISiteContext db = new SENAIExemploStartUpUISiteContext();

        [CustomAuthorize(Roles = "ADMIN")]
        public ActionResult Default(Admin admin)
        {
            return View();
        }

        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult EfetuarLogin(string email, string senha)
        {

            AdminServico adminServico = new AdminServico();
            Admin admin = adminServico.EfetuarLogin(email, senha);

            if (admin != null)
            {
                FormsAuthentication.SetAuthCookie(admin.Email, false);
                var authTicket = new FormsAuthenticationTicket(1, admin.Email,
                DateTime.Now, DateTime.MaxValue, false, admin.Permissao);
                string encryptedTicket = FormsAuthentication.Encrypt(authTicket);
                var authCookie = new HttpCookie(FormsAuthentication.FormsCookieName, encryptedTicket);
                HttpContext.Response.Cookies.Add(authCookie);
                Session.Add("SessionAdmin", admin);
                return View("Default", admin);
            }
            ViewBag.Erro = "Dados inválidos. Tente novamente!";
            return View("Login");
        }

        // GET: Admin
        [CustomAuthorize(Roles = "ADMIN")]
        public ActionResult Index()
        {
            return View(db.Admins.ToList());
        }

        // GET: Admin/Details/5
        [CustomAuthorize(Roles = "ADMIN")]
        public ActionResult Details(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Admin admin = db.Admins.Find(id);
            if (admin == null)
            {
                return HttpNotFound();
            }
            return View(admin);
        }

        // GET: Admin/Create
       // [CustomAuthorize(Roles = "ADMIN")]
        public ActionResult Create()
        {
            return View();
        }

        // POST: Admin/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        //[CustomAuthorize(Roles = "ADMIN")]
        public ActionResult Create([Bind(Include = "AdminId,Email,Senha")] Admin admin)
        {
            if (ModelState.IsValid)
            {
                admin.AdminId = Guid.NewGuid();
                admin.Senha = Criptografia.GetMD5Hash(admin.Senha);
                db.Admins.Add(admin);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(admin);
        }

        // GET: Admin/Edit/5
        [CustomAuthorize(Roles = "ADMIN")]
        public ActionResult Edit(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Admin admin = db.Admins.Find(id);
            if (admin == null)
            {
                return HttpNotFound();
            }
            return View(admin);
        }

        // POST: Admin/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [CustomAuthorize(Roles = "ADMIN")]
        public ActionResult Edit([Bind(Include = "AdminId,Email,Senha")] Admin admin)
        {
            if (ModelState.IsValid)
            {
                db.Entry(admin).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(admin);
        }

        // GET: Admin/Delete/5
        [CustomAuthorize(Roles = "ADMIN")]
        public ActionResult Delete(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Admin admin = db.Admins.Find(id);
            if (admin == null)
            {
                return HttpNotFound();
            }
            return View(admin);
        }

        // POST: Admin/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [CustomAuthorize(Roles = "ADMIN")]
        public ActionResult DeleteConfirmed(Guid id)
        {
            Admin admin = db.Admins.Find(id);
            db.Admins.Remove(admin);
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

        public ActionResult LogOut()
        {
            FormsAuthentication.SignOut();
            Session.Clear();
            Session.Abandon();
            return RedirectToAction("Login","Admin");
        }
    }
}
