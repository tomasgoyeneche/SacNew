using FluentValidation;
using Shared.Models;

namespace Core.Validators
{
    public class LocacionValidator : AbstractValidator<Locacion>
    {
        public LocacionValidator()
        {
            RuleFor(x => x.Nombre)
                .NotEmpty().WithMessage("El nombre es obligatorio.")
                .MaximumLength(100).WithMessage("El nombre no puede exceder 100 caracteres.");

            RuleFor(x => x.Direccion)
                .NotEmpty().WithMessage("La dirección es obligatoria.")
                .MaximumLength(200).WithMessage("La dirección no puede exceder 200 caracteres.");

            RuleFor(x => x.Carga)
                .NotNull().WithMessage("El campo 'Carga' es obligatorio.");

            RuleFor(x => x.Descarga)
                .NotNull().WithMessage("El campo 'Descarga' es obligatorio.");
        }
    }
}