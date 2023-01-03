using ExpensesControl.Libraries;
using ExpensesControl.Models;
using ExpensesControl.Repositories;
using ExpensesControl.Repositories.Contracts;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ExpensesControl.Areas.Access.Controllers
{
    [Area("Access")]
    public class PasswordController : Controller
    {
        private IUserRepository _user;
        private LoginUser _login;
        private Email _email;
        private CookieManagement _cookie;
        private IPasswordResetRepository _passwordReset;

        public PasswordController(IUserRepository user, LoginUser login, Email email, CookieManagement cookie, IPasswordResetRepository passwordReset)
        {
            _user = user;
            _login = login;
            _email = email;
            _cookie = cookie;
            _passwordReset = passwordReset;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult RecoverPassword()
        {
            return View();
        }

        [HttpPost]
        public IActionResult RecoverPassword([FromForm] User inUser)
        {
            inUser = _user.ReadByEmail(inUser.Email);

            if (inUser != null)
            {
                string url = $@"https://{_cookie.GetHost()}/Access/Password/{nameof(GenerateNewPassword)}/{inUser.Id}";
                string code = PasswordManagement.GenerateCodePasswordReset();

                _passwordReset.DeleteAllByUserId(inUser.Id);
                _passwordReset.Create(new PasswordReset() { UserId = inUser.Id, Code = code, ExpirationDate = DateTime.Now.AddMinutes(5.0), Password = inUser.Password });

                _user.UpdateUserStatus(inUser.Id, UserStatus.Pending);

                _email.SendPasswordResetCode(inUser, url, code);

                TempData["MSG_S"] = $"Link para gerar uma nova senha enviado para o e-mail {inUser.Email}.";

                return RedirectToAction("Index", "Home", new { area = "" });
            }
            else
            {
                ViewData["MSG_E"] = $"E-mail não cadastrado.";
            }

            return View();
        }

        [HttpGet]
        public IActionResult GenerateNewPassword()
        {
            return View();
        }

        [HttpPost]
        public IActionResult GenerateNewPassword([FromForm] PasswordReset passwordReset, int id)
        {
            if (ModelState.IsValid)
            {
                PasswordReset resetPassword = _passwordReset.ReadByUserId(id);

                if (resetPassword != null)
                {
                    if (resetPassword.ExpirationDate > DateTime.Now)
                    {
                        if (passwordReset.Code == resetPassword.Code)
                        {
                            User user = _user.Read(id);

                            if (user.Password != passwordReset.Password)
                            {
                                user.Password = passwordReset.Password;

                                _user.UpdatePassword(user);
                                _user.UpdateUserStatus(user.Id, UserStatus.Active);

                                TempData["MSG_S"] = "Nova senha cadastrada com sucesso!";

                                return RedirectToAction(nameof(Index), "Home", new { area = "" });
                            }
                            else
                            {
                                ViewData["MSG_E"] = "A nova senha não pode ser igual a anterior.";
                            }
                        }
                        else
                        {
                            ViewData["MSG_E"] = "O código informado é inválido.";
                        }
                    }
                    else
                    {
                        ViewData["MSG_E"] = "O código enviado está expirado.";
                    }
                }
                else
                {
                    ViewData["MSG_E"] = "O usuário para redefinir a senha não existe.";
                }
            }

            return View();
        }
    }
}
