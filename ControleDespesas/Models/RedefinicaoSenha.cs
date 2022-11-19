using ControleDespesas.Libraries.Mensagens;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ControleDespesas.Models
{
    public class RedefinicaoSenha
    {
        [Display(Name = "Id")]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required(ErrorMessageResourceType = typeof(Mensagens), ErrorMessageResourceName = "MSG_E001")]
        [Display(Name = "Código")]
        public string Codigo { get; set; }

        [Required(ErrorMessageResourceType = typeof(Mensagens), ErrorMessageResourceName = "MSG_E001")]
        public string Senha { get; set; }

        [NotMapped]
        [Display(Name = "Confirme a nova senha")]
        [Compare("Senha", ErrorMessageResourceType = typeof(Mensagens), ErrorMessageResourceName = "MSG_E005")]
        [MinLength(8, ErrorMessageResourceType = typeof(Mensagens), ErrorMessageResourceName = "MSG_E004")]
        [MaxLength(12, ErrorMessageResourceType = typeof(Mensagens), ErrorMessageResourceName = "MSG_E003")]
        public string ConfirmarSenha { get; set; }

        [Display(Name = "Id do Usuário")]
        public int IdUsuario { get; set; }

        [Display(Name = "Data de Expiração")]
        public DateTime DataExpiracao { get; set; }

    }
}
