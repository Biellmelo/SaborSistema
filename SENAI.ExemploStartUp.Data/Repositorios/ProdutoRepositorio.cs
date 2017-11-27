    using SENAI.ExemploStartUp.Data.Context;
using SENAI.ExemploStartUp.Models.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SENAI.ExemploStartUp.Data.Repositorios
{
   public class ProdutoRepositorio
    {
        private ExemploStartUpContext db = new ExemploStartUpContext();

        public Produto RetornarPorId(Guid idProduto)
        {
            return db.Produtos.Where(p => p.ProdutoId == idProduto).FirstOrDefault();
        }

        public List<Produto> Pesquisar(string Texto )
        {
            
            return db.Produtos.Where(t => t.Nome.Contains(Texto)).ToList();


        }


    }
}
