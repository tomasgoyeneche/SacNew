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

        private decimal _consumoPrecioAnterior;
        private int? _idConsumo;
        private POC _Poc;
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

        public async Task CargarDatosAsync(POC poc, EmpresaCredito empresaCredito)
        {
            _Poc = poc;
            _empresaCredito = empresaCredito;

            await EjecutarConCargaAsync(async () =>
            {
                await CalcularLitrosAutorizadosAsync();
                await CargarDatosComunesAsync();
            });
        }

        public async Task CargarDatosParaEditarAsync(POC poc, int idConsumo, EmpresaCredito empresaCredito)
        {
            _Poc = poc;
            _idConsumo = idConsumo;
            _empresaCredito = empresaCredito;

            await EjecutarConCargaAsync(async () =>
            {
                var consumo = await _consumoGasoilRepositorio.ObtenerPorIdAsync(idConsumo);
                if (consumo == null)
                {
                    _view.MostrarMensajeGuna("No se encontró el consumo seleccionado.");
                    return;
                }
                _consumoPrecioAnterior = consumo.PrecioTotal;
                _autorizado = consumo.LitrosAutorizados;
                _idPrograma = consumo.IdPrograma;
                (_patente, _capacidadTanque) = await _pocRepositorio.ObtenerUnidadPorPocAsync(_Poc.IdPoc);

                await CargarDatosComunesAsync();
                _view.InicializarParaEdicion(consumo);
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

                if (VerificarCreditoInsuficiente(nuevoPrecioTotal - _consumoPrecioAnterior)) return;
                ConsumoGasoil consumo;

                _Poc.FechaCreacion = _Poc.FechaCreacion.Date;

                if (_idConsumo == null)
                {
                    consumo = CrearNuevoConsumo(nuevoPrecioTotal, tipoSeleccionado.IdConsumo);
                    if (_view.TipoGasoilSeleccionado.IdConsumo == 20 || _view.TipoGasoilSeleccionado.IdConsumo == 21)
                    {
                        consumo.TransitoEspecial = true;
                    }
                    if (!await ValidarAsync(consumo, _capacidadTanque, _autorizado, _Poc.FechaCreacion))
                        return;
                    await _consumoGasoilRepositorio.AgregarConsumoAsync(consumo);
                }
                else
                {
                    consumo = await ActualizarConsumoExistente(nuevoPrecioTotal, tipoSeleccionado.IdConsumo);
                    if (_view.TipoGasoilSeleccionado.IdConsumo == 20 || _view.TipoGasoilSeleccionado.IdConsumo == 21)
                    {
                        consumo.TransitoEspecial = true;
                    }
                    if (!await ValidarAsync(consumo, _capacidadTanque, _autorizado, _Poc.FechaCreacion))
                        return;
                    await _consumoGasoilRepositorio.ActualizarConsumoAsync(consumo);
                }

                if (consumo != null)
                {
                    await ActualizarCreditoAsync(nuevoPrecioTotal, _idConsumo == null ? 0 : _consumoPrecioAnterior);
                    _view.MostrarMensajeGuna("Consumo guardado correctamente.");
                    _view.Cerrar();
                }
            });
        }

        private async Task CargarDatosComunesAsync()
        {
            await CargarTiposGasoilAsync();
            await CargarAutorizacionAnteriorAsync();
            await CargarAutorizacionActualAsync();
        }

        private async Task CalcularLitrosAutorizadosAsync()
        {
            (_patente, _capacidadTanque) = await _pocRepositorio.ObtenerUnidadPorPocAsync(_Poc.IdPoc);
            var programa = await _consumoGasoilRepositorio.ObtenerProgramaPorPatenteAsync(_patente);
            bool validaPorBahiaBlanca = false;
            bool programaValido = programa?.Kilometros > 0;

            if (_sesionService.IdPosta == 2)
            {
                var ultimoConsumo = await _consumoGasoilRepositorio.ObtenerUltimoConsumoPorPatenteAsync(_patente);

                if (ultimoConsumo != null && ultimoConsumo.NumeroPoc.StartsWith("BB"))
                {
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
                _autorizado = _capacidadTanque;
                _idPrograma = 0;
                _view.MostrarMensaje("No se encontró un programa válido. Se usará como máximo la capacidad del tanque.");
            }
        }

        private async Task CargarAutorizacionAnteriorAsync()
        {
            var consumosAnteriores = await _consumoGasoilRepositorio.ObtenerConsumosUltimosDosMesesAsync(_patente, _idPrograma);

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

        private ConsumoGasoil CrearNuevoConsumo(decimal precioTotal, int idConsumo)
        {
            return new ConsumoGasoil
            {
                IdPOC = _Poc.IdPoc,
                IdConsumo = idConsumo,
                IdPrograma = _idPrograma,
                LitrosAutorizados = _autorizado,
                NumeroVale = _view.NumeroVale,
                LitrosCargados = _view.Litros.Value,
                PrecioTotal = precioTotal,
                Observaciones = _view.Observaciones,
                FechaCarga = _view.FechaCarga,
                Dolar = _view.Dolar,
                Activo = true
            };
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
            consumo.Dolar = _view.Dolar;
            consumo.FechaCarga = _view.FechaCarga;

            return consumo;
        }

        private async Task CargarTiposGasoilAsync()
        {
            var tiposGasoil = (await Task.WhenAll(
                _conceptoRepositorio.ObtenerPorTipoAsync(1),
                _conceptoRepositorio.ObtenerPorTipoAsync(2)
            )).SelectMany(x => x).ToList();

            _view.CargarTiposGasoil(FiltrarTiposGasoil(tiposGasoil), _Poc.NumeroPoc);
        }

        private List<Concepto> FiltrarTiposGasoil(IEnumerable<Concepto> tiposGasoil)
        {
            if (_empresaCredito?.IdEmpresa == 1)
            {
                return tiposGasoil
                    .Where(tipo => tipo.Descripcion.Contains("Chenyi", StringComparison.OrdinalIgnoreCase))
                    .ToList();
            }

            // Empresas que NO son Chenyi
            var resultado = tiposGasoil
                .Where(tipo => tipo.Descripcion.Contains("Autorizado", StringComparison.OrdinalIgnoreCase))
                .ToList();

            // Agregar Transito Especial según la posta
            string? transitoEspecial = _sesionService.IdPosta switch
            {
                2 => "Comb. Transito Especial BB",
                3 => "Comb. Transito Especial PH",
                _ => null
            };

            if (transitoEspecial != null)
            {
                var especial = tiposGasoil
                    .FirstOrDefault(tipo => tipo.Descripcion.Equals(transitoEspecial, StringComparison.OrdinalIgnoreCase));

                if (especial != null)
                {
                    resultado.Add(especial);
                }
            }

            return resultado;
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
                _view.MostrarMensajeGuna($"El crédito disponible ({_empresaCredito.CreditoDisponible:C}) no es suficiente.");
                return true;
            }
            return false;
        }
    }
}