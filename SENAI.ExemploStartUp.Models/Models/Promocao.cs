using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SENAI.ExemploStartUp.Models.Models
{
    [Table("Promocoes")]
    public class Promocao
    {
        public Promocao()
        {
            PromocaoID = Guid.NewGuid();
        }
        public Guid PromocaoID { get; set; }

        public string Descricao { get; set; }

        [Required]
        public string DiaSemana { get; set; }

        public decimal Desconto { get; set; }

        [Required]
        public Guid ProdutoID { get; set; }

        public virtual Produto Produto { get; set; }
    }
}