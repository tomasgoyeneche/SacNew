using FluentValidation;
using Shared.Models.RequerimientoCompra;

namespace Core.Validators
{
    public class RcValidator : AbstractValidator<RcRcc>
    {
        public RcValidator()
        {
            // Validar que se haya seleccionado un proveedor (valor mayor a 0)
            RuleFor(x => x.IdProveedor)
                .GreaterThan(0)
                .WithMessage("Debe seleccionar un proveedor.");

            // Validar que el usuario emisor tenga un valor válido
            RuleFor(x => x.Emitido)
                .GreaterThan(0)
                .WithMessage("El usuario emisor es obligatorio.");

            // Validar que se haya seleccionado un usuario aprobador
            RuleFor(x => x.Aprobado)
                .GreaterThan(0)
                .WithMessage("Debe seleccionar un usuario aprobador.");

            // Validar que el lugar de entrega no esté vacío y no exceda 200 caracteres
            RuleFor(x => x.EntregaLugar)
                .NotEmpty().WithMessage("El lugar de entrega es obligatorio.")
                .MaximumLength(200).WithMessage("El lugar de entrega no puede exceder 200 caracteres.");

            // Validar la fecha de entrega:
            // - No debe estar vacía.
            // - Debe ser un string que se pueda convertir a DateTime.
            // - (Opcional) Debe ser igual o posterior a la fecha del RC.
            RuleFor(x => x.EntregaFecha)
                .NotEmpty().WithMessage("La fecha de entrega es obligatoria.");

            // Validar el importe:
            // - No debe estar vacío.
            // - Debe ser un número válido (se recibe como string).
            // - Debe ser mayor que cero.
            RuleFor(x => x.Importe)
                .NotEmpty().WithMessage("El importe es obligatorio.")
                .MaximumLength(100).WithMessage("El importe no puede exceder 100 caracteres.");

            // Validar la condición de pago: obligatoria y con un máximo de 100 caracteres.
            RuleFor(x => x.CondicionPago)
                .NotEmpty().WithMessage("La condición de pago es obligatoria.")
                .MaximumLength(100).WithMessage("La condición de pago no puede exceder 100 caracteres.");

            // Validar las observaciones: campo opcional, pero con un máximo de 500 caracteres.
            RuleFor(x => x.Observaciones)
                .MaximumLength(500).WithMessage("Las observaciones no pueden exceder 500 caracteres.");
        }
    }
}