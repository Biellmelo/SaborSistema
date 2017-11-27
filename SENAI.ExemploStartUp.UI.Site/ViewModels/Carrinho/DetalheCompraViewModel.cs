using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SENAI.ExemploStartUp.UI.Site.ViewModels.Carrinho
{
    public class DetalheCompraViewModel
    {
        [Key]
        public Guid id { get; set; }

        public string NomeCliente { get; set; }

        public string Pedido { get; set; }

        public DateTime DataCompra { get; set; }

    }
}