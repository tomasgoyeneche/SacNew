using SacNew.Models;
using SacNew.Repositories;
using SacNew.Services;
using SacNew.Views.GestionFlota.Postas.IngresaConsumos.ManualConsumos.IngresarConsumo;

namespace SacNew.Presenters
{
    public class IngresaGasoilPresenter : BasePresenter<IIngresaGasoilView>
    {
        private readonly IConceptoRepositorio _conceptoRepositorio;
        private readonly IConsumoGasoilRepositorio _consumoGasoilRepositorio;
        private readonly IEmpresaCreditoRepositorio _empresaCreditoRepositorio;
        private int _idPoc;
        private EmpresaCredito _empresaCredito;

        public IngresaGasoilPresenter(
            IConceptoRepositorio conceptoRepositorio,
            IConsumoGasoilRepositorio consumoGasoilRepositorio,
            IEmpresaCreditoRepositorio empresaCreditoRepositorio,
            ISesionService sesionService,
            INavigationService navigationService
        ) : base(sesionService, navigationService)
        {
            _conceptoRepositorio = conceptoRepositorio;
            _consumoGasoilRepositorio = consumoGasoilRepositorio;
            _empresaCreditoRepositorio = empresaCreditoRepositorio;
        }

        public async Task CargarDatosAsync(int idPoc, EmpresaCredito empresaCredito)
        {
            _idPoc = idPoc;
            _empresaCredito = empresaCredito;

            await EjecutarConCargaAsync(async () =>
            {
                var tiposGasoil = await Task.WhenAll(
                    _conceptoRepositorio.ObtenerPorTipoAsync(1),
                    _conceptoRepositorio.ObtenerPorTipoAsync(2)
                );

                var todosLosTiposGasoil = tiposGasoil.SelectMany(x => x).ToList();
                _view.CargarTiposGasoil(todosLosTiposGasoil);
            });
        }

        public void CalcularTotal(decimal litros)
        {
            var tipoSeleccionado = _view.TipoGasoilSeleccionado;
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

                var tipoSeleccionado = _view.TipoGasoilSeleccionado;
                var precioTotal = _view.Litros.Value * tipoSeleccionado.PrecioActual;

                if (VerificarCreditoInsuficiente(precioTotal)) return;

                var consumo = new ConsumoGasoil
                {
                    IdPOC = _idPoc,
                    IdConsumo = tipoSeleccionado.IdConsumo,
                    NumeroVale = _view.NumeroVale,
                    LitrosCargados = _view.Litros.Value,
                    PrecioTotal = precioTotal,
                    Observaciones = _view.Observaciones,
                    FechaCarga = _view.FechaCarga,
                    Activo = true
                };

                await _consumoGasoilRepositorio.AgregarConsumoAsync(consumo);
                ActualizarCredito(precioTotal);

                _view.MostrarMensaje("Consumo de gasoil guardado correctamente.");
                _view.Cerrar();
            });
        }

        private bool ValidarDatos()
        {
            if (_view.TipoGasoilSeleccionado == null)
            {
                _view.MostrarMensaje("Debe seleccionar un tipo de gasoil.");
                return false;
            }

            if (!_view.Litros.HasValue || _view.Litros <= 0)
            {
                _view.MostrarMensaje("Debe ingresar un valor válido para los litros.");
                return false;
            }

            if (string.IsNullOrWhiteSpace(_view.NumeroVale))
            {
                _view.MostrarMensaje("Debe ingresar el número de vale.");
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