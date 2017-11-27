using SENAI.ExemploStartUp.Models.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SENAI.ExemploStartUp.UI.Site.ViewModels.Carrinho
{
    public class CarrinhoViewModel
    {
        [Key]   
        public Guid id { get; set; }
        public CarrinhoDeCompras Carrinho { get; set; }
        public List<ItemCarrinho> ItensCarrinho { get; set; }
        public string ValorTotal { get; set; }

    }
}