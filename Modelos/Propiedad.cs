using System.ComponentModel.DataAnnotations;

namespace PropiedadesMinimalApi.Modelos
{
    public class Propiedad
    {
        [Key]
        public int id { get; set; }
        public string nombre { get; set; }
        public string descripcion { get; set; }
        public string ubicacion { get; set; }
        public bool activa { get; set; }
        public DateTime? fechaCreacion { get; set; }
    }
}
