using ControleDespesas.Libraries.Login;
using ControleDespesas.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ControleDespesas.Libraries.Filtros
{
    public class UsuarioAutorizacaoAttribute : Attribute, IAuthorizationFilter
    {
        private LoginUsuario _login;

        public UsuarioAutorizacaoAttribute()
        {
        }

        public void OnAuthorization(AuthorizationFilterContext context)
        {
            _login = (LoginUsuario)context.HttpContext.RequestServices.GetService(typeof(LoginUsuario));
            Usuario usuario = _login.ObterUsuario();

            if (usuario == null)
                context.Result = new RedirectToActionResult("Index", "Login", null);
        }
    }
}
