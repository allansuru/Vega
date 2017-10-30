using Microsoft.EntityFrameworkCore;
using Vega.Core.Models;

namespace Vega.Core
{
    public class VegaDbContext : DbContext
    {
        public DbSet <Model> Models { get; set; }
        public DbSet<Make> Makes { get; set; }
        public DbSet<Feature> Features { get; set; }
        public DbSet<Vehicle> Vehicles { get; set; }
        // como na classe Make, eu tenho a Model, o entity framework ja se liga nisso e cria as tabelas relacionadas
        //public DbSet<Model> Models { get; set; }
        public VegaDbContext(DbContextOptions<VegaDbContext> options )
            :base(options)
        {
        }
        
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //chave primária composta no modelo(classe) VehicleFeature. Fluent API
            modelBuilder.Entity<VehicleFeature>().HasKey(vf =>
              new { vf.VehicleId, vf.FeatureId });
        }


    }
}
