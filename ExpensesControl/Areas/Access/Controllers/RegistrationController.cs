using ExpensesControl.Libraries;
using ExpensesControl.Models;
using ExpensesControl.Repositories;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace ExpensesControl.Controllers
{
    [Area("Access")]
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
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Index([FromForm]User inUser)
        {
            if (ModelState.IsValid)
            {
                if (_user.ReadByEmail(inUser.Email) == null)
                {
                    inUser.Status = UserStatus.Pending;
                    _user.Create(inUser);
                    
                    string url = $@"https://{_cookie.GetHost()}/Access/Registration/{nameof(ConfirmRegistration)}/{inUser.Id}";

                    _email.SendRegistrationConfirmation(inUser, url);

                    TempData["MSG_S"] = $"Cadastro realizado com sucesso! E-mail de confirmação enviado para {inUser.Email}";

                    return RedirectToAction("Index", "Home", new { area = "" });
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
