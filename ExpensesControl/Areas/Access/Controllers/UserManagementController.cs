﻿using ExpensesControl.Models;
using ExpensesControl.Repositories;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using X.PagedList;
using ExpensesControl.Libraries;

namespace ExpensesControl.Controllers
{
    [Area("Access")]
    public class UserManagementController : Controller
    {
        private IUserRepository _user;
        private Email _email;
        private CookieManagement _cookie;

        public UserManagementController(IUserRepository user, Email email, CookieManagement cookie)
        {
            _user = user;
            _email = email;
            _cookie = cookie;
        }

        public IActionResult Index(int? page, string inSearchValue, SearchTypeUser inSearchTypeUser, OrdinationType inOrdinationType)
        {
            try
            {
                IPagedList<User> usersPagedList = _user.ReadAllUsers(page, inSearchTypeUser, inSearchValue, inOrdinationType);

                TempData["inSearchValue"] = inSearchValue;
                TempData["inSearchTypeUser"] = inSearchTypeUser;
                TempData["OrdinationType"] = inOrdinationType;

                return View(usersPagedList);
            }
            catch (Exception ex)
            {
                throw new Exception(string.Concat(Messages.MSG_EX1, ex.Message));
            }
        }

        [HttpGet]
        public IActionResult UpdateRegistrationData(int id)
        {
            try
            {
                User user = _user.Read(id);

                return View(user);
            }
            catch (Exception ex )
            {
                throw new Exception(string.Concat(Messages.MSG_EX1, ex.Message));
            }
        }

        [HttpPost]
        public IActionResult UpdateRegistrationData([FromForm] User inUser)
        {
            try
            {
                ModelState.Remove("Password");
                ModelState.Remove("ConfirmPassword");
                ModelState.Remove("Email");

                if (ModelState.IsValid)
                {
                    _user.UpdateRegistrationData(inUser);

                    TempData["MSG_S"] = Messages.MSG_S001;

                    return RedirectToAction(nameof(Index), "UserManagement");
                }

                return View();
            }
            catch (Exception ex)
            {
                throw new Exception(string.Concat(Messages.MSG_EX1, ex.Message));
            }
        }

        [HttpGet]
        public IActionResult UpdatePassword(int id)
        {
            return View();
        }

        [HttpPost]
        public IActionResult UpdatePassword([FromForm] User inUser, int id)
        {
            try
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
            catch (Exception ex)
            {
                throw new Exception(string.Concat(Messages.MSG_EX1, ex.Message));
            }
        }

        [HttpGet]
        public IActionResult Delete(int id)
        {
            try
            {
                _user.Delete(id);

                TempData["MSG_S"] = "Usuário excluído com sucesso!";

                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                throw new Exception(string.Concat(Messages.MSG_EX1, ex.Message));
            }
        }

        [HttpGet]
        public IActionResult UpdateEmail(int id)
        {
            User user = _user.Read(id);

            return View(user);
        }

        [HttpPost]
        public IActionResult UpdateEmail([FromForm]User inUser)
        {
            try
            {
                ModelState.Remove("Id");
                ModelState.Remove("Password");
                ModelState.Remove("ConfirmPassword");
                ModelState.Remove("Name");
                ModelState.Remove("LastName");
                ModelState.Remove("Gender");
                ModelState.Remove("Active");

                if (ModelState.IsValid)
                {
                    User user = _user.Read(inUser.Id);

                    if (user.Email != inUser.Email)
                    {
                        user.Email = inUser.Email;
                        user.Status = UserStatus.Pending;

                        _user.UpdateEmail(user);

                        string url = $@"https://{_cookie.GetHost()}/Access/UserManagement/{nameof(ConfirmUpdateEmail)}/{user.Id}";

                        _email.SendUpdateEmailConfirmation(user, url);

                        TempData["MSG_S"] = $"e-mail alterado com sucesso! será enviado um e-mail de confirmação para {inUser.Email} para formalizar a alteração.";

                        return RedirectToAction("Index", "Home", new { area = "" });
                    }
                    else
                    {
                        TempData["MSG_E"] = "o novo e-mail não pode ser igual ao e-mail atual.";
                    }
                }

                return View();

            }
            catch (Exception ex)
            {
                throw new Exception(string.Concat(Messages.MSG_EX1, ex.Message));
            }
        }

        public IActionResult ConfirmUpdateEmail(int id)
        {
            _user.UpdateUserStatus(id, UserStatus.Active);

            return View();
        }
    }
}
