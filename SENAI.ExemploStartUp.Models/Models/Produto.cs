using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SENAI.ExemploStartUp.Models.Models
{
    [Table("Produtos")]
    public class Produto
    {
        public Produto()
        {
            ProdutoId = Guid.NewGuid();
        }

        [Key]
        public Guid ProdutoId { get; set; }

        [StringLength(50, MinimumLength = 2)]
        [Required]
        public string Nome { get; set; }

        [Required]
        public double Preco { get; set; }

        [Required]
        public int Quantidade { get; set; }

        public string Imagem { get; set; }

        [Required]
        public bool Ativo { get; set; }

        [StringLength(500)]
        public string Descricao { get; set; }

        [Required]
        public Guid CategoriaId { get; set; }
        public virtual Categoria Categoria { get; set; }

        public virtual List<Promocao> Promocao { get; set; }
    }
}
