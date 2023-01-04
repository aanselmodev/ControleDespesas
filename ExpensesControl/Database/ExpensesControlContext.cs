using ExpensesControl.Libraries;
using ExpensesControl.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ExpensesControl.Database
{
    public class ExpensesControlDbContext : DbContext
    {
        public ExpensesControlDbContext(DbContextOptions<ExpensesControlDbContext> options) : base(options)
        {

        }

        //TODO: Criar indexes para a tabela para melhorar performance
        public DbSet<User> Users { get; set; }
        public DbSet<PasswordReset> PasswordReset { get; set; }
    }
}
