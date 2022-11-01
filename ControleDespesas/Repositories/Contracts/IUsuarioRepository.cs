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
        public void Consultar(int id);
        public void Atualizar(int id);
        public void Excluir(int id);

    }
}
