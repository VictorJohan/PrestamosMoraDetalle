using Microsoft.EntityFrameworkCore;
using PrestamosMoraDetalle.Models;
using RegistroPrestamoBlazor.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PrestamosMoraDetalle.DAL
{
    public class Contexto : DbContext
    {
        public DbSet<Personas> Personas { get; set; }
        public DbSet<Prestamos> Prestamos { get; set; }
        public DbSet<Moras> Moras { get; set; }

        public Contexto(DbContextOptions<Contexto> option) : base(option) { }
    }
}
