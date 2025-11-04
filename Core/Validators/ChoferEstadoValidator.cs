using FluentValidation;
using Shared.Models;

namespace Core.Validators
{
    public class ChoferEstadoValidator : AbstractValidator<ChoferEstado>
    {
        public ChoferEstadoValidator()
        {
            CascadeMode = CascadeMode.Stop;

            RuleFor(x => x.IdChofer)
                .GreaterThan(0).WithMessage("Debe seleccionar un chofer válido.");

            RuleFor(x => x.IdEstado)
                .GreaterThan(0).WithMessage("Debe seleccionar un estado válido.");

            RuleFor(x => x.Observaciones)
                .MaximumLength(250).WithMessage("Las observaciones no pueden exceder los 250 caracteres.");

            RuleFor(x => x.FechaInicio)
                .NotEmpty().WithMessage("Debe ingresar una fecha de inicio.");

            RuleFor(x => x.FechaFin)
                .NotEmpty().WithMessage("Debe ingresar una fecha de fin.");
        }
    }
}