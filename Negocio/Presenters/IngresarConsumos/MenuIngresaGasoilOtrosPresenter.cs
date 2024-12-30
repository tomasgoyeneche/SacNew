using Core.Base;
using Core.Repositories;
using Core.Services;
using GestionFlota.Views.Postas.IngresaConsumos.ManualConsumos.IngresarConsumo;
using SacNew.Views.GestionFlota.Postas.IngresaConsumos.IngresarConsumo;
using SacNew.Views.GestionFlota.Postas.YpfIngresaConsumos.IngresoManual;
using Shared.Models;
using System.Globalization;

namespace GestionFlota.Presenters
{
    public class MenuIngresaGasoilOtrosPresenter : BasePresenter<IMenuIngresaGasoilOtrosView>
    {
        private readonly IEmpresaCreditoRepositorio _empresaCreditoRepositorio;
        private readonly IPOCRepositorio _pocRepositorio;
        private readonly IUnidadRepositorio _unidadRepositorio;
        private readonly IConsumoOtrosRepositorio _consumoOtrosRepositorio;
        private EmpresaCredito _amount;

        public MenuIngresaGasoilOtrosPresenter(
            IEmpresaCreditoRepositorio empresaCreditoRepositorio,
            IPOCRepositorio pocRepositorio,
            IUnidadRepositorio unidadRepositorio,
            ISesionService sesionService,
            IConsumoOtrosRepositorio consumoOtrosRepositorio,
            INavigationService navigationService
        ) : base(sesionService, navigationService)
        {
            _consumoOtrosRepositorio = consumoOtrosRepositorio;
            _empresaCreditoRepositorio = empresaCreditoRepositorio ?? throw new ArgumentNullException(nameof(empresaCreditoRepositorio));
            _pocRepositorio = pocRepositorio ?? throw new ArgumentNullException(nameof(pocRepositorio));
            _unidadRepositorio = unidadRepositorio ?? throw new ArgumentNullException(nameof(unidadRepositorio));
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

                _view.NumeroPoc = poc.NumeroPoc;
                _view.IdPoc = poc.IdPoc;

                var unidad = await _unidadRepositorio.ObtenerPorIdAsync(poc.IdUnidad)
                             ?? throw new Exception("No se encontró la nomina asociada al POC.");

                var empresaCredito = await _empresaCreditoRepositorio.ObtenerPorEmpresaAsync(unidad.idEmpresa);
                var consumos = await _consumoOtrosRepositorio.ObtenerPorPocAsync(idPoc);
                var total = consumos.Sum(c => c.ImporteTotal ?? 0);

                if (empresaCredito != null)
                {
                    _amount = empresaCredito;
                    _view.CreditoEnPoc = total.ToString("C", new CultureInfo("es-AR"));
                    _view.CreditoTotal = empresaCredito.CreditoAsignado.ToString("C", new CultureInfo("es-AR"));
                    _view.CreditoDisponible = empresaCredito.CreditoDisponible.ToString("C", new CultureInfo("es-AR"));
                    _view.CreditoConsumido = empresaCredito.CreditoConsumido.ToString("C", new CultureInfo("es-AR"));
                }
                else
                {
                    _amount = null;
                    _view.MostrarMensaje("No se encontraron creditos para esta empresa");
                }

                _view.MostrarConsumos(consumos);
            });
        }

        public async Task AbrirGasoilAutorizadoAsync(int idPoc)
        {
            await AbrirFormularioAsync<IngresaGasoil>(async form =>
            {
                await form._presenter.CargarDatosAsync(idPoc, _amount);
            });
        }

        public async Task AbrirConsumosenYpfEnRutaAsync(int idPoc)
        {
            await AbrirFormularioAsync<IngresoManualYPF>(async form =>
            {
                await form._presenter.CargarDatosAsync(idPoc);
            });
        }

        public async Task CerrarPocAsync(int idPoc, DateTime fechaCierre)
        {
            if (idPoc <= 0)
            {
                _view.MostrarMensaje("El ID del POC no es válido.");
                return;
            }

            await EjecutarConCargaAsync(async () =>
            {
                // Actualizar el POC en la base de datos
                await _pocRepositorio.ActualizarFechaCierreYEstadoAsync(idPoc, fechaCierre, "cerrada");

                // Notificar al usuario
                _view.MostrarMensaje("La POC se ha cerrado exitosamente.");
            });
        }

        public async Task AbrirOtrosConsumos(int idPoc)
        {
            await AbrirFormularioAsync<OtrosConsumosForm>(async form =>
            {
                await form._presenter.CargarDatosAsync(idPoc, _amount);
            });
        }
    }
}