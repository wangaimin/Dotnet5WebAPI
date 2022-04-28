using Dotnet5WebAPI.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Dotnet5WebAPI.Microsoft.EntityFrameworkCore
{
    public class AuthCenterContext : DbContext
    {
        public AuthCenterContext(DbContextOptions<AuthCenterContext> dbContextOptions)
            :base(dbContextOptions)
        { 
        
        }

        public DbSet<SystemUser> SystemUsers { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<SystemUser>().ToTable("SystemUser");
        }
    }
}
