using AccessManagement.Models;
using AccessManagement.Repositories;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using X.PagedList;
using AccessManagement.Libraries;

namespace AccessManagement.Controllers
{
    public class UserManagementController : Controller
    {
        private IUserRepository _user;

        public UserManagementController(IUserRepository user)
        {
            _user = user;
        }

        public IActionResult Index(int? page, string inSearchValue, SearchTypeUser inSearchTypeUser, OrdinationType inOrdinationType)
        { 
            IPagedList<User> usersPagedList = _user.ReadAllUsers(page, inSearchTypeUser, inSearchValue, inOrdinationType);

            TempData["inSearchValue"] = inSearchValue;
            TempData["inSearchTypeUser"] = inSearchTypeUser;
            TempData["OrdinationType"] = inOrdinationType;

            return View(usersPagedList);
        }

        [HttpGet]
        public IActionResult UpdateRegistrationData(int id)
        {
            User user = _user.Read(id);

            return View(user);
        }

        [HttpPost]
        public IActionResult UpdateRegistrationData([FromForm] User inUser)
        {
            ModelState.Remove("Password");
            ModelState.Remove("ConfirmPassword");

            if (ModelState.IsValid)
            {
                _user.UpdateRegistrationData(inUser);

                TempData["MSG_S"] = Messages.MSG_S001;

                return RedirectToAction(nameof(Index), "UserManagement");
            }

            return View();
        }

        [HttpGet]
        public IActionResult UpdatePassword(int id)
        {
            return View();
        }

        [HttpPost]
        public IActionResult UpdatePassword([FromForm] User inUser, int id)
        {
            ModelState.Remove("Id");
            ModelState.Remove("Email");
            ModelState.Remove("Name");
            ModelState.Remove("LastName");
            ModelState.Remove("Gender");
            ModelState.Remove("Active");

            if (ModelState.IsValid)
            {
                User user = _user.Read(id);
                user.Password = inUser.Password;

                _user.UpdatePassword(user);

                TempData["MSG_S"] = "Senha atualizada com sucesso!";

                return RedirectToAction(nameof(Index), "ControlPanel");
            }

            return View();
        }

        [HttpGet]
        public IActionResult Delete(int id)
        {
            _user.Delete(id);

            TempData["MSG_S"] = "Usuário excluído com sucesso!";

            return RedirectToAction(nameof(Index));
        }

    }
}
