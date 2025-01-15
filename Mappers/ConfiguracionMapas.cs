using AutoMapper;
using PropiedadesMinimalApi.Modelos;
using PropiedadesMinimalApi.Modelos.Dtos;

namespace PropiedadesMinimalApi.Mappers
{
    public class ConfiguracionMapas: Profile
    {
        public ConfiguracionMapas()
        {
            CreateMap<Propiedad, CrearPropiedadDto>().ReverseMap();
            CreateMap<Propiedad, PropiedadDto>().ReverseMap();
        }
    }
}
