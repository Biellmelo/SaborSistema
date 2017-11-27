using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SENAI.ExemploStartUp.Models.Models
{
    [Table("Admins")]
    public class Admin
    {
        public Admin()
        {
            AdminId = Guid.NewGuid();
            Permissao = "ADMIN";
        }

        [Key]
        public Guid AdminId { get; set; }

        [StringLength(50,MinimumLength = 5)]
        [Column(TypeName = "varchar")]
        [Required]
        public string Email { get; set; }

        [StringLength(100, MinimumLength = 5)]
        [Column(TypeName = "varchar")]
        [Required]
        public string Senha { get; set; }


        public string Permissao { get; set; }

    }
}
