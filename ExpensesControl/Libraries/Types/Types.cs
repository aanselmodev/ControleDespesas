using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ExpensesControl.Libraries
{
    public enum UserType
    {
        [Display(Name = "sistema")]
        System = 0,
        [Display(Name = "usuário")]
        UserDefault = 1
    }

    public enum UserStatus
    {
        [Display(Name = "inativo")]
        Inactive = 0,
        [Display(Name = "ativo")]
        Active = 1,
        [Display(Name = "pendente")]
        Pending = 2,
        [Display(Name = "bloqueado")]
        Blocked = 3
    }

    public enum OrdinationType
    {
        [Display(Name = "ordem crescente")]
        AscendingOrder = 0,
        [Display(Name = "ordem decrescente")]
        DescendingOrder = 1,
        [Display(Name = "ativos")]
        ActiveOnTop = 2,
        [Display(Name = "inativos")]
        InactiveOnTop = 3,
        [Display(Name = "pendentes")]
        PendingOnTop = 4,
        [Display(Name = "bloqueados")]
        BlockedOnTop = 5
    }

    public enum SearchTypeUser
    { 
        [Display(Name = "id")]
        Id = 0,
        [Display(Name = "e-mail")]
        Email = 1,
        [Display(Name = "nome")]
        Name = 2,
        [Display(Name = "sobrenome")]
        LastName = 3
    }

}   
