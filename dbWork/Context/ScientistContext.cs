using System;
using System.Linq;
using dbWork.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace dbWork.Context
{
    public sealed class ScientistContext : DbContext 
    {
        public DbSet<Scientist> Scientist { get; set; }
        public DbSet<Organization> Organization { get; set; }
        public ScientistContext()
        {
            try
            {
                var count = Scientist.Count();
                count = Organization.Count();
            }
            catch (Exception)
            {
                Database.EnsureCreated();
            }
        }
        
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var pass = Environment
            .GetEnvironmentVariable("DBPOSTGRESQLPASSWORD", EnvironmentVariableTarget.Machine);
            var login = Environment
            .GetEnvironmentVariable("DBPOSTGRESQLLOGIN", EnvironmentVariableTarget.Machine);

            optionsBuilder.UseNpgsql($"Host=localhost;Port=5432;Database=postgres;" +
                                     $"Username={login};Password={pass}");
        }
        
    }
}