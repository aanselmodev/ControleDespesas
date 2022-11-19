using ControleDespesas.Libraries.Filtros;
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
        public IActionResult Atualizar(int id)
        {
            Usuario usuario = _usuario.Consultar(id);

            return View(usuario);
        }

        [HttpPost]
        public IActionResult Atualizar([FromForm] Usuario usuario)
        {
            ModelState.Remove("Senha");
            ModelState.Remove("ConfirmarSenha");

            if (ModelState.IsValid)
            {
                _usuario.Atualizar(usuario);
                ViewData["MSG_S"] = "Dados atualizados com sucesso!";
            }

            return View();
        }

        [HttpGet]
        public IActionResult AtualizarSenha()
        {
            return View();

        }
    }
}
