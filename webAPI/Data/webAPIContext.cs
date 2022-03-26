#nullable disable
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using webAPI.Models;

namespace webAPI.Data
{
    public class webAPIContext : DbContext
    {
        public webAPIContext (DbContextOptions<webAPIContext> options)
            : base(options)
        {
        }

        public DbSet<webAPI.Models.PersonasFisicas> PersonasFisicas { get; set; }

        public DbSet<webAPI.Models.Usuarios> Usuarios { get; set; }
    }
}
