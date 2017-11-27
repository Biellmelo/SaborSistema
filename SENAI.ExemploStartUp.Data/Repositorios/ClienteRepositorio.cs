using SENAI.ExemploStartUp.Data.Context;
using SENAI.ExemploStartUp.Models.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SENAI.ExemploStartUp.Data.Repositorios
{
    public class ClienteRepositorio
    {

        private ExemploStartUpContext db = new ExemploStartUpContext();

        public ClienteRepositorio()
        {

        }

        public Cliente EfetuarLogin(string email, string senha)
        {
            return db.Clientes.Where(c => c.Email.Equals(email) && c.Senha.Equals(senha)).FirstOrDefault();

        }

        //public Cliente TodosOsEmail(string email)
        //{
            
        //}
        public void InserirCliente(Cliente cliente)
        {
            db.Clientes.Add(cliente);
            db.SaveChanges();
        }

        public List<Cliente> GettAll()
        {
            return db.Clientes.ToList();
        }

       

    }
}
