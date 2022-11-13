using ControleDespesas.Libraries.Email;
using ControleDespesas.Libraries.Filtros;
using ControleDespesas.Libraries.Login;
using ControleDespesas.Libraries.Senha;
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
        private Email _email;

        public LoginController(IUsuarioRepository usuarioRepository, LoginUsuario login, Email email)
        {
            _usuarioRepository = usuarioRepository;
            _login = login;
            _email = email;
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }
        
        [HttpPost]
        public IActionResult Index([FromForm]Usuario usuarioForm)
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
            Usuario usuario = _usuarioRepository.ConsultarPorEmail(usuarioForm.Email);
            
            if (usuario != null)
            {
                usuario.Senha = Senha.GerarSenha();
                _usuarioRepository.Atualizar(usuario);
                _email.EnviarNovaSenha(usuario);

                ViewData["MSG_S"] = $"Senha enviada para o e-mail {usuario.Email}.";
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


    }
}
