using ExpensesControl.Libraries;
using ExpensesControl.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ExpensesControl.Libraries
{
    public class UserAuthAttribute : Attribute, IAuthorizationFilter
    {
        private LoginUser _login;

        public UserAuthAttribute()
        {
        }

        public void OnAuthorization(AuthorizationFilterContext context)
        {
            _login = (LoginUser)context.HttpContext.RequestServices.GetService(typeof(LoginUser));
            User user = _login.GetUser();

            if (user == null)
                context.Result = new RedirectToActionResult("Index", "Login", null);
        }
    }
}
