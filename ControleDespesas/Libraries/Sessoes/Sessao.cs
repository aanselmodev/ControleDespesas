using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ControleDespesas.Libraries.Sessoes
{
    public class Sessao
    {
        private IHttpContextAccessor _context;

        public Sessao(IHttpContextAccessor context)
        {
            _context = context;
        }

        public void Cadastrar(string chave, string valor)
        {
            _context.HttpContext.Session.SetString(chave, valor); 
        }

        public void Atualizar(string chave, string valor)
        {
            if (Existe(chave))
                Excluir(chave);

            _context.HttpContext.Session.SetString(chave, valor);
        }

        public void Excluir(string chave)
        {
            _context.HttpContext.Session.Remove(chave);
        }

        public string Consultar(string chave)
        {
            return _context.HttpContext.Session.GetString(chave);
        }

        public bool Existe(string chave)
        {
            return _context.HttpContext.Session.GetString(chave) == null ? false : true;
        }

        public void RemoverTodas()
        {
            _context.HttpContext.Session.Clear();
        }
    }

}
