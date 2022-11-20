using ControleDespesas.Libraries;
using ControleDespesas.Models;
using ControleDespesas.Repositories;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ControleDespesas.Controllers
{
    [UsuarioAutorizacao()]
    public class HomeController : Controller
    {
        private IUsuarioRepository _usuario;

        public HomeController(IUsuarioRepository usuario)
        {
            _usuario = usuario;
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }
       
    }
}
