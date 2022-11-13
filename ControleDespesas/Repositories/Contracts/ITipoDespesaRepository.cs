using ControleDespesas.Libraries.Tipos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ControleDespesas.Repositories.Contracts
{
    public interface ITipoDespesaRepository
    {
        public void Cadastrar(TipoDespesa tipo);
        public TipoDespesa Consultar(int id);
        public List<TipoDespesa> ConsultarTodosTipos();
        public void Atualizar(TipoDespesa tipo);
        public void Excluir(int id);
    }
}
