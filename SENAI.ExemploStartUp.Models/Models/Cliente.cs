using SENAI.ExemploStartUp.Models.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SENAI.ExemploStartUp.Models.Models
{
    [Table("Clientes2")]
    public class Cliente
    {
        public Cliente()
        {
            ClienteId = Guid.NewGuid();
            Permissao = "CLIENTE";
        }

        [Key]
        [Column("Id")]
        public Guid ClienteId { get; set; }

        [StringLength(50, MinimumLength = 2)]
        [Required]
        public string Nome { get; set; }

        [StringLength(11, MinimumLength = 11)]
        [Required]
        public string CPF { get; set; }

        [StringLength(50, MinimumLength = 5)]
        [Required]
        public string Email { get; set; }

        [DataType(DataType.Date)]
        [Required]
        public DateTime DataNascimento { get; set; }

        [Required]
        [DataType(DataType.Date)]
        public DateTime DataCadastro { get; set; }

        [Required]
        public bool Ativo { get; set; }

        [Required]
        public bool Excluido { get; set; }

        [Required]
        [StringLength(250)]
        public string Senha { get; set; }

        [Required]
        [StringLength(15, MinimumLength = 2)]
        public string Permissao { get; set; }

        public virtual List<Endereco> Enderecos { get; set; }
    }
}
