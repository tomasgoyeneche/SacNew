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

        private int? _idConsumo;
        private int _idPoc;
        private int _idPrograma;
        private decimal _capacidadTanque;
        private string _patente;
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

        // ==========================
        // MÉTODOS PÚBLICOS PRINCIPALES
        // ==========================

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

        public async Task CargarDatosParaEditarAsync(int idPoc, int idConsumo, EmpresaCredito empresaCredito)
        {
            _idPoc = idPoc;
            _idConsumo = idConsumo;
            _empresaCredito = empresaCredito;

            await EjecutarConCargaAsync(async () =>
            {
                var consumo = await _consumoGasoilRepositorio.ObtenerPorIdAsync(idConsumo);
                if (consumo == null)
                {
                    _view.MostrarMensaje("No se encontró el consumo seleccionado.");
                    return;
                }

                _autorizado = consumo.LitrosAutorizados;
                _idPrograma = consumo.IdPrograma;
                (_patente, _capacidadTanque) = await _pocRepositorio.ObtenerUnidadPorPocAsync(_idPoc);

                await CargarTiposGasoilAsync();
                _view.InicializarParaEdicion(consumo);
                await CargarAutorizacionAnteriorAsync();
                await CargarAutorizacionActualAsync();
            });
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
                var nuevoPrecioTotal = _view.Litros.Value * tipoSeleccionado.PrecioActual;

                if (VerificarCreditoInsuficiente(nuevoPrecioTotal)) return;

                ConsumoGasoil consumo;

                if (_idConsumo == null)
                {
                    consumo = await CrearNuevoConsumoAsync(nuevoPrecioTotal, tipoSeleccionado.IdConsumo);
                    if (!await ValidarAsync(consumo, _capacidadTanque, _autorizado))
                        return;
                    await _consumoGasoilRepositorio.AgregarConsumoAsync(consumo);
                }
                else
                {
                    consumo = await ActualizarConsumoExistente(nuevoPrecioTotal, tipoSeleccionado.IdConsumo);
                    if (!await ValidarAsync(consumo, _capacidadTanque, _autorizado))
                        return;
                    await _consumoGasoilRepositorio.ActualizarConsumoAsync(consumo);
                }

                if (consumo != null)
                {
                    await ActualizarCreditoAsync(nuevoPrecioTotal, _idConsumo == null ? 0 : consumo.PrecioTotal);
                    _view.MostrarMensaje("Consumo guardado correctamente.");
                    _view.Cerrar();
                }
            });
        }

        // ==========================
        // MÉTODOS PRIVADOS DE CÁLCULO Y UTILIDAD
        // ==========================

        private async Task CalcularLitrosAutorizadosAsync()
        {
            (_patente, _capacidadTanque) = await _pocRepositorio.ObtenerUnidadPorPocAsync(_idPoc);
            var programa = await _consumoGasoilRepositorio.ObtenerProgramaPorPatenteAsync(_patente);
            bool validaPorBahiaBlanca = false;
            bool programaValido = programa?.Kilometros > 0;

            // Validar si estamos en Bahía Blanca y el último consumo fue en Bahía Blanca

            if (_sesionService.IdPosta == 2 /*si el programa no es nulo -> && programa != null*/)
            {
                var ultimoConsumo = await _consumoGasoilRepositorio.ObtenerUltimoConsumoPorPatenteAsync(_patente);

                if (ultimoConsumo != null && ultimoConsumo.NumeroPoc.StartsWith("BB"))
                {
                    // Usar el programa anterior

                    _idPrograma = ultimoConsumo.IdPrograma;
                    _autorizado = ultimoConsumo.LitrosAutorizados - ultimoConsumo.LitrosCargados;
                    _view.MostrarLitrosAutorizados(_autorizado, 640);
                    validaPorBahiaBlanca = true;
                }
            }

            if (programaValido && !validaPorBahiaBlanca)
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

            if (!programaValido && !validaPorBahiaBlanca)
            {
                // Caso sin programa válido y sin Bahía Blanca
                _autorizado = _capacidadTanque;
                _idPrograma = 0;
                _view.MostrarMensaje("No se encontró un programa válido. Se usará como máximo la capacidad del tanque.");
            }
        }

        private async Task CargarAutorizacionAnteriorAsync()
        {
            int idProgramaParaBusqueda = _idPrograma;
            var idProgramaAnterior = await _consumoGasoilRepositorio.ObtenerIdProgramaAnteriorAsync(_patente, idProgramaParaBusqueda);

            if (idProgramaAnterior == null || idProgramaAnterior == 0)
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
            if (_idPrograma == 0)
            {
                _view.MostrarMensaje("No se carga autorizado actual ya que no hay");
                _view.MostrarConsumosTotales(new List<ConsumoGasoilAutorizadoDto>());
                _view.ActualizarLabelTotal(0);
                return;
            }

            var consumosActuales = _idConsumo == null
                ? await _consumoGasoilRepositorio.ObtenerConsumosPorProgramaAsync(_idPrograma, _patente)
                : await _consumoGasoilRepositorio.ObtenerConsumosPorProgramaEditableAsync(_idConsumo.Value, _idPrograma, _patente);

            var restanteTotal = _autorizado + _restanteAnterior;
            _view.MostrarConsumosTotales(consumosActuales);
            _view.ActualizarLabelTotal(restanteTotal);
        }

        private async Task<ConsumoGasoil> CrearNuevoConsumoAsync(decimal precioTotal, int idConsumo)
        {
            var nuevoConsumo = new ConsumoGasoil
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

            // Guardar en la base de datos

            return nuevoConsumo;
        }

        private async Task<ConsumoGasoil?> ActualizarConsumoExistente(decimal nuevoPrecioTotal, int idConsumo)
        {
            var consumo = await _consumoGasoilRepositorio.ObtenerPorIdAsync(_idConsumo.Value);
            if (consumo == null) return null;

            consumo.IdConsumo = idConsumo;
            consumo.NumeroVale = _view.NumeroVale;
            consumo.LitrosCargados = _view.Litros.Value;
            consumo.PrecioTotal = nuevoPrecioTotal;
            consumo.Observaciones = _view.Observaciones;
            consumo.FechaCarga = _view.FechaCarga;

            return consumo;
        }

        private async Task CargarTiposGasoilAsync()
        {
            var tiposGasoil = (await Task.WhenAll(
                _conceptoRepositorio.ObtenerPorTipoAsync(1),
                _conceptoRepositorio.ObtenerPorTipoAsync(2)
            )).SelectMany(x => x).ToList();

            _view.CargarTiposGasoil(FiltrarTiposGasoil(tiposGasoil));
        }

        private List<Concepto> FiltrarTiposGasoil(IEnumerable<Concepto> tiposGasoil)
        {
            return _empresaCredito?.IdEmpresa == 1
                ? tiposGasoil.Where(tipo => tipo.Descripcion.Contains("Chenyi", StringComparison.OrdinalIgnoreCase)).ToList()
                : tiposGasoil.Where(tipo => tipo.Descripcion.Contains("Autorizado", StringComparison.OrdinalIgnoreCase)).ToList();
        }

        public void CalcularTotal(decimal litros)
        {
            var tipoSeleccionado = _view.TipoGasoilSeleccionado;

            var total = litros * tipoSeleccionado.PrecioActual;
            _view.MostrarTotalCalculado(total);
        }

        private async Task ActualizarCreditoAsync(decimal nuevoImporte, decimal importeAnterior)
        {
            decimal diferencia = nuevoImporte - importeAnterior;

            _empresaCredito.CreditoConsumido += diferencia;
            _empresaCredito.CreditoDisponible -= diferencia;

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