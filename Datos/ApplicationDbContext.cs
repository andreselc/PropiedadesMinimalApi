using Microsoft.EntityFrameworkCore;
using PropiedadesMinimalApi.Modelos;

namespace PropiedadesMinimalApi.Datos
{
    public class ApplicationDbContext: DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options): base(options)
        {
            
        }

        public DbSet<Propiedad> Propiedad { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Propiedad>().HasData(
                new Propiedad { id = 1, nombre = "Casa de campo", descripcion = "Casa de campo en la montaña", ubicacion = "Montaña", activa = true, fechaCreacion = DateTime.Now },
                new Propiedad { id = 2, nombre = "Casa de playa", descripcion = "Casa de playa en la costa", ubicacion = "Costa", activa = true, fechaCreacion = DateTime.Now },
                new Propiedad { id = 3, nombre = "Casa de ciudad", descripcion = "Casa en la ciudad", ubicacion = "Ciudad", activa = true, fechaCreacion = DateTime.Now }
            );
        }
    }
}
