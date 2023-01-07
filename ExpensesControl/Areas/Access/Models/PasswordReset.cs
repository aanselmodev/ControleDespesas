using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using ExpensesControl.Libraries;

namespace ExpensesControl.Models
{
    public class PasswordReset
    {
        [Display(Name = "Id")]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { get; set; }

        [Required(ErrorMessageResourceType = typeof(Messages), ErrorMessageResourceName = "MSG_E001")]
        [Display(Name = "Código")]
        public string Code { get; set; }

        [Display(Name = "Senha")]
        [MinLength(8, ErrorMessageResourceType = typeof(Messages), ErrorMessageResourceName = "MSG_E004")]
        [MaxLength(16, ErrorMessageResourceType = typeof(Messages), ErrorMessageResourceName = "MSG_E003")]
        [Required(ErrorMessageResourceType = typeof(Messages), ErrorMessageResourceName = "MSG_E001")]
        public string Password { get; set; }

        [NotMapped]
        [Display(Name = "Confirme a nova senha")]
        [MinLength(8, ErrorMessageResourceType = typeof(Messages), ErrorMessageResourceName = "MSG_E004")]
        [MaxLength(16, ErrorMessageResourceType = typeof(Messages), ErrorMessageResourceName = "MSG_E003")]
        [Compare(nameof(Password), ErrorMessageResourceType = typeof(Messages), ErrorMessageResourceName = "MSG_E005")]
        [Required(ErrorMessageResourceType = typeof(Messages), ErrorMessageResourceName = "MSG_E001")]
        public string ConfirmPassword { get; set; }

        [Display(Name = "Id do Usuário")]
        public long UserId { get; set; }

        [Display(Name = "Data de Expiração")]
        public DateTime ExpirationDate { get; set; }

    }
}
