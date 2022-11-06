using ControleDespesas.Libraries.Sessoes;
using ControleDespesas.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ControleDespesas.Libraries.Login
{
    public class LoginUsuario
    {
        private Sessao _sessao;
        private const  string chaveLogin = "Login.Usuario";

        public LoginUsuario(Sessao sessao)
        {
            _sessao = sessao;
        }

        public void Login(Usuario usuario)
        {
            string jsonUsuario = JsonConvert.SerializeObject(usuario);

            _sessao.Cadastrar(chaveLogin, jsonUsuario);
        }

        public Usuario ObterUsuario()
        {
            if (_sessao.Existe(chaveLogin))
            {
                string jsonUsuario = _sessao.Consultar(chaveLogin);
                return JsonConvert.DeserializeObject<Usuario>(jsonUsuario);
            }
            else
            {
                return null;
            }
        }

        public void Logout()
        {
            _sessao.RemoverTodas();
        } 
    }
}
