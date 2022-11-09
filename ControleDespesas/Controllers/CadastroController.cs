using ControleDespesas.Libraries.Email;
using ControleDespesas.Libraries.Senha;
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
        public IActionResult Cadastrar([FromForm]Usuario usuarioForm)
        {
            if (ModelState.IsValid)
            {
                Usuario usuario = _usuarioRepository.ConsultarPorEmail(usuarioForm.Email);

                if (usuario == null)
                {
                    _usuarioRepository.Cadastrar(usuarioForm);
                    _email.EnviarConfirmacaoCadastro(usuarioForm);
                    _sessao.Cadastrar("Usuario.Email", usuarioForm.Email);

                    return RedirectToAction("Confirmacao", "Cadastro");
                }
                else
                {
                    ViewData["MSG_E"] = "E-mail já cadastrado!";
                }
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
