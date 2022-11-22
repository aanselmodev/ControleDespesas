using AccessManagement.Libraries;
using Microsoft.Net.Http.Headers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Net.Http;
using Microsoft.AspNetCore.Http;

namespace AccessManagement.Libraries
{
    public class CookieManagement
    {
        private IHttpContextAccessor _context;

        public CookieManagement(IHttpContextAccessor context)
        {
            _context = context;
        }

        public string GetHost()
        {
            return _context.HttpContext.Request.Host.Value;
        }
    }
}
