using ExpensesControl.Models;
using X.PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ExpensesControl.Libraries;

namespace ExpensesControl.Repositories
{
    public interface IUserRepository
    {
        public void Create(User user);
        public User Read(int id);
        public IPagedList<User> ReadAllUsers(int? page, SearchTypeUser searchTypeUser, string nameSearchField, OrdinationType ordination);
        public User ReadByEmail(string email);
        public void Update(User user);
        public void UpdateRegistrationData(User user);
        public void UpdatePassword(User user);
        public void Delete(int id);
        public User Login(string email, string password);
        public void UpdateUserStatus(int id, UserStatus status);
        public void UpdateEmail(User user);
    }
}
