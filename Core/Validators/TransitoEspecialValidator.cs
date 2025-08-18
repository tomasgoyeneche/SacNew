using FluentValidation;
using Shared.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Validators
{
    public class TransitoEspecialValidator : AbstractValidator<TransitoEspecial>
    {
        public TransitoEspecialValidator()
        {
            CascadeMode = CascadeMode.Stop;

            RuleFor(x => x.RazonSocial)
                .NotEmpty().WithMessage("La razón social es obligatoria.")
                .MaximumLength(150).WithMessage("La razón social no puede exceder los 150 caracteres.");

            RuleFor(x => x.Cuit)
                .NotEmpty().WithMessage("El CUIT es obligatorio.")
                .Matches(@"^\d{11}$").WithMessage("El CUIT debe contener 11 dígitos numéricos.");

            RuleFor(x => x.Apellido)
                .NotEmpty().WithMessage("El apellido es obligatorio.")
                .MaximumLength(100).WithMessage("El apellido no puede exceder los 100 caracteres.");

            RuleFor(x => x.Nombre)
                .NotEmpty().WithMessage("El nombre es obligatorio.")
                .MaximumLength(100).WithMessage("El nombre no puede exceder los 100 caracteres.");

            RuleFor(x => x.Documento)
                .NotEmpty().WithMessage("El documento es obligatorio.")
                .Matches(@"^\d+$").WithMessage("El documento debe contener solo números.")
                .MaximumLength(15).WithMessage("El documento no puede exceder los 15 caracteres.");

            RuleFor(x => x.Licencia)
                .NotNull().WithMessage("La licencia es obligatoria.")
                .GreaterThan(DateTime.Today.AddYears(-10)).WithMessage("La fecha de la licencia no puede ser muy antigua.");

            RuleFor(x => x.Art)
                .NotNull().WithMessage("La ART es obligatoria.");

            RuleFor(x => x.Seguro)
                .NotNull().WithMessage("El seguro es obligatorio.");

            RuleFor(x => x.Tractor)
                .NotEmpty().WithMessage("El tractor es obligatorio.")
                .MaximumLength(50).WithMessage("El tractor no puede exceder los 50 caracteres.");

            RuleFor(x => x.Semi)
                .NotEmpty().WithMessage("El semi es obligatorio.")
                .MaximumLength(50).WithMessage("El semi no puede exceder los 50 caracteres.");

            RuleFor(x => x.Zona)
                .NotEmpty().WithMessage("La zona es obligatoria.")
                .MaximumLength(100).WithMessage("La zona no puede exceder los 100 caracteres.");
        }
    }
}
