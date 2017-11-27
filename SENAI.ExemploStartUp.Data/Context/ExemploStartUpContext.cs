using SENAI.ExemploStartUp.Models.Models;
using System;
using System.Data.Entity;
using System.Linq;


namespace SENAI.ExemploStartUp.Data.Context
{
    public class ExemploStartUpContext : DbContext
    {
        public ExemploStartUpContext()
            : base("DefaultConnection")
        {

        }

        public DbSet<Admin> Admins { get; set; }

        public DbSet<Cliente> Clientes { get; set; }

        public DbSet<Endereco> Enderecos { get; set; }

        public DbSet<Categoria> Categorias { get; set; }

        public DbSet<Produto> Produtos { get; set; }

        public DbSet<CarrinhoDeCompras> Carrinhos { get; set; }

        public DbSet<ItemCarrinho> ItensCarrinho { get; set; }

        public DbSet<Promocao> Promocoes { get; set; }

        public DbSet<Comentario> Comentarios { get; set; }


        public override int SaveChanges()
        {
            //Faz um foreach na classes e verifica qual classe possui uma propriedade
            //chamada DataCadastro.
            foreach (var entry in ChangeTracker.Entries().Where(entry =>
            entry.Entity.GetType().GetProperty("DataCadastro") != null))
            {
                if (entry.State == EntityState.Added)
                {  //Se estou adicionando no banco, a DataCadastro recebe DateTime.Now
                    entry.Property("DataCadastro").CurrentValue = DateTime.Now;
                }

                if (entry.State == EntityState.Modified)
                {   //Se estou efetuando um Update eu não altero a DataCadastro.
                    entry.Property("DataCadastro").IsModified = false;
                }
            }

            return base.SaveChanges();
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            //Toda string no meu sistema vai ser varchar no banco de dados.
            modelBuilder.Properties<string>()
               .Configure(p => p.HasColumnType("varchar"));

            //quando uma string não tiver maxlength, ela terá um maxlength no banco de 100.
            modelBuilder.Properties<string>()
                .Configure(p => p.HasMaxLength(100));

            base.OnModelCreating(modelBuilder);
        }

       // public System.Data.Entity.DbSet<SENAI.ExemploStartUp.UI.Site.ViewModels.Clientes.ClienteViewModel> ClienteViewModels { get; set; }

        //public System.Data.Entity.DbSet<SENAI.ExemploStartUp.UI.Site.ViewModels.Carrinho.DetalheCompraViewModel> DetalheCompraViewModels { get; set; }
    }
}
