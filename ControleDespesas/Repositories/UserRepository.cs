using AccessManagement.Database;
using AccessManagement.Models;
using AccessManagement.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AccessManagement.Repositories
{
    public class UserRepository : IUserRepository
    {
        private AccessManagementDbContext _db;

        public UserRepository(AccessManagementDbContext db)
        {
            _db = db;
        }

        public void Update(User user)
        {
            _db.Update(user);
            _db.SaveChanges();
        }

        public void UpdateRegistrationData(User user)
        {
            _db.Update(user);
            _db.Entry(user).Property(x => x.Password).IsModified = false;
            _db.SaveChanges();
        }

        public void UpdatePassword(User user)
        {
            _db.Update(user);
            _db.Entry(user).Property(x => x.Name).IsModified = false;
            _db.Entry(user).Property(x => x.Gender).IsModified = false;
            _db.Entry(user).Property(x => x.LastName).IsModified = false;
            _db.Entry(user).Property(x => x.Email).IsModified = false;
            _db.Entry(user).Property(x => x.Active).IsModified = false;
            _db.SaveChanges();
        }

        public void Create(User user)
        {
            _db.Add(user);
            _db.SaveChanges();
        }

        public User Read(int id)
        {
           return _db.Users.Find(id);
        }

        public User ReadByEmail(string email)
        {
            return _db.Users.Where(a => a.Email == email).FirstOrDefault();
        }

        public void Delete(int id)
        {
            User user = Read(id);
            _db.Remove(user);
            _db.SaveChanges();
        }

        public User Login(string email, string password)
        {
            return _db.Users.Where(u => u.Email == email && u.Password == password).FirstOrDefault();
        }

        public void UpdateUserStatus(int id, bool active)
        {
            User user = Read(id);
            user.Active = active;

            Update(user);
        }
    }
}
