using ExpensesControl.Libraries;
using ExpensesControl.Models;
using ExpensesControl.Repositories;
using ExpensesControl.Repositories.Contracts;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ExpensesControl.Controllers
{
   
    public class HomeController : Controller
    {
        private IUserRepository _user;
        private LoginUser _login;
        

        public HomeController(IUserRepository user, LoginUser login)
        {
            _user = user;
            _login = login;
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }
        
        [HttpPost]
        public IActionResult Index([FromForm]User inUser)
        {
            ModelState.Remove("Id");
            ModelState.Remove("Name");
            ModelState.Remove("LastName");
            ModelState.Remove("Gender"); 
            ModelState.Remove("ConfirmPassword");
            ModelState.Remove("Active");

            if (ModelState.IsValid)
            {
                User user = _user.Login(inUser.Email, inUser.Password);

                if (user != null)
                {
                    if (user.Status == UserStatus.Active)
                    {
                        _login.Login(user);

                        return new RedirectToActionResult("Index", "ControlPanel", new { id = user.Id, area = "Access"});
                    }
                    else
                    {
                        ViewData["MSG_E"] = "Usuário inativo.";
                    }
                }
                else
                {
                    ViewData["MSG_E"] = "Usuário ou senha inválidos.";
                }
            }

            return View();
        }
    }
}
