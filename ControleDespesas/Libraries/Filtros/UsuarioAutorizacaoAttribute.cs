using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ControleDespesas.Libraries.Filtros
{
    public class UsuarioAutorizacaoAttribute : Attribute, IAuthorizationFilter
    {
        public UsuarioAutorizacaoAttribute()
        {

        }

        public void OnAuthorization(AuthorizationFilterContext context)
        {
            
        }

        
    }
}
