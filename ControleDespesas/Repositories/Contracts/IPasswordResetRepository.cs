using AccessManagement.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AccessManagement.Repositories.Contracts
{
    public interface IPasswordResetRepository
    {
        public void Create(PasswordReset passwordReset);
        public void DeleteAllByUserId(int id);
        public PasswordReset ReadByUserId(int id);
        public PasswordReset ReadByCode(string code);

    }
}
