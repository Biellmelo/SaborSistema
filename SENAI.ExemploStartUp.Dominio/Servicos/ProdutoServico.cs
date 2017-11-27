using SENAI.ExemploStartUp.Data.Repositorios;
using SENAI.ExemploStartUp.Models.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SENAI.ExemploStartUp.Dominio.Servicos
{
    public class ProdutoServico
    {
        public Produto RetornarPorId(Guid idProduto)
        {
            ProdutoRepositorio pr = new ProdutoRepositorio();
            return pr.RetornarPorId(idProduto);
        }

        public List<Produto> Pesquisar(string Texto)
        {
            ProdutoRepositorio pr = new ProdutoRepositorio();
            return pr.Pesquisar(Texto);
        }
    }
}