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
        private decimal _restanteAnterior;
        private EmpresaCredito _empresaCredito;

        public IngresaGasoilPresenter(
            IConceptoRepositorio conceptoRepositorio,
            IConsumoGasoilRepositorio consumoGasoilRepositorio,
            IEmpresaCreditoRepositorio empresaCreditoRepositorio,
            IPOCRepositorio pocRepositorio,
            ISesionService sesionService,
            INavigationService navigationService
        ) : base(sesionService, navigationService)
        {
            _conceptoRepositorio = conceptoRepositorio;
            _consumoGasoilRepositorio = consumoGasoilRepositorio;
            _empresaCreditoRepositorio = empresaCreditoRepositorio;
            _pocRepositorio = pocRepositorio;
        }

        public async Task CargarDatosAsync(int idPoc, EmpresaCredito empresaCredito)
        {
            _idPoc = idPoc;
            _empresaCredito = empresaCredito;

            await EjecutarConCargaAsync(async () =>
            {
                await CargarTiposGasoilAsync();
                await CalcularLitrosAutorizadosAsync();
                await CargarAutorizacionAnteriorAsync();
                await CargarAutorizacionActualAsync();
            });
        }

        private async Task CargarTiposGasoilAsync()
        {
            var tiposGasoil = (await Task.WhenAll(
                _conceptoRepositorio.ObtenerPorTipoAsync(1),
                _conceptoRepositorio.ObtenerPorTipoAsync(2)
            )).SelectMany(x => x).ToList();

            var tiposFiltrados = FiltrarTiposGasoil(tiposGasoil);
            _view.CargarTiposGasoil(tiposFiltrados);
        }

        private List<Concepto> FiltrarTiposGasoil(IEnumerable<Concepto> tiposGasoil)
        {
            if (_empresaCredito == null)
            {
                return tiposGasoil.Where(tipo =>
                    !tipo.Descripcion.Contains("Chenyi", StringComparison.OrdinalIgnoreCase) &&
                    !tipo.Descripcion.Contains("Autorizado", StringComparison.OrdinalIgnoreCase)).ToList();
            }

            return _empresaCredito.IdEmpresa == 1
                ? tiposGasoil.Where(tipo => tipo.Descripcion.Contains("Chenyi", StringComparison.OrdinalIgnoreCase)).ToList()
                : tiposGasoil.Where(tipo => tipo.Descripcion.Contains("Autorizado", StringComparison.OrdinalIgnoreCase)).ToList();
        }

        private async Task CalcularLitrosAutorizadosAsync()
        {
            (_patente, _capacidadTanque) = await _pocRepositorio.ObtenerUnidadPorPocAsync(_idPoc);
            var programa = await _consumoGasoilRepositorio.ObtenerProgramaPorPatenteAsync(_patente);


            // Validar si estamos en Bahía Blanca y el último consumo fue en Bahía Blanca
         

            if (programa?.Kilometros > 0)
            {
                _kilometros = programa.Value.Kilometros;
                _autorizado = _empresaCredito.IdEmpresa == 1
                    ? _kilometros * 32 / 100 * 2
                    : _kilometros * 35 / 100 * 2;

                _idPrograma = programa.Value.IdPrograma;
                var litrosCargados = await _consumoGasoilRepositorio.ObtenerLitrosCargadosPorProgramaAsync(_idPrograma);
                _autorizado -= litrosCargados;

                _view.MostrarLitrosAutorizados(_autorizado, _kilometros);

            }
            else
            {
                _autorizado = _capacidadTanque;
                _idPrograma = 0;
                _view.MostrarMensaje("No se encontró un programa válido, se usara como maximo la capacidad del tanque.");
            }


            if (_sesionService.IdPosta == 2 /*si el programa no es nulo -> && programa != null*/)
            {
                var ultimoConsumo = await _consumoGasoilRepositorio.ObtenerUltimoConsumoPorPatenteAsync(_patente);

                if (ultimoConsumo != null && ultimoConsumo.NumeroPoc.StartsWith("BB"))
                {
                    // Usar el programa anterior

                    _idPrograma = ultimoConsumo.IdPrograma;
                    _autorizado = ultimoConsumo.LitrosAutorizados - ultimoConsumo.LitrosCargados;
                    _view.MostrarLitrosAutorizados(_autorizado, 1000);
                }
            }

        }

        private async Task CargarAutorizacionAnteriorAsync()
        {
            var idProgramaAnterior = await _consumoGasoilRepositorio.ObtenerIdProgramaAnteriorAsync(_patente, _idPrograma);

            if (!idProgramaAnterior.HasValue)
            {
                _view.MostrarMensaje("No se encontró un programa anterior.");
                _view.MostrarConsumosAnteriores(new List<ConsumoGasoilAutorizadoDto>());
                _view.ActualizarLabelAnterior(0);
                return;
            }

            var consumosAnteriores = await _consumoGasoilRepositorio.ObtenerConsumosPorProgramaAsync(idProgramaAnterior.Value, _patente);
            _restanteAnterior = consumosAnteriores.FirstOrDefault()?.LitrosAutorizados ?? 0;
            _restanteAnterior -= consumosAnteriores.Sum(c => c.LitrosCargados);

            _view.MostrarConsumosAnteriores(consumosAnteriores);
            _view.ActualizarLabelAnterior(_restanteAnterior);
        }

        private async Task CargarAutorizacionActualAsync()
        {
            if(_idPrograma != 0)
            {
                var consumosActuales = await _consumoGasoilRepositorio.ObtenerConsumosPorProgramaAsync(_idPrograma, _patente);

                var totalCargadoActual = consumosActuales.Sum(c => c.LitrosCargados);
                var restanteActual = _autorizado - totalCargadoActual;
                var restanteTotal = restanteActual + _restanteAnterior;

                _view.MostrarConsumosTotales(consumosActuales);
                _view.ActualizarLabelTotal(restanteTotal);
            }
            else
            {
                _view.MostrarMensaje("No se carga autorizado actual ya que no hay");
                _view.MostrarConsumosTotales(new List<ConsumoGasoilAutorizadoDto>());
                _view.ActualizarLabelTotal(0);
            }
     
        }

        public void CalcularTotal(decimal litros)
        {
            var tipoSeleccionado = _view.TipoGasoilSeleccionado;

            var total = litros * tipoSeleccionado.PrecioActual;
            _view.MostrarTotalCalculado(total);
        }

        public async Task GuardarConsumoAsync()
        {
            if (!_view.Litros.HasValue || (_view.Litros.Value > _autorizado && !_view.ConfirmarGuardado("El consumo excede el autorizado, ¿desea guardar de todos modos?")))
            {
                return;
            }

            await EjecutarConCargaAsync(async () =>
            {
                var tipoSeleccionado = _view.TipoGasoilSeleccionado;
                var precioTotal = _view.Litros.Value * tipoSeleccionado.PrecioActual;

                if (VerificarCreditoInsuficiente(precioTotal)) return;

                var consumo = CrearConsumo(precioTotal, tipoSeleccionado.IdConsumo);

                if (!await ValidarAsync(consumo, _capacidadTanque, _autorizado))
                {
                    return; // Detener si la validación falla
                }
                await _consumoGasoilRepositorio.AgregarConsumoAsync(consumo);
                await ActualizarCreditoAsync(precioTotal);

                _view.MostrarMensaje("Consumo guardado correctamente.");
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

        private bool VerificarCreditoInsuficiente(decimal precioTotal)
        {
            if (precioTotal > _empresaCredito.CreditoDisponible)
            {
                _view.MostrarMensaje($"El crédito disponible ({_empresaCredito.CreditoDisponible:C}) no es suficiente.");
                return true;
            }
            return false;
        }
    }
}