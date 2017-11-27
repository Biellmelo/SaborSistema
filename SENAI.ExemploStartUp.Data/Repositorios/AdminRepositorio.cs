using SENAI.ExemploStartUp.Data.Context;
using SENAI.ExemploStartUp.Models.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SENAI.ExemploStartUp.Data.Repositorios
{
   public class AdminRepositorio
    {
        private ExemploStartUpContext db = new ExemploStartUpContext();

        public Admin EfetuarLogin(string email, string senha)
        {
            return db.Admins.Where(c => c.Email.Equals(email) && c.Senha.Equals(senha)).FirstOrDefault();
        }
    }
}
