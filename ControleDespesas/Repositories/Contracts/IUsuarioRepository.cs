using ControleDespesas.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ControleDespesas.Repositories
{
    public interface IUsuarioRepository
    {
        public void Cadastrar(Usuario usuario);
        public Usuario Consultar(int id);
        public void Atualizar(Usuario usuario);
        public void Excluir(int id);
        public Usuario Login(string email, string senha);

    }
}
