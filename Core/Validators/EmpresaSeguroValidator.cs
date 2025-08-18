using FluentValidation;
using Shared.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Validators
{
    public class EmpresaSeguroValidator : AbstractValidator<EmpresaSeguro>
    {
        public EmpresaSeguroValidator()
        {
            CascadeMode = CascadeMode.Stop;

            RuleFor(x => x.idEmpresa)
                .GreaterThan(0).WithMessage("Debe seleccionar una empresa válida.");

            RuleFor(x => x.idEmpresaSeguroEntidad)
                .GreaterThan(0).WithMessage("Debe seleccionar una entidad de seguro válida.");

            RuleFor(x => x.idCia)
                .GreaterThan(0).WithMessage("Debe seleccionar una compañía de seguros válida.");

            RuleFor(x => x.idCobertura)
                .GreaterThan(0).WithMessage("Debe seleccionar una cobertura válida.");

            RuleFor(x => x.numeroPoliza)
                .NotEmpty().WithMessage("El número de póliza es obligatorio.")
                .MaximumLength(50).WithMessage("El número de póliza no puede exceder los 50 caracteres.");

            RuleFor(x => x.certificadoMensual)
                .NotEmpty().WithMessage("La fecha del certificado mensual es obligatoria.");

            RuleFor(x => x.vigenciaAnual)
                .NotEmpty().WithMessage("La fecha de vigencia anual es obligatoria.");

            RuleFor(x => x)
                .Must(x => x.vigenciaAnual >= x.certificadoMensual)
                .WithMessage("La vigencia anual debe ser posterior o igual a la fecha del certificado mensual.");

            // Opcional: asegurar que la póliza siga vigente al menos hoy
            RuleFor(x => x.vigenciaAnual)
                .GreaterThanOrEqualTo(DateTime.Today)
                .WithMessage("La póliza ya está vencida, la vigencia anual debe ser mayor o igual a la fecha actual.");
        }
    }
}
