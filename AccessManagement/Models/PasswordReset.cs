using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using AccessManagement.Libraries;

namespace AccessManagement.Models
{
    public class PasswordReset
    {
        private string _password;
        private string _confirmPassword;

        [Display(Name = "Id")]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required(ErrorMessageResourceType = typeof(Messages), ErrorMessageResourceName = "MSG_E001")]
        [Display(Name = "Código")]
        public string Code { get; set; }

        [Required(ErrorMessageResourceType = typeof(Messages), ErrorMessageResourceName = "MSG_E001")]
        public string Password
        {
            get => _password;
            set => _password = AccessManagement.Libraries.PasswordManagement.Encrypt(value);
        }

        [NotMapped]
        [Display(Name = "Confirme a nova senha")]
        [Compare("Password", ErrorMessageResourceType = typeof(Messages), ErrorMessageResourceName = "MSG_E005")]
        [MinLength(8, ErrorMessageResourceType = typeof(Messages), ErrorMessageResourceName = "MSG_E004")]
        [MaxLength(12, ErrorMessageResourceType = typeof(Messages), ErrorMessageResourceName = "MSG_E003")]
        public string ConfirmPassword
        {
            get => _confirmPassword;
            set => _confirmPassword = AccessManagement.Libraries.PasswordManagement.Encrypt(value);
        }

        [Display(Name = "Id do Usuário")]
        public int UserId { get; set; }

        [Display(Name = "Data de Expiração")]
        public DateTime ExpirationDate { get; set; }

    }
}
