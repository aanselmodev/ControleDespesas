using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using ControleDespesas.Libraries.Mensagens;

namespace ControleDespesas.Libraries.Tipos
{
    public class TipoDespesa
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Display(Name = "Id")]
        public int Id { get; set; }

        [Required(ErrorMessageResourceType = typeof(Mensagens.Mensagens), ErrorMessageResourceName = "MSG_E001")]
        [Display(Name = "Nome")]
        public string Nome { get; set; }
    }
}
