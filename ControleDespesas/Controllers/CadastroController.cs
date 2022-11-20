using ControleDespesas.Libraries;
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
        private IUsuarioRepository _usuario;
        private Email _email;
        private Sessao _sessao;
        private Cookie _cookie;

        public CadastroController(IUsuarioRepository usuarioRepository, Email email, Sessao sessao, Cookie cookie)
        {
            _usuario = usuarioRepository;
            _email = email;
            _sessao = sessao;
            _cookie = cookie;
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
                if (_usuario.ConsultarPorEmail(usuario.Email) == null)
                {
                    usuario.Ativo = false;
                    _usuario.Cadastrar(usuario);
                    
                    string url = $@"https://{_cookie.ObterHost()}/Cadastro/{nameof(Confirmar)}/{usuario.Id}";

                    _email.EnviarConfirmacaoCadastro(usuario, url);

                    TempData["MSG_S"] = $"Cadastro realizado com sucesso! E-mail de confirmação enviado para {usuario.Email}";

                    return RedirectToAction("Index", "Login");
                }
                else
                {
                    ViewData["MSG_E"] = "E-mail já cadastrado!";
                }
            }

            return View();
        }

        [HttpGet]
        public IActionResult Confirmar(int id)
        {
            _usuario.AtualizarStatusUsuario(id, true);

            return View();
        }

    }
}
