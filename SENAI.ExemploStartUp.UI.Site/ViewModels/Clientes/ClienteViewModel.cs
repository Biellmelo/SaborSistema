using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SENAI.ExemploStartUp.UI.Site.ViewModels.Clientes
{
    public class ClienteViewModel
    {
        public ClienteViewModel()
        {
            ClienteId = Guid.NewGuid();
        }

        [Key]
        public Guid ClienteId { get; set; }

        [StringLength(50, MinimumLength = 2)]
        [Required(ErrorMessage ="O Nome é obrigatório.")]
        [DisplayName("Nome: ")]
        public string Nome { get; set; }

        [StringLength(14, MinimumLength = 11)]
        [Required(ErrorMessage = "O CPF é obrigatório.")]
        [DisplayName("CPF: ")]
        public string CPF { get; set; }

        [StringLength(50, MinimumLength = 5)]
        [Required(ErrorMessage = "O E-mail é obrigatório.")]
        [DisplayName("E-mail: ")]
        [EmailAddress(ErrorMessage = "E-mail em formato inválido.")]
        public string Email { get; set; }

        [DataType(DataType.Date)]
        [Required(ErrorMessage = "A Data de Nascimento é obrigatória.")]
        [DisplayName("Data de Nascimento: ")]
        public DateTime DataNascimento { get; set; }


        [DataType(DataType.Date)]
        [ScaffoldColumn(false)]
        public DateTime DataCadastro { get; set; }

        [Required]
        [DisplayName("Deseja inativar este Cliente?")]
        public bool Ativo { get; set; }

        [Required]
        [DisplayName("Deseja Exluir este Cliente?")]
        public bool Excluido { get; set; }

        [Required(ErrorMessage = "A Senha é obrigatória.")]
        [StringLength(10, MinimumLength = 2)]
        [DataType(DataType.Password)] //Senha não ficará visível.
        [DisplayName("Senha do Cliente:")] // Texto que aparecerá em tela.
        public string Senha { get; set; }

        [Required(ErrorMessage = "A Confirmação de Senha é obrigatória.")]
        [StringLength(10, MinimumLength = 2)]
        [DataType(DataType.Password)] //Senha não ficará visível.
        [DisplayName("Confirme a Senha do Cliente:")] // Texto que aparecerá em tela.
        [Compare("Senha",ErrorMessage = "As senhas não conferem.")]
        public string ConfirmaSenha { get; set; }

        [StringLength(15, MinimumLength = 2)]
        [ScaffoldColumn(false)]
        public string Role { get; set; }
    }
}