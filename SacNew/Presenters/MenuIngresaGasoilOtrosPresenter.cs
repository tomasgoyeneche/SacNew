using Microsoft.Extensions.DependencyInjection;
using SacNew.Interfaces;
using SacNew.Repositories;
using SacNew.Services;
using SacNew.Views.GestionFlota.Postas.IngresaConsumos.IngresarConsumo;
using System.Globalization;

namespace SacNew.Presenters
{
    public class MenuIngresaGasoilOtrosPresenter : BasePresenter<IMenuIngresaGasoilOtrosView>
    {
        private readonly IEmpresaCreditoRepositorio _empresaCreditoRepositorio;
        private readonly IRepositorioPOC _pocRepositorio;
        private readonly INominaRepositorio _nominaRepositorio;

        public MenuIngresaGasoilOtrosPresenter(
            IEmpresaCreditoRepositorio empresaCreditoRepositorio,
            IRepositorioPOC pocRepositorio,
            INominaRepositorio nominaRepositorio,
            ISesionService sesionService,
            IServiceProvider serviceProvider
        ) : base(sesionService, serviceProvider)
        {
            _empresaCreditoRepositorio = empresaCreditoRepositorio ?? throw new ArgumentNullException(nameof(empresaCreditoRepositorio));
            _pocRepositorio = pocRepositorio ?? throw new ArgumentNullException(nameof(pocRepositorio));
            _nominaRepositorio = nominaRepositorio ?? throw new ArgumentNullException(nameof(nominaRepositorio));
        }

        public async Task CargarDatosAsync(int idPoc)
        {
            if (idPoc <= 0)
            {
                _view.MostrarMensaje("El ID del POC no es válido.");
                return;
            }

            await EjecutarConCargaAsync(async () =>
            {
                var poc = await _pocRepositorio.ObtenerPorIdAsync(idPoc)
                           ?? throw new Exception("No se encontró el POC seleccionado.");

                _view.NumeroPoc = poc.NumeroPOC;

                var nomina = await _nominaRepositorio.ObtenerPorIdAsync(poc.IdNomina)
                             ?? throw new Exception("No se encontró la nomina asociada al POC.");

                var empresaCredito = await _empresaCreditoRepositorio.ObtenerPorEmpresaAsync(nomina.idEmpresa)
                                     ?? throw new Exception("No se encontraron créditos para la empresa.");

                _view.CreditoTotal = empresaCredito.CreditoAsignado.ToString("C", new CultureInfo("es-AR"));
                _view.CreditoDisponible = empresaCredito.CreditoDisponible.ToString("C", new CultureInfo("es-AR"));
            });
        }

        public void IngresaGasoil()
        {
            _serviceProvider.GetService<IngresaGasoil>()?.Show();
        }
    }
}