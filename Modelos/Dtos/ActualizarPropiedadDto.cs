namespace PropiedadesMinimalApi.Modelos.Dtos
{
    public class ActualizarPropiedadDto
    {
        public int id { get; set; }
        public string nombre { get; set; }
        public string descripcion { get; set; }
        public string ubicacion { get; set; }
        public bool activa { get; set; }
    }
}
