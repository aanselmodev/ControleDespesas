using ControleDespesas.Libraries;
using ControleDespesas.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ControleDespesas.Database
{
    public class ControleDespesasContext : DbContext
    {
        public ControleDespesasContext(DbContextOptions<ControleDespesasContext> options) : base(options)
        {

        }

        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<Despesa> Despesas { get; set; }
        public DbSet<TipoDespesa> TiposDespesa { get; set; }
        public DbSet<RedefinicaoSenha> RedefinicaoSenha { get; set; }

        
    }
}
