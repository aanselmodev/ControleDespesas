using ExpensesControl.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ExpensesControl.Repositories.Contracts
{
    public interface IPasswordResetRepository
    {
        public void Create(PasswordReset passwordReset);
        public void DeleteAllByUserId(long id);
        public PasswordReset ReadByUserId(long id);
        public PasswordReset ReadByCode(string code);

    }
}
