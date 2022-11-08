using ControleDespesas.Libraries.Login;
using ControleDespesas.Models;
using ControleDespesas.Repositories;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ControleDespesas.Controllers
{
    public class LoginController : Controller
    {
        private IUsuarioRepository _usuarioRepository;
        private LoginUsuario _login;

        public LoginController(IUsuarioRepository usuarioRepository, LoginUsuario login)
        {
            _usuarioRepository = usuarioRepository;
            _login = login;
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }
        
        [HttpPost]
        public IActionResult Login([FromForm]Usuario usuarioForm)
        {
            Usuario usuario = _usuarioRepository.Login(usuarioForm.Email, usuarioForm.Senha);

            if (usuario != null)
            {
                _login.Login(usuario);

                ViewData["MSG_S"] = "Usuário logado com sucesso!";

                return RedirectToAction("Index", "Home");
            }
            else
            {
                ViewData["MSG_E"] = "Usuário ou senha inválidos.";
            }
             
            return View();
        }

        [HttpGet]
        public IActionResult RecuperarSenha()
        {
            return View();
        }

        [HttpPost]
        public IActionResult RecuperarSenha([FromForm]Usuario usuarioForm)
        {


            return View();
        }


    }
}
