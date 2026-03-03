using FluentValidation;
using Shared.Models;

namespace Core.Validators
{
    public class UnidadMantenimientoValidator : AbstractValidator<UnidadMantenimiento>
    {
        public UnidadMantenimientoValidator()
        {
            CascadeMode = CascadeMode.Stop;

            RuleFor(x => x.IdUnidad)
                .GreaterThan(0).WithMessage("Debe seleccionar una unidad válida.");

            RuleFor(x => x.IdMantenimientoEstado)
                .GreaterThan(0).WithMessage("Debe seleccionar un estado de mantenimiento válido.");

            RuleFor(x => x.Observaciones)
                .MaximumLength(250).WithMessage("Las observaciones no pueden exceder los 250 caracteres.");

            RuleFor(x => x.Odometro)
                .GreaterThanOrEqualTo(0).WithMessage("El odómetro debe ser un número mayor o igual a 0.");

            RuleFor(x => x.FechaInicio)
                .NotEmpty().WithMessage("Debe ingresar una fecha de inicio.");

            RuleFor(x => x.FechaFin)
                .NotEmpty().WithMessage("Debe ingresar una fecha de fin.");

            RuleFor(x => x)
            .Must(x => x.FechaInicio.Date <= x.FechaFin.Date)
            .WithMessage("La fecha de inicio no puede ser mayor que la fecha de fin.");
        }
    }
}