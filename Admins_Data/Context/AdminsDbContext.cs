using Admins_Data.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utils.Global;

namespace Admins_Data.Context
{
    public class AdminsDbContext : DbContext
    {
        public DbSet<Admin> Admins { get; set; }
        public DbSet<AdminFile> AdminFiles { get; set; }
        public DbSet<RefreshAdminToken> RefreshAdminTokens { get; set; }

        public AdminsDbContext()
        {
            Admins = this.Set<Admin>();
            AdminFiles = this.Set<AdminFile>();
            RefreshAdminTokens = this.Set<RefreshAdminToken>();
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(GlobalVariables.Admins_DB_CN);
        }
    }
}
