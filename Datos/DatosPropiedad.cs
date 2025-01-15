using PropiedadesMinimalApi.Modelos;

namespace PropiedadesMinimalApi.Datos
{
    public static class DatosPropiedad
    {
        public static List<Propiedad> listaPropiedades= new List<Propiedad>
        {
            new Propiedad { id = 1, nombre = "Casa de campo", descripcion = "Casa de campo en la montaña", ubicacion = "Montaña", activa = true, fechaCreacion = DateTime.Now.AddDays(-10) },
            new Propiedad { id = 2, nombre = "Casa de playa", descripcion = "Casa de playa en la costa", ubicacion = "Costa", activa = true, fechaCreacion = DateTime.Now.AddDays(-15) },
            new Propiedad { id = 3, nombre = "Casa de ciudad", descripcion = "Casa en la ciudad", ubicacion = "Ciudad", activa = true, fechaCreacion = DateTime.Now.AddDays(-30) }
        };

    }
}
