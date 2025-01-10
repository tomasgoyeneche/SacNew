using Core.Repositories;
using FluentValidation;
using Shared.Models;

namespace Core.Validators
{
    public class ConsumoGasoilValidator : AbstractValidator<ConsumoGasoil>, IConfigurableValidator<ConsumoGasoil>
    {
        private decimal _capacidadTanque;
        private decimal _autorizado;
        private IConsumoGasoilRepositorio _consumoGasoilRepositorio;

        public ConsumoGasoilValidator(IConsumoGasoilRepositorio consumoGasoilRepositorio)
        {
            _consumoGasoilRepositorio = consumoGasoilRepositorio;

            RuleFor(c => c.LitrosCargados)
                .NotEmpty().WithMessage("Debe ingresar un valor para los litros.")
                .GreaterThan(0).WithMessage("Los litros deben ser mayores a 0.")
                .Must(litros => decimal.TryParse(litros.ToString(), out _)).WithMessage("Los litros deben ser un número válido.");

            RuleFor(c => c.NumeroVale)
                .NotEmpty().WithMessage("Debe ingresar un número de vale.")
                .MaximumLength(20).WithMessage("El número de vale no debe exceder los 20 caracteres.")
                //  .MustAsync(async (numeroVale, cancellation) => !await _consumoGasoilRepositorio.ExisteNumeroValeAsync(numeroVale))
                .WithMessage("El número de vale ya existe.");

            RuleFor(c => c.IdConsumo)
                .GreaterThan(0).WithMessage("Debe seleccionar un tipo de gasoil válido.");

            RuleFor(c => c.FechaCarga)
                .NotEmpty().WithMessage("Debe ingresar una fecha válida.");

            RuleFor(c => c.Observaciones)
                .MaximumLength(250).WithMessage("Las observaciones no deben exceder los 250 caracteres.");

            RuleFor(c => c.PrecioTotal)
                .GreaterThan(0).WithMessage("El precio total debe ser mayor a 0.");
        }

        public void Configurar(params object[] parametros)
        {
            if (parametros.Length >= 2)
            {
                _capacidadTanque = (decimal)parametros[0];
                _autorizado = (decimal)parametros[1];

                RuleFor(c => c.LitrosCargados)
                    .LessThanOrEqualTo(_capacidadTanque).WithMessage($"Los litros no pueden exceder la capacidad del tanque ({_capacidadTanque}).");
                // .LessThanOrEqualTo(_autorizado).WithMessage($"Los litros no pueden exceder el límite autorizado ({_autorizado}).");
            }
        }
    }
}