using AccessManagement.Libraries;
using AccessManagement.Models;
using AccessManagement.Repositories;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace AccessManagement.Controllers
{
    [UserAuth]
    public class ControlPanelController : Controller
    {
        private IUserRepository _user;

        public ControlPanelController(IUserRepository user)
        {
            _user = user;
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult UpdateRegistrationData(int id)
        {
            User user = _user.Read(id);

            return View(user);
        }

        [HttpPost]
        public IActionResult UpdateRegistrationData([FromForm] User user)
        {
            ModelState.Remove("Password");
            ModelState.Remove("ConfirmPassword");

            if (ModelState.IsValid)
            {
                _user.UpdateRegistrationData(user);

                TempData["MSG_S"] = "Dados atualizados com sucesso!";

                return RedirectToAction(nameof(Index), "ControlPanel");
            }

            return View();
        }

        [HttpGet]
        public IActionResult UpdatePassword(int id)
        {
            return View();
        }

        [HttpPost]
        public IActionResult UpdatePassword([FromForm]User inUser, int id)
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
    }
}
