using SENAI.ExemploStartUp.Models.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SENAI.ExemploStartUp.UI.Site.ViewModels.Carrinho
{
    public class ProdutoDetalheViewModel
    {
        public Produto Produto { get; set; }
        public int Quantidade { get; set; }
        public Guid CarrinhoId { get; set; }
        public virtual CarrinhoDeCompras Carrinho { get; set; }

    }
}