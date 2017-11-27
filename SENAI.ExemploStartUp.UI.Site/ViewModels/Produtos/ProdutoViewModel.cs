using SENAI.ExemploStartUp.Models.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SENAI.ExemploStartUp.UI.Site.ViewModels.Produtos
{
    public class ProdutoViewModel
    {
        public ProdutoViewModel()
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

        public HttpPostedFileBase Imagem { get; set; }

        [Required]
        [StringLength(500)]
        public string Descricao { get; set; }

        [Required]
        public bool Ativo { get; set; }

        [Required]
        public Guid CategoriaId { get; set; }
        public virtual Categoria Categoria { get; set; }
    }
}