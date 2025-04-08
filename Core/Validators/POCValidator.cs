using Core.Repositories;
using FluentValidation;
using Shared.Models;

namespace Core.Validators
{
    public class POCValidator : AbstractValidator<POC>, IConfigurableValidator<POC>
    {
        private IPOCRepositorio _pocRepositorio;
        private int? _idExistente;

        public POCValidator(IPOCRepositorio pocRepositorio)
        {
            _pocRepositorio = pocRepositorio;

            RuleFor(p => p.NumeroPoc)
                .NotEmpty().WithMessage("El número de POC es obligatorio.")
                .MaximumLength(50).WithMessage("El número de POC no debe superar los 50 caracteres.")
                .MustAsync(async (poc, numeroPoc, _) => !await ExisteNumeroPocAsync(numeroPoc))
                .WithMessage("Ya existe una POC con ese número.");

            RuleFor(p => p.IdUnidad)
                .GreaterThan(0).WithMessage("Debe seleccionar una unidad.")
                .MustAsync(async (poc, idUnidad, _) => !await ExistePocAbiertaConUnidadAsync(idUnidad))
                .WithMessage("Ya existe una POC abierta para esta unidad.");

            RuleFor(p => p.IdChofer)
                .GreaterThan(0).WithMessage("Debe seleccionar un chofer.")
                .MustAsync(async (poc, idChofer, _) => !await ExistePocAbiertaConChoferAsync(idChofer))
                .WithMessage("Ya existe una POC abierta para este chofer.");

            RuleFor(p => p.IdPeriodo)
                .GreaterThan(0).WithMessage("Debe seleccionar un período.");

            RuleFor(p => p.FechaCreacion)
                .NotEmpty().WithMessage("La fecha de creación es obligatoria.");

            RuleFor(p => p.Comentario)
                .MaximumLength(250).WithMessage("El comentario no debe exceder los 250 caracteres.");

            RuleFor(p => p.Odometro)
                .NotEmpty().WithMessage("El odometro es obligatorio.")
                .GreaterThanOrEqualTo(0).When(p => p.Odometro.HasValue)
                .WithMessage("El odómetro no puede ser negativo.");
        }

        public void Configurar(params object[] parametros)
        {
            // 👉 [0] = id actual (puede ser null si es nuevo)
            if (parametros.Length > 0 && parametros[0] is int id)
            {
                _idExistente = id;
            }
        }

        // ===========================
        // MÉTODOS PRIVADOS AUXILIARES
        // ===========================

        private async Task<bool> ExisteNumeroPocAsync(string numeroPoc)
        {
            var existente = await _pocRepositorio.ObtenerPorNumeroAsync(numeroPoc);
            return existente != null && existente.IdPoc != _idExistente;
        }

        private async Task<bool> ExistePocAbiertaConUnidadAsync(int idUnidad)
        {
            var abierta = await _pocRepositorio.ObtenerAbiertaPorUnidadAsync(idUnidad);
            return abierta != null && abierta.IdPoc != _idExistente;
        }

        private async Task<bool> ExistePocAbiertaConChoferAsync(int idChofer)
        {
            var abierta = await _pocRepositorio.ObtenerAbiertaPorChoferAsync(idChofer);
            return abierta != null && abierta.IdPoc != _idExistente;
        }
    }
}