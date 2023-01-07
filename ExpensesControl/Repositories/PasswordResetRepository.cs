using ExpensesControl.Database;
using ExpensesControl.Models;
using ExpensesControl.Repositories.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ExpensesControl.Repositories
{
    public class PasswordResetRepository : IPasswordResetRepository
    {
        private ExpensesControlDbContext _db;

        public PasswordResetRepository(ExpensesControlDbContext db)
        {
            _db = db;
        }

        public void Create(PasswordReset passwordReset)
        {
            _db.Add(passwordReset);
            _db.SaveChanges();
        }

        public PasswordReset ReadByCode(string code)
        {
            return _db.PasswordReset.Where(x => x.Code == code).FirstOrDefault();
        }

        public PasswordReset ReadByUserId(long id)
        {
            return _db.PasswordReset.Where(x => x.UserId == id).FirstOrDefault();
        }

        public void DeleteAllByUserId(long id)
        {
            IQueryable<PasswordReset> list = _db.PasswordReset.Where(x => x.UserId == id);
            _db.PasswordReset.RemoveRange(list);
            _db.SaveChanges();
        }

    }
}
