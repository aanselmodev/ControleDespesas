using AccessManagement.Libraries;
using AccessManagement.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AccessManagement.Database
{
    public class AccessManagementDbContext : DbContext
    {
        public AccessManagementDbContext(DbContextOptions<AccessManagementDbContext> options) : base(options)
        {

        }

        public DbSet<User> Users { get; set; }
        public DbSet<PasswordReset> PasswordReset { get; set; }

        
    }
}
