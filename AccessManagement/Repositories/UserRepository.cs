using AccessManagement.Database;
using AccessManagement.Libraries;
using AccessManagement.Models;
using AccessManagement.Repositories;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using X.PagedList;

namespace AccessManagement.Repositories
{
    public class UserRepository : IUserRepository
    {
        private AccessManagementDbContext _db;
        private IConfiguration _config;

        public UserRepository(AccessManagementDbContext db, IConfiguration config)
        {
            _db = db;
            _config = config;
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
            _db.Entry(user).Property(x => x.Status).IsModified = false;
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

        public IPagedList<User> ReadAllUsers(int? page, SearchTypeUser searchTypeUser, string searchValue, OrdinationType ordination)
        {
            int quantityPerPage = _config.GetValue<int>("QuantityPerPage");
            int currentPage = page ?? 1;

            var dbUsers = _db.Users.OrderBy(x => x.Id).AsQueryable();

            if (!string.IsNullOrWhiteSpace(searchValue))
            {
                switch (searchTypeUser)
                {
                    case SearchTypeUser.Id:
                        dbUsers = dbUsers.Where(x => x.Id.ToString().Contains(searchValue.Trim()));
                        break;
                    case SearchTypeUser.Email:
                        dbUsers = dbUsers.Where(x => x.Email.Contains(searchValue.Trim()));
                        break;
                    default:
                        break;
                } 
            }

            switch (ordination)
            {
                case OrdinationType.AscendingOrder:
                    switch (searchTypeUser)
                    {
                        case SearchTypeUser.Id:
                            dbUsers = dbUsers.OrderBy(x => x.Id);
                            break;
                        case SearchTypeUser.Email:
                            dbUsers = dbUsers.OrderBy(x => x.Email);
                            break;
                        default:
                            break;
                    }
                    break;
                case OrdinationType.DescendingOrder:
                    switch (searchTypeUser)
                    {
                        case SearchTypeUser.Id:
                            dbUsers = dbUsers.OrderByDescending(x => x.Id);
                            break;
                        case SearchTypeUser.Email:
                            dbUsers = dbUsers.OrderByDescending(x => x.Email);
                            break;
                        default:
                            break;
                    }
                    break;
                case OrdinationType.ActivesOnTop:
                    dbUsers = dbUsers.OrderByDescending(x => x.Status == UserStatus.Active);
                    break;
                case OrdinationType.InactivesOnTop:
                    dbUsers = dbUsers.OrderByDescending(x => x.Status == UserStatus.Inactive);
                    break;
                default:
                    break;
            }

            return dbUsers.ToPagedList<User>(currentPage, quantityPerPage);
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

        public void UpdateUserStatus(int id, UserStatus status)
        {
            User user = Read(id);
            user.Status = status;

            Update(user);
        }
    }
}
