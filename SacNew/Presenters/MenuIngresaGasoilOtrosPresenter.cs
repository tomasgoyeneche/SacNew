using SacNew.Repositories;
using SacNew.Services;
using SacNew.Views.GestionFlota.Postas.IngresaConsumos.IngresarConsumo;
using SacNew.Views.GestionFlota.Postas.YpfIngresaConsumos.IngresoManual;
using System.Globalization;

namespace SacNew.Presenters
{
    public class MenuIngresaGasoilOtrosPresenter : BasePresenter<IMenuIngresaGasoilOtrosView>
    {
        private readonly IEmpresaCreditoRepositorio _empresaCreditoRepositorio;
        private readonly IPOCRepositorio _pocRepositorio;
        private readonly INominaRepositorio _nominaRepositorio;

        public MenuIngresaGasoilOtrosPresenter(
            IEmpresaCreditoRepositorio empresaCreditoRepositorio,
            IPOCRepositorio pocRepositorio,
            INominaRepositorio nominaRepositorio,
            ISesionService sesionService,
            INavigationService navigationService
        ) : base(sesionService, navigationService)
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
                _view.IdPoc = poc.IdPOC;

                var nomina = await _nominaRepositorio.ObtenerPorIdAsync(poc.IdNomina)
                             ?? throw new Exception("No se encontró la nomina asociada al POC.");

                var empresaCredito = await _empresaCreditoRepositorio.ObtenerPorEmpresaAsync(nomina.idEmpresa)
                                     ?? throw new Exception("No se encontraron créditos para la empresa.");

                _view.CreditoTotal = empresaCredito.CreditoAsignado.ToString("C", new CultureInfo("es-AR"));
                _view.CreditoDisponible = empresaCredito.CreditoDisponible.ToString("C", new CultureInfo("es-AR"));
            });
        }

        //public async Task AbrirGasoilAutorizadoAsync(int idPoc)
        //{
        //    await AbrirFormularioAsync<IngresaGasoil>(async form =>
        //    {
        //        await form._presenter.CargarDatosAsync(idPoc);
        //    });
        //}

        public async Task AbrirConsumosenYpfEnRutaAsync(int idPoc)
        {
            await AbrirFormularioAsync<IngresoManualYPF>(async form =>
            {
                await form._presenter.CargarDatosAsync(idPoc);
            });
        }
    }
}