using Microsoft.EntityFrameworkCore;
using NovelistBlazor.Common.Model;
using System.Globalization;
using Microsoft.Data.SqlClient;

namespace NovelistBlazor.API.Data
{
    public class NovelistDbContext : DbContext
    {
        public DbSet<Novel> Novels { get; set; }
        public DbSet<PlotUnit> PlotUnits { get; set; }
        public DbSet<PlotUnitType> PlotUnitTypes { get; set; }
        public DbSet<Character> Characters { get; set; }



        public NovelistDbContext(DbContextOptions<NovelistDbContext> options) : base(options)
        {

        }
    }
}

