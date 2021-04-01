using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Text;
using Cerveceria2._0.Models;

namespace Cerveceria2._0.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
            InitContext();
        }

        public ApplicationDbContext():base()
        {
            InitContext();
        }

        private string connectionString;

        private void InitContext()
        {
            var builder = new ConfigurationBuilder();
            builder.AddJsonFile("appsettings.json", optional: false);
            var config = builder.Build();
            connectionString = config.GetConnectionString("DefaultConnection").ToString();

        }
        protected override void OnConfiguring(DbContextOptionsBuilder options) => options.UseSqlServer(connectionString);
        public DbSet<Cerveceria2._0.Models.Product> Product { get; set; }
        public DbSet<Cerveceria2._0.Models.Category> Category { get; set; }
        public DbSet<Cerveceria2._0.Models.VentaCabecera> VentaCabecera { get; set; }
        public DbSet<Cerveceria2._0.Models.VentaDetalle> VentaDetalle { get; set; }
    }

}
