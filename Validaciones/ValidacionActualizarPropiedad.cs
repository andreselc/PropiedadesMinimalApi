using FluentValidation;
using PropiedadesMinimalApi.Modelos.Dtos;

namespace PropiedadesMinimalApi.Validaciones
{
    public class ValidacionActualizarPropiedad : AbstractValidator<ActualizarPropiedadDto>
    {
        public ValidacionActualizarPropiedad()
        {
            RuleFor(modelo => modelo.id).NotEmpty().GreaterThan(0);
            RuleFor(modelo => modelo.nombre).NotEmpty();
            RuleFor(modelo => modelo.descripcion).NotEmpty();
            RuleFor(modelo => modelo.ubicacion).NotEmpty();
        }
    }
}
