using SENAI.ExemploStartUp.Data.Context;
using SENAI.ExemploStartUp.Models.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SENAI.ExemploStartUp.Data.Repositorios
{
     public class PromocaoRepositorio
    {
        private ExemploStartUpContext db = new ExemploStartUpContext();

        public void NovaPromocao(Promocao promocao)
        {
            db.Promocoes.Add(promocao);
           
        }

        public List<Promocao> PromocaoDoDia(string dia)
        {
            var Lista = db.Promocoes.Where(m => m.DiaSemana.Equals(dia)).ToList();

            return Lista;
        }
    }
}
