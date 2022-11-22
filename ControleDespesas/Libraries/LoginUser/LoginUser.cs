using AccessManagement.Libraries;
using AccessManagement.Models;
using Microsoft.Net.Http.Headers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace AccessManagement.Libraries
{
    public class LoginUser
    {
        private SessionManagement _session;
        private const string key = "Login.User";

        public LoginUser(SessionManagement session)
        {
            _session = session;
        }

        public void Login(User user)
        {
            string jsonUser = JsonSerializer.Serialize(user, new JsonSerializerOptions() { IgnoreNullValues = true, WriteIndented = true });

            _session.Create(key, jsonUser);
        }

        public User GetUser()
        {
            if (_session.Exists(key))
            {
                string jsonUser = _session.Read(key);
                return JsonSerializer.Deserialize<User>(jsonUser);
            }
            else
            {
                return null;
            }
        }

        public void Logout()
        {
            _session.DeleteAll();
        } 

    }
}
