using Core.Base;
using Core.Repositories;
using Core.Services;
using GestionFlota.Views.Postas.IngresaConsumos.ManualConsumos.IngresarConsumo;
using Shared.Models;

namespace GestionFlota.Presenters.IngresarConsumos
{
    public class IngresaOtrosConsumosPresenter : BasePresenter<IOtrosConsumosView>
    {
        private readonly IConceptoRepositorio _conceptoRepositorio;
        private readonly IEmpresaCreditoRepositorio _empresaCreditoRepositorio;
        private readonly IConsumoOtrosRepositorio _consumoOtrosRepositorio;

        private int _idPoc;
        private EmpresaCredito _empresaCredito;

        public IngresaOtrosConsumosPresenter(
            IConceptoRepositorio conceptoRepositorio,
            IEmpresaCreditoRepositorio empresaCreditoRepositorio,
            IConsumoOtrosRepositorio consumoOtrosRepositorio,
            ISesionService sesionService,
            INavigationService navigationService
        ) : base(sesionService, navigationService)
        {
            _conceptoRepositorio = conceptoRepositorio;
            _empresaCreditoRepositorio = empresaCreditoRepositorio;
            _consumoOtrosRepositorio = consumoOtrosRepositorio;
        }

        public async Task CargarDatosAsync(int idPoc, EmpresaCredito empresaCredito)
        {
            _idPoc = idPoc;
            _empresaCredito = empresaCredito;

            await EjecutarConCargaAsync(async () =>
            {
                var tiposConsumo = await Task.WhenAll(
                    _conceptoRepositorio.ObtenerPorTipoAsync(3),
                    _conceptoRepositorio.ObtenerPorTipoAsync(5)
                );

                var todosLosTiposConsumo = tiposConsumo.SelectMany(x => x).ToList();
                _view.CargarTiposConsumo(todosLosTiposConsumo);
            });
        }

        public void CalcularTotal(decimal litros)
        {
            var tipoSeleccionado = _view.TipoConsumoSeleccionado;
            if (tipoSeleccionado == null)
            {
                _view.MostrarMensaje("Debe seleccionar un tipo de gasoil.");
                return;
            }

            _view.MostrarTotalCalculado(litros * tipoSeleccionado.PrecioActual);
        }

        public async Task GuardarConsumoAsync()
        {
            await EjecutarConCargaAsync(async () =>
            {
                if (!ValidarDatos()) return;

                var tipoSeleccionado = _view.TipoConsumoSeleccionado;
                var precioTotal = _view.Cantidad.Value * tipoSeleccionado.PrecioActual;

                if (VerificarCreditoInsuficiente(precioTotal)) return;

                var consumo = new ConsumoOtros
                {
                    IdPOC = _idPoc,
                    IdConsumo = tipoSeleccionado.IdConsumo,
                    NumeroVale = _view.RemitoExterno,
                    Cantidad = _view.Cantidad.Value,
                    ImporteTotal = precioTotal,
                    Aclaracion = _view.Aclaraciones,
                    FechaRemito = _view.FechaRemito,
                    Activo = true
                };

                await _consumoOtrosRepositorio.AgregarConsumoAsync(consumo);
                ActualizarCredito(precioTotal);

                _view.MostrarMensaje("Consumo de gasoil guardado correctamente.");
                _view.Cerrar();
            });
        }

        private bool ValidarDatos()
        {
            if (_view.TipoConsumoSeleccionado == null)
            {
                _view.MostrarMensaje("Debe seleccionar un tipo de gasoil.");
                return false;
            }

            if (!_view.Cantidad.HasValue || _view.Cantidad <= 0 || _view.Cantidad > 10)
            {
                _view.MostrarMensaje("Debe ingresar un valor válido para la cantidad.");
                return false;
            }

            if (string.IsNullOrWhiteSpace(_view.RemitoExterno))
            {
                _view.MostrarMensaje("Debe ingresar el número de Remito.");
                return false;
            }

            return true;
        }

        private bool VerificarCreditoInsuficiente(decimal precioTotal)
        {
            if (_empresaCredito.CreditoConsumido + precioTotal > _empresaCredito.CreditoDisponible)
            {
                _view.MostrarMensaje("El crédito disponible no es suficiente para este consumo.");
                return true;
            }
            return false;
        }

        private async void ActualizarCredito(decimal precioTotal)
        {
            _empresaCredito.CreditoConsumido += precioTotal;
            await _empresaCreditoRepositorio.ActualizarCreditoAsync(_empresaCredito);
        }
    }
}