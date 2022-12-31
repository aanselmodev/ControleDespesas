using ExpensesControl.Database;
using ExpensesControl.Libraries;
using ExpensesControl.Models;
using ExpensesControl.Repositories;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using X.PagedList;

namespace ExpensesControl.Repositories
{
    public class UserRepository : IUserRepository
    {
        private ExpensesControlDbContext _db;
        private IConfiguration _config;

        public UserRepository(ExpensesControlDbContext db, IConfiguration config)
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
            user.Password = Cryptography.Encrypt(user.Password);

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
            user.Password = Cryptography.Encrypt(user.Password);

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

            var dbUsers = _db.Users.AsQueryable();

            if (!string.IsNullOrWhiteSpace(searchValue))
            {
                switch (searchTypeUser)
                {
                    case SearchTypeUser.Id:
                        dbUsers = _db.Users.Where(x => x.Id.ToString().Contains(searchValue.Trim()));
                        
                        switch (ordination)
                        {
                            case OrdinationType.AscendingOrder:
                                dbUsers = dbUsers.OrderBy(x => x.Id);
                                break;
                            case OrdinationType.DescendingOrder:
                                dbUsers = dbUsers.OrderByDescending(x => x.Id);
                                break;
                            default:
                                break;
                        }
                        
                        break;
                    case SearchTypeUser.Email:
                        dbUsers = _db.Users.Where(x => x.Email.Contains(searchValue.Trim()));
                        
                        switch (ordination)
                        {
                            case OrdinationType.AscendingOrder:
                                dbUsers = dbUsers.OrderBy(x => x.Email);
                                break;
                            case OrdinationType.DescendingOrder:
                                dbUsers = dbUsers.OrderByDescending(x => x.Email);
                                break;
                            default:
                                break;
                        }
                        
                        break;
                    case SearchTypeUser.Name:
                        dbUsers = _db.Users.Where(x => x.Name.Contains(searchValue.Trim()));

                        switch (ordination)
                        {
                            case OrdinationType.AscendingOrder:
                                dbUsers = dbUsers.OrderBy(x => x.Name);
                                break;
                            case OrdinationType.DescendingOrder:
                                dbUsers = dbUsers.OrderByDescending(x => x.Name);
                                break;
                            default:
                                break;
                        }

                        break;
                    case SearchTypeUser.LastName:
                        dbUsers = _db.Users.Where(x => x.LastName.Contains(searchValue.Trim()));

                        switch (ordination)
                        {
                            case OrdinationType.AscendingOrder:
                                dbUsers = dbUsers.OrderBy(x => x.LastName);
                                break;
                            case OrdinationType.DescendingOrder:
                                dbUsers = dbUsers.OrderByDescending(x => x.LastName);
                                break;
                            default:
                                break;
                        }

                        break;
                    default:
                        break;
                }

                switch (ordination)
                {
                    case OrdinationType.ActiveOnTop:
                        dbUsers = dbUsers.OrderByDescending(x => x.Status == UserStatus.Active);
                        break;
                    case OrdinationType.InactiveOnTop:
                        dbUsers = dbUsers.OrderByDescending(x => x.Status == UserStatus.Inactive);
                        break;
                    case OrdinationType.PendingOnTop:
                        dbUsers = dbUsers.OrderByDescending(x => x.Status == UserStatus.Pending);
                        break;
                    case OrdinationType.BlockedOnTop:
                        dbUsers = dbUsers.OrderByDescending(x => x.Status == UserStatus.Blocked);
                        break;
                    default:
                        break;
                }
            }
            else
            {
                switch (ordination)
                {
                    case OrdinationType.AscendingOrder:
                        dbUsers = dbUsers.OrderBy(x => x.Id);
                        break;
                    case OrdinationType.DescendingOrder:
                        dbUsers = dbUsers.OrderByDescending(x => x.Id);
                        break;
                    case OrdinationType.ActiveOnTop:
                        dbUsers = dbUsers.OrderByDescending(x => x.Status == UserStatus.Active);
                        break;
                    case OrdinationType.InactiveOnTop:
                        dbUsers = dbUsers.OrderByDescending(x => x.Status == UserStatus.Inactive);
                        break;
                    case OrdinationType.PendingOnTop:
                        dbUsers = dbUsers.OrderByDescending(x => x.Status == UserStatus.Pending);
                        break;
                    case OrdinationType.BlockedOnTop:
                        dbUsers = dbUsers.OrderByDescending(x => x.Status == UserStatus.Blocked);
                        break;
                    default:
                        break;
                }
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
            return _db.Users.Where(u => u.Email == email && u.Password == Cryptography.Encrypt(password)).FirstOrDefault();
        }

        public void UpdateUserStatus(int id, UserStatus status)
        {
            User user = Read(id);
            user.Status = status;

            Update(user);
        }
    }
}
