using ControleDespesas.Libraries.Cookies;
using ControleDespesas.Libraries.Email;
using ControleDespesas.Libraries.Filtros;
using ControleDespesas.Libraries.Login;
using ControleDespesas.Libraries.Senha;
using ControleDespesas.Models;
using ControleDespesas.Repositories;
using ControleDespesas.Repositories.Contracts;
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
        private Cookie _cookie;
        private IRedefinicaoSenhaRepository _redefinicaoSenha;

        public LoginController(IUsuarioRepository usuarioRepository, LoginUsuario login, Email email, Cookie cookie, IRedefinicaoSenhaRepository redefinicaoSenha)
        {
            _usuarioRepository = usuarioRepository;
            _login = login;
            _email = email;
            _cookie = cookie;
            _redefinicaoSenha = redefinicaoSenha;
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }
        
        [HttpPost]
        public IActionResult Index([FromForm]Usuario usuario)
        {
            ModelState.Remove("Id");
            ModelState.Remove("Nome");
            ModelState.Remove("Sobrenome");
            ModelState.Remove("Sexo"); 
            ModelState.Remove("ConfirmarSenha");
            ModelState.Remove("Ativo");

            if (ModelState.IsValid)
            {
                usuario = _usuarioRepository.Login(usuario.Email, usuario.Senha);

                if (usuario != null)
                {
                    if (usuario.Ativo)
                    {
                        _login.Login(usuario);

                        return RedirectToAction("Index", "Home", new { id = usuario.Id });
                    }
                    else
                    {
                        ViewData["MSG_E"] = "Usuário inativo.";
                    }
                }
                else
                {
                    ViewData["MSG_E"] = "Usuário ou senha inválidos.";
                }
            }

            return View();
        }

        [HttpGet]
        public IActionResult RecuperarSenha()
        {
            return View();
        }

        [HttpPost]
        public IActionResult RecuperarSenha([FromForm]Usuario usuario)
        {
            usuario = _usuarioRepository.ConsultarPorEmail(usuario.Email);
            
            if (usuario != null)
            {
                string url = $@"https://{_cookie.ObterHost()}/Login/{nameof(GerarNovaSenha)}/{usuario.Id}";
                string codigo = Senha.GerarCodigoRedefinicaoSenha();

                _redefinicaoSenha.ExcluirTodosPorIdUsuario(usuario.Id);
                _redefinicaoSenha.Cadastrar(new RedefinicaoSenha() { IdUsuario = usuario.Id, Codigo = codigo, DataExpiracao = DateTime.Now.AddMinutes(10.0), Senha = usuario.Senha } );

                _email.EnviarNovaSenha(usuario, url, codigo);

                ViewData["MSG_S"] = $"Link para gerar uma nova senha enviado para o e-mail {usuario.Email}.";
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

        [HttpGet]
        public IActionResult GerarNovaSenha()
        {
            return View();
        }

        [HttpPost]
        public IActionResult GerarNovaSenha([FromForm]RedefinicaoSenha redefinicaoSenha, int id)
        {
            if (ModelState.IsValid)
            {
                RedefinicaoSenha redefinicao = _redefinicaoSenha.ConsultarPorIdUsuario(id);

                if (redefinicao != null )
                {
                    if (redefinicao.DataExpiracao > DateTime.Now)
                    {
                        if (redefinicaoSenha.Codigo == redefinicao.Codigo)
                        {
                            Usuario usuario = _usuarioRepository.Consultar(id);

                            if(usuario.Senha != redefinicaoSenha.Senha)
                            {
                                usuario.Senha = redefinicaoSenha.Senha;

                                _usuarioRepository.AtualizarSenha(usuario);

                                TempData["MSG_S"] = "Nova senha cadastrada com sucesso!";

                                return RedirectToAction(nameof(Index), "Login");
                            }
                            else
                            {
                                ViewData["MSG_E"] = "A nova senha não pode ser igual a anterior.";
                            }
                        }
                        else
                        {
                            ViewData["MSG_E"] = "O código informado é inválido.";
                        }
                    }
                    else
                    {
                        ViewData["MSG_E"] = "O código enviado está expirado.";
                    }
                }
                else
                {
                    ViewData["MSG_E"] = "O usuário para redefinir a senha não existe.";
                }
            }

            return View();
        }


    }
}
