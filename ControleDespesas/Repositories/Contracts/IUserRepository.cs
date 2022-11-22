using AccessManagement.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AccessManagement.Repositories
{
    public interface IUserRepository
    {
        public void Create(User user);
        public User Read(int id);
        public User ReadByEmail(string email);
        public void Update(User user);
        public void UpdateRegistrationData(User user);
        public void UpdatePassword(User user);
        public void Delete(int id);
        public User Login(string email, string password);
        public void UpdateUserStatus(int id, bool active);
    }
}
