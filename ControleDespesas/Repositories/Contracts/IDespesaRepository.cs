using ControleDespesas.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ControleDespesas.Repositories.Contracts
{
    public interface IDespesaRepository
    {
        public void Cadastrar(Despesa despesa);
        public Despesa Consultar(int id);
        public List<Despesa> ConsultarTodasDespesas();
        public void Atualizar(Despesa despesa);
        public void Excluir(int id);
    }
}
