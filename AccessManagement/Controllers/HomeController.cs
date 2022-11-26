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
    [UserAuth()]
    public class HomeController : Controller
    {
        private IUserRepository _user;

        public HomeController(IUserRepository user)
        {
            _user = user;
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }
       
    }
}
