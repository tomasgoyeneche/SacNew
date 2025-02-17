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
        private readonly IConsumoGasoilRepositorio _consumoGasoilRepositorio;
        private EmpresaCredito _amount;

        public MenuIngresaGasoilOtrosPresenter(
            IEmpresaCreditoRepositorio empresaCreditoRepositorio,
            IPOCRepositorio pocRepositorio,
            IUnidadRepositorio unidadRepositorio,
            ISesionService sesionService,
            IConsumoOtrosRepositorio consumoOtrosRepositorio,
            IConsumoGasoilRepositorio consumoGasoilRepositorio,
            INavigationService navigationService
        ) : base(sesionService, navigationService)
        {
            _consumoOtrosRepositorio = consumoOtrosRepositorio;
            _empresaCreditoRepositorio = empresaCreditoRepositorio ?? throw new ArgumentNullException(nameof(empresaCreditoRepositorio));
            _pocRepositorio = pocRepositorio ?? throw new ArgumentNullException(nameof(pocRepositorio));
            _unidadRepositorio = unidadRepositorio ?? throw new ArgumentNullException(nameof(unidadRepositorio));
            _consumoGasoilRepositorio = consumoGasoilRepositorio;
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

                _amount = await _empresaCreditoRepositorio.ObtenerPorEmpresaAsync(unidad.idEmpresa);
                var consumos = await _consumoOtrosRepositorio.ObtenerPorPocAsync(idPoc);
                var total = consumos.Sum(c => c.ImporteTotal ?? 0);

                if (_amount != null)
                {
                    _view.CreditoEnPoc = total.ToString("C", new CultureInfo("es-AR"));
                    _view.CreditoTotal = _amount.CreditoAsignado.ToString("C", new CultureInfo("es-AR"));
                    _view.CreditoDisponible = _amount.CreditoDisponible.ToString("C", new CultureInfo("es-AR"));
                    _view.CreditoConsumido = _amount.CreditoConsumido.ToString("C", new CultureInfo("es-AR"));
                }
                else
                {
                    _view.MostrarMensaje("No se encontraron créditos para esta empresa.");
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
            await CargarDatosAsync(_view.IdPoc);
        }

        public async Task AbrirConsumosenYpfEnRutaAsync(int idPoc)
        {
            await AbrirFormularioAsync<IngresoManualYPF>(async form =>
            {
                await form._presenter.CargarDatosAsync(idPoc);
            });
            await CargarDatosAsync(_view.IdPoc);
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

            await CargarDatosAsync(_view.IdPoc);
        }

        public async Task EliminarConsumo(int idConsumo, int tipoConsumo, decimal importeTotal)
        {
            await EjecutarConCargaAsync(async () =>
            {
                // Determinar el repositorio correcto
                if (tipoConsumo == 1)
                {
                    await _consumoGasoilRepositorio.EliminarConsumoAsync(idConsumo);
                }
                else if (tipoConsumo == 2)
                {
                    await _consumoOtrosRepositorio.EliminarConsumoAsync(idConsumo);
                }
                else
                {
                    _view.MostrarMensaje("Tipo de consumo desconocido.");
                    return;
                }

                // Restar el importe eliminado al crédito consumido
                if (_amount != null)
                {
                    _amount.CreditoConsumido -= importeTotal;

                    // Actualizar en la base de datos
                    await _empresaCreditoRepositorio.ActualizarCreditoAsync(_amount);
                }

                // Refrescar la vista
                await CargarDatosAsync(_view.IdPoc);
                _view.MostrarMensaje("Consumo eliminado correctamente.");
            });
        }

        public async Task EditarConsumoOtros(int idPoc, int idConsumo, int tipoConsumo)
        {
            if (tipoConsumo == 2)
            {
                await AbrirFormularioAsync<OtrosConsumosForm>(async form =>
                {
                    await form._presenter.CargarDatosParaEditarAsync(idPoc, idConsumo, _amount);
                });
            }
            else if (tipoConsumo == 1)
            {
                await AbrirFormularioAsync<IngresaGasoil>(async form =>
                {
                    await form._presenter.CargarDatosParaEditarAsync(idPoc, idConsumo, _amount);
                });
            }

            await CargarDatosAsync(_view.IdPoc);
        }
    }
}