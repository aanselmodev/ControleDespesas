using ControleDespesas.Libraries.Mensagens;
using ControleDespesas.Libraries.Tipos;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ControleDespesas.Models
{
    public class Despesa
    {
        [Key]
        [Display(Name = "Id")]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required(ErrorMessageResourceType = typeof(Mensagens), ErrorMessageResourceName = "MSG_E001")]
        [MinLength(4, ErrorMessageResourceType = typeof(Mensagens), ErrorMessageResourceName = "MSG_E004")]
        [Display(Name = "Nome")]
        public string Nome { get; set; }

        [Required(ErrorMessageResourceType = typeof(Mensagens), ErrorMessageResourceName = "MSG_E001")]
        [Display(Name = "Tipo")]
        public int TipoDespesaId { get; set; }

        [ForeignKey("TipoDespesaId")]
        public virtual TipoDespesa Tipo { get; set; }

        [Required(ErrorMessageResourceType = typeof(Mensagens), ErrorMessageResourceName = "MSG_E001")]
        [Display(Name = "Valor total")]
        public double ValorTotal { get; set; }

        [Required(ErrorMessageResourceType = typeof(Mensagens), ErrorMessageResourceName = "MSG_E001")]
        [Display(Name = "Saldo")]
        public double Saldo { get; set; }

        [Required(ErrorMessageResourceType = typeof(Mensagens), ErrorMessageResourceName = "MSG_E001")]
        [Display(Name = "Quantidade de parcelas")]
        public int QuantidadeParcelas { get; set; }

        [Required(ErrorMessageResourceType = typeof(Mensagens), ErrorMessageResourceName = "MSG_E001")]
        [Display(Name = "Quantidade de parcelas pagas")]
        public int QuantidadeParcelasPagas { get; set; }

        [Required(ErrorMessageResourceType = typeof(Mensagens), ErrorMessageResourceName = "MSG_E001")]
        [Display(Name = "Quantidade de parcelas restantes")]
        public int QuantidadeParcelasRestantes { get; set; }

        [Required(ErrorMessageResourceType = typeof(Mensagens), ErrorMessageResourceName = "MSG_E001")]
        [Display(Name = "Dia de vencimento")]
        public int DiaVencimento { get; set; }

        [Required(ErrorMessageResourceType = typeof(Mensagens), ErrorMessageResourceName = "MSG_E001")]
        [Display(Name = "Data de ínicio")]
        public DateTime DataInicio { get; set; }

        [Required(ErrorMessageResourceType = typeof(Mensagens), ErrorMessageResourceName = "MSG_E001")]
        [Display(Name = "Data final")]
        public DateTime DataFinal { get; set; }

    }
}
