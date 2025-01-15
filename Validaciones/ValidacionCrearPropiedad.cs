using FluentValidation;
using PropiedadesMinimalApi.Modelos.Dtos;

namespace PropiedadesMinimalApi.Validaciones
{
    public class ValidacionCrearPropiedad : AbstractValidator<CrearPropiedadDto>
    {
        public ValidacionCrearPropiedad()
        {
            RuleFor(modelo => modelo.nombre).NotEmpty();
            RuleFor(modelo => modelo.descripcion).NotEmpty();
            RuleFor(modelo => modelo.ubicacion).NotEmpty();
        }
    }
}
