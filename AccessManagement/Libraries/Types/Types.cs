using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AccessManagement.Libraries
{
    public enum UserType
    {
        System = 0,
        UserDefault = 1,
        Manager = 2,
        Customer = 3,
        Employee = 4
    }

    public enum UserStatus
    {
        Inactive = 0,
        Active = 1,
        Pending = 2,
        Blocked = 3
    }

    public enum OrdinationType
    {
        AscendingOrder = 0,
        DescendingOrder = 1,
        ActivesOnTop = 2,
        InactivesOnTop = 3
    }
}
