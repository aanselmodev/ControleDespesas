using ControleDespesas.Libraries;
using ControleDespesas.Models;
using Microsoft.Net.Http.Headers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace ControleDespesas.Libraries
{
    public class LoginUsuario
    {
        private Sessao _sessao;
        private const string chaveLogin = "Login.Usuario";

        public LoginUsuario(Sessao sessao)
        {
            _sessao = sessao;
        }

        public void Login(Usuario usuario)
        {
            string jsonUsuario = JsonSerializer.Serialize(usuario, new JsonSerializerOptions() { IgnoreNullValues = true, WriteIndented = true });

            _sessao.Cadastrar(chaveLogin, jsonUsuario);
        }

        public Usuario ObterUsuario()
        {
            if (_sessao.Existe(chaveLogin))
            {
                string jsonUsuario = _sessao.Consultar(chaveLogin);
                return JsonSerializer.Deserialize<Usuario>(jsonUsuario);
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
