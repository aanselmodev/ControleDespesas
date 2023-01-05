using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using ExpensesControl.Libraries;
using Microsoft.EntityFrameworkCore;

namespace ExpensesControl.Models
{
    public class User
    {
        [Key]
        [Display(Name = "id")]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        
        public int Id { get; set; }
        
        [Display(Name = "nome")]
        [Required(ErrorMessageResourceType = typeof(Messages), ErrorMessageResourceName = "MSG_E001")]
        [MinLength(4, ErrorMessageResourceType = typeof(Messages), ErrorMessageResourceName = "MSG_E004")]
        public string Name { get; set; }

        [Display(Name = "sobrenome")]
        [Required(ErrorMessageResourceType = typeof(Messages), ErrorMessageResourceName = "MSG_E001")]
        [MinLength(4, ErrorMessageResourceType = typeof(Messages), ErrorMessageResourceName = "MSG_E004")]
        public string LastName { get; set; }

        [Display(Name = "sexo")]
        [Required(ErrorMessageResourceType = typeof(Messages), ErrorMessageResourceName = "MSG_E001")]
        public string Gender { get; set; }

        [Display(Name = "e-mail")]
        [Required(ErrorMessageResourceType = typeof(Messages), ErrorMessageResourceName = "MSG_E001")]
        [EmailAddress(ErrorMessageResourceType = typeof(Messages), ErrorMessageResourceName = "MSG_E002")]
        public string Email { get; set; }
        
        [Display(Name = "senha")]
        [Required(ErrorMessageResourceType = typeof(Messages), ErrorMessageResourceName = "MSG_E001")]
        [MinLength(8, ErrorMessageResourceType = typeof(Messages), ErrorMessageResourceName = "MSG_E004")]
        [MaxLength(16, ErrorMessageResourceType = typeof(Messages), ErrorMessageResourceName = "MSG_E003")]
        public string Password { get; set; }
       
        [NotMapped]
        [JsonIgnore]
        [Display(Name = "confirmar a senha")]
        [Required(ErrorMessageResourceType = typeof(Messages), ErrorMessageResourceName = "MSG_E001")]
        [Compare(nameof(Password), ErrorMessageResourceType = typeof(Messages), ErrorMessageResourceName = "MSG_E005")]
        public string ConfirmPassword { get; set; }

        [JsonIgnore]
        [Display(Name = "tipo")]
        public UserType Type { get; set; }

        [Display(Name = "status")]
        public UserStatus Status { get; set; }

        [Display(Name = "data de cadastro")]
        public DateTime RegistrationDate { get; set; }
    }
}
