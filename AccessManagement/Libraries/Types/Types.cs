using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AccessManagement.Libraries
{
    public enum UserType
    {
        [Display(Name = "Sistema")]
        System = 0,
        [Display(Name = "Usuário")]
        UserDefault = 1,
        [Display(Name = "Gerente")]
        Manager = 2,
        [Display(Name = "Cliente")]
        Customer = 3,
        [Display(Name = "Funcionário")]
        Employee = 4
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
        ActivesOnTop = 2,
        [Display(Name = "Inativos")]
        InactivesOnTop = 3
    }

    public enum SearchTypeUser
    { 
        [Display(Name = "Id")]
        Id = 0,
        [Display(Name = "Email")]
        Email = 1
    }
}
