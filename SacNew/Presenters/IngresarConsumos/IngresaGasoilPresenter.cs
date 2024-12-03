using SacNew.Models;
using SacNew.Repositories;
using SacNew.Services;
using SacNew.Views.GestionFlota.Postas.IngresaConsumos.IngresarConsumo;
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
                var tiposGasoil = (await _conceptoRepositorio.ObtenerPorTipoAsync(1)).ToList();
                _view.CargarTiposGasoil(tiposGasoil);
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

            var precioActual = tipoSeleccionado.PrecioActual;
            var total = litros * precioActual;
            _view.MostrarTotalCalculado(total);
        }

        public async Task GuardarConsumoAsync()
        {
            await EjecutarConCargaAsync(async () =>
            {
                // Validar datos ingresados
                var tipoSeleccionado = _view.TipoGasoilSeleccionado;
                if (tipoSeleccionado == null)
                {
                    _view.MostrarMensaje("Debe seleccionar un tipo de gasoil.");
                    return;
                }

                if (!_view.Litros.HasValue || _view.Litros <= 0)
                {
                    _view.MostrarMensaje("Debe ingresar un valor válido para los litros.");
                    return;
                }

                if (string.IsNullOrWhiteSpace(_view.NumeroVale))
                {
                    _view.MostrarMensaje("Debe ingresar el número de vale.");
                    return;
                }

                // Calcular el precio total
                var precioTotal = _view.Litros.Value * tipoSeleccionado.PrecioActual;

                if (_empresaCredito.CreditoConsumido + precioTotal > _empresaCredito.CreditoDisponible)
                {
                    _view.MostrarMensaje("El crédito disponible no es suficiente para este consumo.");
                    return;
                }

                // Crear entidad ConsumoGasoil
                var consumo = new ConsumoGasoil
                {
                    IdPOC = _idPoc,
                    IdConsumo = tipoSeleccionado.IdConsumo,
                    NumeroVale = _view.NumeroVale,
                    LitrosCargados = _view.Litros.Value,
                    PrecioTotal = _view.Litros.Value * tipoSeleccionado.PrecioActual,
                    Observaciones = _view.Observaciones,
                    FechaCarga = _view.FechaCarga,
                    Activo = true
                };



                // Guardar en repositorio
                await _consumoGasoilRepositorio.AgregarConsumoAsync(consumo);

                _empresaCredito.CreditoConsumido += precioTotal;
                await _empresaCreditoRepositorio.ActualizarCreditoAsync(_empresaCredito);

                _view.MostrarMensaje("Consumo de gasoil guardado correctamente.");
                _view.Cerrar();
            });
        }
    }
}