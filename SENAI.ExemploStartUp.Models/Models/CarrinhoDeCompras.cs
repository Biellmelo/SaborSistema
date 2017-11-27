using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SENAI.ExemploStartUp.Models.Models
{
    [Table("CarrinhosDeCompras")]
    public class CarrinhoDeCompras
    {
        public CarrinhoDeCompras()
        {
            CarrinhoDeComprasId = Guid.NewGuid();
        }

        [Key]
        public Guid CarrinhoDeComprasId { get; set; }

        // 0 - Ativo
        //1 - Concluido
        [Required]
        public int StatusId { get; set; }

        [Required]
        [DataType(DataType.Date)]
        public DateTime DataCadastro { get; set; }

        [DataType(DataType.Date)]
        public DateTime DataCompra { get; set; }

        [Required]
        public double ValorTotal { get; set; }

        public virtual List<ItemCarrinho> ItensCarrinho { get; set; }

        [Required]
        public Guid ClienteId { get; set; }
        public virtual Cliente Cliente { get; set; }

    }
}
