using AccessManagement.Libraries;
using AccessManagement.Models;
using AccessManagement.Repositories;
using AccessManagement.Repositories.Contracts;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AccessManagement.Controllers
{
    public class LoginController : Controller
    {
        private IUserRepository _user;
        private LoginUser _login;
        private Email _email;
        private CookieManagement _cookie;
        private IPasswordResetRepository _passwordReset;

        public LoginController(IUserRepository user, LoginUser login, Email email, CookieManagement cookie, IPasswordResetRepository passwordReset)
        {
            _user = user;
            _login = login;
            _email = email;
            _cookie = cookie;
            _passwordReset = passwordReset;
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

                        return RedirectToAction("Index", "Home", new { id = user.Id });
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

        [HttpGet]
        public IActionResult RecoverPassword()
        {
            return View();
        }

        [HttpPost]
        public IActionResult RecoverPassword([FromForm]User inUser)
        {
            inUser = _user.ReadByEmail(inUser.Email);
            
            if (inUser != null)
            {
                string url = $@"https://{_cookie.GetHost()}/Login/{nameof(GenerateNewPassword)}/{inUser.Id}";
                string code = PasswordManagement.GenerateCodePasswordReset();

                _passwordReset.DeleteAllByUserId(inUser.Id);
                _passwordReset.Create(new PasswordReset() { UserId = inUser.Id, Code = code, ExpirationDate = DateTime.Now.AddMinutes(5.0), Password = inUser.Password } );

                _user.UpdateUserStatus(inUser.Id, UserStatus.Pending);
                
                _email.SendPasswordResetCode(inUser, url, code);

                TempData["MSG_S"] = $"Link para gerar uma nova senha enviado para o e-mail {inUser.Email}.";

                return RedirectToAction("Index", "Login");
            }
            else
            {
                ViewData["MSG_E"] = $"E-mail não cadastrado.";
            }

            return View();
        }
        
        [HttpGet]
        public IActionResult Logout()
        {
            _login.Logout();

            return RedirectToAction("Index", "Login");
        }

        [HttpGet]
        public IActionResult GenerateNewPassword()
        {
            return View();
        }

        [HttpPost]
        public IActionResult GenerateNewPassword([FromForm]PasswordReset passwordReset, int id)
        {
            if (ModelState.IsValid)
            {
                PasswordReset resetPassword = _passwordReset.ReadByUserId(id);

                if (resetPassword != null )
                {
                    if (resetPassword.ExpirationDate > DateTime.Now)
                    {
                        if (passwordReset.Code == resetPassword.Code)
                        {
                            User user = _user.Read(id);

                            if(user.Password != passwordReset.Password)
                            {
                                user.Password = passwordReset.Password;

                                _user.UpdatePassword(user);
                                _user.UpdateUserStatus(user.Id, UserStatus.Active);

                                TempData["MSG_S"] = "Nova senha cadastrada com sucesso!";

                                return RedirectToAction(nameof(Index), "Login");
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
