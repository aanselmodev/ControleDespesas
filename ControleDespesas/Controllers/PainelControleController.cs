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
    [UsuarioAutorizacao]
    public class PainelControleController : Controller
    {
        private IUsuarioRepository _usuario;

        public PainelControleController(IUsuarioRepository usuario)
        {
            _usuario = usuario;
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult AtualizarDados(int id)
        {
            Usuario usuario = _usuario.Consultar(id);

            return View(usuario);
        }

        [HttpPost]
        public IActionResult AtualizarDados([FromForm] Usuario usuario)
        {
            ModelState.Remove("Senha");
            ModelState.Remove("ConfirmarSenha");

            if (ModelState.IsValid)
            {
                _usuario.AtualizarDadosCadastrais(usuario);

                TempData["MSG_S"] = "Dados atualizados com sucesso!";

                return RedirectToAction(nameof(Index), "PainelControle");
            }

            return View();
        }

        [HttpGet]
        public IActionResult AtualizarSenha(int id)
        {
            return View();
        }

        [HttpPost]
        public IActionResult AtualizarSenha([FromForm]Usuario usuarioForm, int id)
        {
            ModelState.Remove("Id");
            ModelState.Remove("Email");
            ModelState.Remove("Nome");
            ModelState.Remove("Sobrenome");
            ModelState.Remove("Sexo");
            ModelState.Remove("Ativo");

            if (ModelState.IsValid)
            {
                Usuario usuario = _usuario.Consultar(id);
                usuario.Senha = usuarioForm.Senha;

                _usuario.AtualizarSenha(usuario);

                TempData["MSG_S"] = "Senha atualizada com sucesso!";

                return RedirectToAction(nameof(Index), "PainelControle");
            }


            return View();
        }
    }
}
