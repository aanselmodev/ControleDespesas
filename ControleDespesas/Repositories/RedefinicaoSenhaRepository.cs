using ControleDespesas.Database;
using ControleDespesas.Models;
using ControleDespesas.Repositories.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ControleDespesas.Repositories
{
    public class RedefinicaoSenhaRepository : IRedefinicaoSenhaRepository
    {
        private ControleDespesasContext _banco;

        public RedefinicaoSenhaRepository(ControleDespesasContext banco)
        {
            _banco = banco;
        }

        public void Cadastrar(RedefinicaoSenha redefinicaoSenha)
        {
            _banco.Add(redefinicaoSenha);
            _banco.SaveChanges();
        }

        public RedefinicaoSenha ConsultarPorCodigo(string codigo)
        {
            return _banco.RedefinicaoSenha.Where(x => x.Codigo == codigo).FirstOrDefault();
        }

        public RedefinicaoSenha ConsultarPorIdUsuario(int id)
        {
            return _banco.RedefinicaoSenha.Where(x => x.IdUsuario == id).FirstOrDefault();
        }

        public void ExcluirTodosPorIdUsuario(int id)
        {
            IQueryable<RedefinicaoSenha> lista = _banco.RedefinicaoSenha.Where(x => x.IdUsuario == id);
            _banco.RedefinicaoSenha.RemoveRange(lista);
            _banco.SaveChanges();
        }

    }
}
