using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SENAI.ExemploStartUp.Models.Models
{
    [Table("ItensCarrinho")]
    public class ItemCarrinho
    {
        public ItemCarrinho()
        {
            ItemCarrinhoId = Guid.NewGuid();
        }

        [Key]
        public Guid ItemCarrinhoId { get; set; }

        [Required]
        public int Quantidade { get; set; }

        [Required]
        public Guid CarrinhoDeComprasId { get; set; }
        public virtual CarrinhoDeCompras CarrinhoDeCompras { get; set; }

        [Required]
        public Guid ProdutoId { get; set; }
        public virtual Produto Produto { get; set; }

    }
}