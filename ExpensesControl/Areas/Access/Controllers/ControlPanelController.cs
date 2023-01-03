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
    [UserAuth]
    [Area("Access")]
    public class ControlPanelController : Controller
    {
        private IUserRepository _user;

        public ControlPanelController(IUserRepository user)
        {
            _user = user;
        }

        [HttpGet]
        public IActionResult Index(int id)
        {
            User user = _user.Read(id);

            return View(user);
        }


    }
}
