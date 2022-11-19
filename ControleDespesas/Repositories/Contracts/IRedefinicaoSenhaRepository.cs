using ControleDespesas.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ControleDespesas.Repositories.Contracts
{
    public interface IRedefinicaoSenhaRepository
    {
        public void Cadastrar(RedefinicaoSenha codigoSenha);
        public void ExcluirTodosPorIdUsuario(int id);
        public RedefinicaoSenha ConsultarPorIdUsuario(int id);
        public RedefinicaoSenha ConsultarPorCodigo(string codigo);

    }
}
