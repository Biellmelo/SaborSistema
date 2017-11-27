using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SENAI.ExemploStartUp.Models.Models
{   [Table("Categorias")]
    public class Categoria
    {
        public Categoria()
        {
            CategoriaId = Guid.NewGuid();
        }

        [Key]
        public Guid CategoriaId { get; set; }

        [StringLength(50, MinimumLength = 2)]
        [Required]
        public string Descricao { get; set; }

        public virtual List<Produto> Produtos { get; set; }
    }
}
