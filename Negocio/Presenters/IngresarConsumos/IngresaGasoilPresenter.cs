using Core.Base;
using Core.Repositories;
using Core.Services;
using SacNew.Views.GestionFlota.Postas.IngresaConsumos.ManualConsumos.IngresarConsumo;
using Shared.Models;

namespace GestionFlota.Presenters
{
    public class IngresaGasoilPresenter : BasePresenter<IIngresaGasoilView>
    {
        private readonly IConceptoRepositorio _conceptoRepositorio;
        private readonly IConsumoGasoilRepositorio _consumoGasoilRepositorio;
        private readonly IPOCRepositorio _pocRepositorio;
        private readonly IEmpresaCreditoRepositorio _empresaCreditoRepositorio;
        private int _idPoc;
        private decimal _capacidadTanque;
        private string _patente;
        private int _idPrograma;
        private decimal _kilometros;
        private decimal _autorizado;
        private EmpresaCredito _empresaCredito;
        private decimal _restanteAnterior;

        public IngresaGasoilPresenter(
            IConceptoRepositorio conceptoRepositorio,
            IConsumoGasoilRepositorio consumoGasoilRepositorio,
            IEmpresaCreditoRepositorio empresaCreditoRepositorio,
            IPOCRepositorio pocRepositorio,
            ISesionService sesionService,
            INavigationService navigationService
        ) : base(sesionService, navigationService)
        {
            _pocRepositorio = pocRepositorio;
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
                var tiposGasoil = (await Task.WhenAll(
                    _conceptoRepositorio.ObtenerPorTipoAsync(1),
                    _conceptoRepositorio.ObtenerPorTipoAsync(2)
                )).SelectMany(x => x).ToList();

                var tiposFiltrados = FiltrarTiposGasoil(tiposGasoil);
                _view.CargarTiposGasoil(tiposFiltrados);
                // Calcular litros autorizados (y actualizar el idPrograma)
                await CalcularLitrosAutorizadosAsync(_idPoc);

                // Una vez obtenido el idPrograma, cargar autorización anterior
                await CargarAutorizacionAnteriorAsync();
                await CargarAutorizacionActualAsync();
            });
        }

        public async Task CalcularLitrosAutorizadosAsync(int idPoc)
        {
            await EjecutarConCargaAsync(async () =>
            {
                (_patente, _capacidadTanque) = await _pocRepositorio.ObtenerUnidadPorPocAsync(idPoc);
                var programa = await _consumoGasoilRepositorio.ObtenerProgramaPorPatenteAsync(_patente);

                if (programa?.Kilometros > 0)
                {
                    _kilometros = programa.Value.Kilometros;
                    _autorizado = programa.Value.Kilometros * 35 / 100 * 2;
                    _idPrograma = programa.Value.IdPrograma;
                    var litrosCargados = await _consumoGasoilRepositorio.ObtenerLitrosCargadosPorProgramaAsync(programa.Value.IdPrograma);

                    _autorizado -= litrosCargados; // Descontar litros cargados

                    _view.MostrarLitrosAutorizados(_autorizado, _kilometros);
                }
                else
                {
                    _autorizado = _capacidadTanque;
                    _idPrograma = 0;
                    _view.MostrarMensaje("No se encontró un programa de combustible para esta unidad. Se usará la capacidad del tanque como límite.");
                }
            });
        }

        private List<Concepto> FiltrarTiposGasoil(IEnumerable<Concepto> tiposGasoil)
        {
            return _empresaCredito == null
                ? tiposGasoil.Where(tipo =>
                    !tipo.Descripcion.Contains("Chenyi", StringComparison.OrdinalIgnoreCase) &&
                    !tipo.Descripcion.Contains("Autorizado", StringComparison.OrdinalIgnoreCase)).ToList()
                : _empresaCredito.IdEmpresa == 1
                    ? tiposGasoil.Where(tipo => tipo.Descripcion.Contains("Chenyi", StringComparison.OrdinalIgnoreCase)).ToList()
                    : tiposGasoil.Where(tipo => tipo.Descripcion.Contains("Autorizado", StringComparison.OrdinalIgnoreCase)).ToList();
        }

        public void CalcularTotal(decimal litros)
        {
            var tipoSeleccionado = _view.TipoGasoilSeleccionado;
            if (tipoSeleccionado == null)
            {
                _view.MostrarMensaje("Debe seleccionar un tipo de gasoil.");
                return;
            }

            var total = litros * tipoSeleccionado.PrecioActual;
            _view.MostrarTotalCalculado(total);
        }

        public async Task GuardarConsumoAsync()
        {
            if (_view.Litros.Value > _autorizado && !_view.ConfirmarGuardado("El consumo excede el autorizado, ¿desea guardar de todos modos?"))
            {
                return;
            }

            await EjecutarConCargaAsync(async () =>
            {
                if (!ValidarDatos()) return;

                var tipoSeleccionado = _view.TipoGasoilSeleccionado;
                var precioTotal = _view.Litros.Value * tipoSeleccionado.PrecioActual;

                if (VerificarCreditoInsuficiente(precioTotal)) return;

                var consumo = CrearConsumo(precioTotal, tipoSeleccionado.IdConsumo);

                await _consumoGasoilRepositorio.AgregarConsumoAsync(consumo);
                await ActualizarCreditoAsync(precioTotal);

                _view.MostrarMensaje("Consumo de gasoil guardado correctamente.");
                _view.Cerrar();
            });
        }

        private ConsumoGasoil CrearConsumo(decimal precioTotal, int idConsumo)
        {
            return new ConsumoGasoil
            {
                IdPOC = _idPoc,
                IdConsumo = idConsumo,
                IdPrograma = _idPrograma,
                LitrosAutorizados = _autorizado,
                NumeroVale = _view.NumeroVale,
                LitrosCargados = _view.Litros.Value,
                PrecioTotal = precioTotal,
                Observaciones = _view.Observaciones,
                FechaCarga = _view.FechaCarga,
                Activo = true
            };
        }

        private async Task ActualizarCreditoAsync(decimal precioTotal)
        {
            _empresaCredito.CreditoConsumido += precioTotal;
            await _empresaCreditoRepositorio.ActualizarCreditoAsync(_empresaCredito);
        }

        private bool ValidarDatos()
        {
            if (_view.Litros.Value > _capacidadTanque)
            {
                _view.MostrarMensaje("Estas cargando mas litros que la capacidad del tanque");
                return false;
            }
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
            var creditoRestante = _empresaCredito.CreditoDisponible;
            if (precioTotal > creditoRestante)
            {
                _view.MostrarMensaje($"El crédito disponible ({creditoRestante:C}) no es suficiente para este consumo.");
                return true;
            }
            return false;
        }

        public async Task CargarAutorizacionAnteriorAsync()
        {
            await EjecutarConCargaAsync(async () =>
            {
                // Obtener el idPrograma anterior más cercano
                var idProgramaAnterior = await _consumoGasoilRepositorio.ObtenerIdProgramaAnteriorAsync(_patente, _idPrograma);

                if (!idProgramaAnterior.HasValue)
                {
                    _view.MostrarMensaje("No se encontró un programa anterior.");
                    _view.MostrarConsumosAnteriores(new List<ConsumoGasoilAutorizadoDto>());
                    _view.ActualizarLabelAnterior(0);
                    return;
                }

                // Obtener los consumos del idPrograma anterior
                var consumosAnteriores = await _consumoGasoilRepositorio.ObtenerConsumosPorProgramaAsync(idProgramaAnterior.Value);

                // Calcular el restante
                _restanteAnterior = consumosAnteriores.FirstOrDefault()?.LitrosAutorizados ?? 0;
                _restanteAnterior -= consumosAnteriores.Sum(c => c.LitrosCargados);

                _view.MostrarConsumosAnteriores(consumosAnteriores);
                _view.ActualizarLabelAnterior(_restanteAnterior);
            });
        }

        public async Task CargarAutorizacionActualAsync()
        {
            await EjecutarConCargaAsync(async () =>
            {
                // Obtener consumos del programa actual
                var consumosActuales = await _consumoGasoilRepositorio.ObtenerConsumosPorProgramaAsync(_idPrograma);

                // Calcular restante del programa actual
                var totalCargadoActual = consumosActuales.Sum(c => c.LitrosCargados);
                var restanteActual = _autorizado - totalCargadoActual;

                // Mostrar consumos del programa actual
                _view.MostrarConsumosTotales(consumosActuales);

                // Actualizar etiqueta de restante total
                var restanteTotal = restanteActual + _restanteAnterior;
                _view.ActualizarLabelTotal(restanteTotal);
            });
        }
    }
}