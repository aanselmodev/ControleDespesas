using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ExpensesControl.Libraries
{
    public class SessionManagement
    {
        private IHttpContextAccessor _context;

        public SessionManagement(IHttpContextAccessor context)
        {
            _context = context;
        }

        public void Create(string key, string value)
        {
            _context.HttpContext.Session.SetString(key, value); 
        }

        public void Update(string key, string value)
        {
            if (Exists(key))
                Delete(key);

            _context.HttpContext.Session.SetString(key, value);
        }

        public void Delete(string key)
        {
            _context.HttpContext.Session.Remove(key);
        }

        public string Read(string key)
        {
            return _context.HttpContext.Session.GetString(key);
        }

        public bool Exists(string key)
        {
            return _context.HttpContext.Session.GetString(key) == null ? false : true;
        }

        public void DeleteAll()
        {
            _context.HttpContext.Session.Clear();
        }
    }

}
