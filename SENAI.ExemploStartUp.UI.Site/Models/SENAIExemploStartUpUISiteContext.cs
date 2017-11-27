using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace SENAI.ExemploStartUp.UI.Site.Models
{
    public class SENAIExemploStartUpUISiteContext : DbContext
    {
        // You can add custom code to this file. Changes will not be overwritten.
        // 
        // If you want Entity Framework to drop and regenerate your database
        // automatically whenever you change your model schema, please use data migrations.
        // For more information refer to the documentation:
        // http://msdn.microsoft.com/en-us/data/jj591621.aspx
    
        public SENAIExemploStartUpUISiteContext() : base("DefaultConnection")
        {
        }

        public System.Data.Entity.DbSet<SENAI.ExemploStartUp.Models.Models.Admin> Admins { get; set; }

        public System.Data.Entity.DbSet<SENAI.ExemploStartUp.Models.Models.Categoria> Categorias { get; set; }

        public System.Data.Entity.DbSet<SENAI.ExemploStartUp.Models.Models.Produto> Produtoes { get; set; }

        public System.Data.Entity.DbSet<SENAI.ExemploStartUp.Models.Models.Cliente> Clientes { get; set; }

        public System.Data.Entity.DbSet<SENAI.ExemploStartUp.UI.Site.ViewModels.Clientes.ClienteViewModel> ClienteViewModels { get; set; }

        public System.Data.Entity.DbSet<SENAI.ExemploStartUp.UI.Site.ViewModels.Carrinho.CarrinhoViewModel> CarrinhoViewModels { get; set; }

        public System.Data.Entity.DbSet<SENAI.ExemploStartUp.Models.Models.ItemCarrinho> ItemCarrinhoes { get; set; }

        public System.Data.Entity.DbSet<SENAI.ExemploStartUp.Models.Models.CarrinhoDeCompras> CarrinhoDeCompras { get; set; }

        public System.Data.Entity.DbSet<SENAI.ExemploStartUp.Models.Models.Promocao> Promocaos { get; set; }
    }
}
