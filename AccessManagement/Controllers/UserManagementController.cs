using AccessManagement.Models;
using AccessManagement.Repositories;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using X.PagedList;

namespace AccessManagement.Controllers
{
    public class UserManagementController : Controller
    {
        private IUserRepository _user;

        public UserManagementController(IUserRepository user)
        {
            _user = user;
        }

        [HttpGet]
        public IActionResult Index(int page)
        {
            IPagedList<User> usersPagedList = _user.ReadAllUsers(page, string.Empty, Libraries.OrdinationType.AscendingOrder);

            return View(usersPagedList);
        }
    }
}
