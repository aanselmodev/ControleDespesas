using ControleDespesas.Models;
using ControleDespesas.Repositories;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ControleDespesas.Controllers
{
    public class CadastroController : Controller
    {
        private IUsuarioRepository _usuarioRepository;

        public CadastroController(IUsuarioRepository usuarioRepository)
        {
            _usuarioRepository = usuarioRepository;
        }

        [HttpGet]
        public IActionResult Cadastrar()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Cadastrar([FromForm]Usuario usuario)
        {
            if (ModelState.IsValid)
            {
                _usuarioRepository.Cadastrar(usuario);

                ViewData["MSG_S"] = "Cadastro realizado com sucesso!";

                //TODO: Enviar e-mail de confirmação de cadastro e futuro envio de senha

                return RedirectToAction("Login", "Login");
            }

            return View();
        }
    }
}
