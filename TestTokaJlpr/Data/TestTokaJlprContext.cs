#nullable disable
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TestTokaJlpr.Models;

namespace TestTokaJlpr.Data
{
    public class TestTokaJlprContext : DbContext
    {
        public TestTokaJlprContext (DbContextOptions<TestTokaJlprContext> options)
            : base(options)
        {
        }

        public DbSet<TestTokaJlpr.Models.PersonasFisicasModel> PersonasFisicasModel { get; set; }

        public DbSet<TestTokaJlpr.Models.LoginModel> LoginModel { get; set; }

        public DbSet<TestTokaJlpr.Models.CustomersModel> CustomersModel { get; set; }

        public DbSet<TestTokaJlpr.Models.DataModel> DataModel { get; set; }
    }
}
