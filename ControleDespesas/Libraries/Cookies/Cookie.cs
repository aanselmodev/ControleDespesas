using ControleDespesas.Libraries;
using Microsoft.Net.Http.Headers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Net.Http;
using Microsoft.AspNetCore.Http;

namespace ControleDespesas.Libraries
{
    public class Cookie
    {
        private Sessao _sessao;
        private IHttpContextAccessor _context;

        public Cookie(Sessao sessao, IHttpContextAccessor context)
        {
            _sessao = sessao;
            _context = context;
        }

        public string ObterHost()
        {
            return _context.HttpContext.Request.Host.Value;
        }
    }
}
