using AccessManagement.Libraries;
using AccessManagement.Models;
using AccessManagement.Repositories;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace AccessManagement.Controllers
{
    public class RegistrationController : Controller
    {
        private IUserRepository _user;
        private Email _email;
        private SessionManagement _session;
        private CookieManagement _cookie;

        public RegistrationController(IUserRepository user, Email email, SessionManagement session, CookieManagement cookie)
        {
            _user = user;
            _email = email;
            _session = session;
            _cookie = cookie;
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Register([FromForm]User inUser)
        {
            if (ModelState.IsValid)
            {
                if (_user.ReadByEmail(inUser.Email) == null)
                {
                    inUser.Status = UserStatus.Pending;
                    _user.Create(inUser);
                    
                    string url = $@"https://{_cookie.GetHost()}/Registration/{nameof(ConfirmRegistration)}/{inUser.Id}";

                    _email.SendRegistrationConfirmation(inUser, url);

                    TempData["MSG_S"] = $"Cadastro realizado com sucesso! E-mail de confirmação enviado para {inUser.Email}";

                    return RedirectToAction("Index", "Login");
                }
                else
                {
                    ViewData["MSG_E"] = "E-mail já cadastrado!";
                }
            }

            return View();
        }

        [HttpGet]
        public IActionResult ConfirmRegistration(int id)
        {
            _user.UpdateUserStatus(id, UserStatus.Active);

            return View();
        }

    }
}
