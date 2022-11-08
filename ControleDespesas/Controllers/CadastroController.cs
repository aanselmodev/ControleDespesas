using ControleDespesas.Libraries.Email;
using ControleDespesas.Libraries.Sessoes;
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
    public class CadastroController : Controller
    {
        private IUsuarioRepository _usuarioRepository;
        private Email _email;
        private Sessao _sessao;
        

        public CadastroController(IUsuarioRepository usuarioRepository, Email email, Sessao sessao)
        {
            _usuarioRepository = usuarioRepository;
            _email = email;
            _sessao = sessao;
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
                _email.EnviarConfirmacaoCadastro(usuario);
                _sessao.Cadastrar("Usuario.Email", usuario.Email);
                
                return RedirectToAction("Confirmacao", "Cadastro");
            }

            return View();
        }

        [HttpGet]
        public IActionResult Confirmacao()
        {
            return View();
        }
    }
}
