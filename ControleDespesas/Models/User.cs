using AccessManagement.Libraries;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace AccessManagement.Models
{
    public class User
    {
        private string _password;
        private string _confirmPassword;

        [Key]
        [Display(Name = "Id")]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        
        [Display(Name = "Nome")]
        [Required(ErrorMessageResourceType = typeof(Messages), ErrorMessageResourceName = "MSG_E001")]
        [MinLength(4, ErrorMessageResourceType = typeof(Messages), ErrorMessageResourceName = "MSG_E004")]
        public string Name { get; set; }

        [Display(Name = "Sobrenome")]
        [Required(ErrorMessageResourceType = typeof(Messages), ErrorMessageResourceName = "MSG_E001")]
        [MinLength(4, ErrorMessageResourceType = typeof(Messages), ErrorMessageResourceName = "MSG_E004")]
        public string LastName { get; set; }

        [Display(Name = "Sexo")]
        [Required(ErrorMessageResourceType = typeof(Messages), ErrorMessageResourceName = "MSG_E001")]
        public string Gender { get; set; }

        [Display(Name = "E-mail")]
        [Required(ErrorMessageResourceType = typeof(Messages), ErrorMessageResourceName = "MSG_E001")]
        [EmailAddress(ErrorMessageResourceType = typeof(Messages), ErrorMessageResourceName = "MSG_E002")]
        public string Email { get; set; }
        
        [Required(ErrorMessageResourceType = typeof(Messages), ErrorMessageResourceName = "MSG_E001")]
        [MinLength(8, ErrorMessageResourceType = typeof(Messages), ErrorMessageResourceName = "MSG_E004")]
        [MaxLength(12, ErrorMessageResourceType = typeof(Messages), ErrorMessageResourceName = "MSG_E003")]
        public string Password
        {
            get => _password;
            set => _password = AccessManagement.Libraries.PasswordManagement.Encrypt(value);
        }

        [NotMapped]
        [JsonIgnore]
        [Display(Name = "Confirmar a Senha")]
        [Required(ErrorMessageResourceType = typeof(Messages), ErrorMessageResourceName = "MSG_E001")]
        [Compare("Password", ErrorMessageResourceType = typeof(Messages), ErrorMessageResourceName = "MSG_E005")]
        #nullable enable
        public string? ConfirmPassword
        {
            get => _confirmPassword;
            set => _confirmPassword = AccessManagement.Libraries.PasswordManagement.Encrypt(value);
        }

        [Display(Name = "Ativo")]
        public bool Active { get; set; }
    }
}
