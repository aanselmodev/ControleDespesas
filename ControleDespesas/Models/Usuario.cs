using ControleDespesas.Libraries.Mensagens;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;


namespace ControleDespesas.Models
{
    public class Usuario
    {
        [Key]
        [Display(Name = "Id")]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        
        [Display(Name = "Nome")]
        [Required(ErrorMessageResourceType = typeof(Mensagens), ErrorMessageResourceName = "MSG_E001")]
        [MinLength(4, ErrorMessageResourceType = typeof(Mensagens), ErrorMessageResourceName = "MSG_E004")]
        public string Nome { get; set; }

        [Display(Name = "Sobrenome")]
        [Required(ErrorMessageResourceType = typeof(Mensagens), ErrorMessageResourceName = "MSG_E001")]
        [MinLength(4, ErrorMessageResourceType = typeof(Mensagens), ErrorMessageResourceName = "MSG_E004")]
        public string Sobrenome { get; set; }

        [Display(Name = "Sexo")]
        [Required(ErrorMessageResourceType = typeof(Mensagens), ErrorMessageResourceName = "MSG_E001")]
        public string Sexo { get; set; }

        [Display(Name = "E-mail")]
        [Required(ErrorMessageResourceType = typeof(Mensagens), ErrorMessageResourceName = "MSG_E001")]
        [EmailAddress(ErrorMessageResourceType = typeof(Mensagens), ErrorMessageResourceName = "MSG_E002")]
        public string Email { get; set; }
        
        [Display(Name = "Senha")]
        [Required(ErrorMessageResourceType = typeof(Mensagens), ErrorMessageResourceName = "MSG_E001")]
        public string Senha { get; set; }

        [NotMapped]
        [Display(Name = "Confirmar a Senha")]
        [Required(ErrorMessageResourceType = typeof(Mensagens), ErrorMessageResourceName = "MSG_E001")]
        [Compare("Senha", ErrorMessageResourceType = typeof(Mensagens), ErrorMessageResourceName = "MSG_E005")]
        public string ConfirmarSenha { get; set; }
    }
}
