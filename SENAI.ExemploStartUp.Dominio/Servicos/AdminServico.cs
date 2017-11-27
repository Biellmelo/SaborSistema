using SENAI.ExemploStartUp.Data.Repositorios;
using SENAI.ExemploStartUp.Models.Models;
using SENAI.ExemploStartUp.Util.Seguranca;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SENAI.ExemploStartUp.Dominio.Servicos
{
    public class AdminServico
    {


        public Admin EfetuarLogin(string email, string senha)
        {
            AdminRepositorio adminRepositorio = new AdminRepositorio();
            Admin admin = adminRepositorio.EfetuarLogin(email, Criptografia.GetMD5Hash(senha));
            return admin;
        }

    }
}
