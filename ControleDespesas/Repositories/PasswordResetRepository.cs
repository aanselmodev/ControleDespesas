using AccessManagement.Database;
using AccessManagement.Models;
using AccessManagement.Repositories.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AccessManagement.Repositories
{
    public class PasswordResetRepository : IPasswordResetRepository
    {
        private AccessManagementDbContext _db;

        public PasswordResetRepository(AccessManagementDbContext db)
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

        public PasswordReset ReadByUserId(int id)
        {
            return _db.PasswordReset.Where(x => x.UserId == id).FirstOrDefault();
        }

        public void DeleteAllByUserId(int id)
        {
            IQueryable<PasswordReset> list = _db.PasswordReset.Where(x => x.UserId == id);
            _db.PasswordReset.RemoveRange(list);
            _db.SaveChanges();
        }

    }
}
