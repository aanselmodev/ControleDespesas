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
        [Display(Name = "Sistema")]
        System = 0,
        [Display(Name = "Usuário")]
        UserDefault = 1
    }

    public enum UserStatus
    {
        [Display(Name = "Inativo")]
        Inactive = 0,
        [Display(Name = "Ativo")]
        Active = 1,
        [Display(Name = "Pendente")]
        Pending = 2,
        [Display(Name = "Bloqueado")]
        Blocked = 3
    }

    public enum OrdinationType
    {
        [Display(Name = "Ordem crescente")]
        AscendingOrder = 0,
        [Display(Name = "Ordem decrescente")]
        DescendingOrder = 1,
        [Display(Name = "Ativos")]
        ActiveOnTop = 2,
        [Display(Name = "Inativos")]
        InactiveOnTop = 3,
        [Display(Name = "Pendentes")]
        PendingOnTop = 4,
        [Display(Name = "Bloqueados")]
        BlockedOnTop = 5
    }

    public enum SearchTypeUser
    { 
        [Display(Name = "Id")]
        Id = 0,
        [Display(Name = "Email")]
        Email = 1,
        [Display(Name = "Nome")]
        Name = 2,
        [Display(Name = "Sobrenome")]
        LastName = 3
    }

}   
