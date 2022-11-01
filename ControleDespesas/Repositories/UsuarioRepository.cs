using ControleDespesas.Database;
using ControleDespesas.Models;
using ControleDespesas.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ControleDespesas.Repositories
{
    public class UsuarioRepository : IUsuarioRepository
    {
        private ControleDespesasContext _banco;

        public UsuarioRepository(ControleDespesasContext banco)
        {
            _banco = banco;
        }

        public void Atualizar(int id)
        {
            throw new NotImplementedException();
        }

        public void Cadastrar(Usuario usuario)
        {
            _banco.Add(usuario);
            _banco.SaveChanges();
        }

        public void Consultar(int id)
        {
            throw new NotImplementedException();
        }

        public void Excluir(int id)
        {
            throw new NotImplementedException();
        }
    }
}
