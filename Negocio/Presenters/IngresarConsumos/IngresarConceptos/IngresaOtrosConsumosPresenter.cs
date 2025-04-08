using Core.Base;
using Core.Repositories;
using Core.Services;
using GestionFlota.Views.Postas.IngresaConsumos.ManualConsumos.IngresarConsumo;
using Shared.Models;
using System.Threading.Tasks;

namespace GestionFlota.Presenters.IngresarConsumos
{
    public class IngresaOtrosConsumosPresenter : BasePresenter<IOtrosConsumosView>
    {
        private readonly IConceptoRepositorio _conceptoRepositorio;
        private readonly IEmpresaCreditoRepositorio _empresaCreditoRepositorio;
        private readonly IConsumoOtrosRepositorio _consumoOtrosRepositorio;
        public int? _idConsumo; // Null si es un nuevo consumo, valor si es edición
        private POC _Poc;
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

        public async Task CargarDatosAsync(POC poc, EmpresaCredito empresaCredito)
        {
            _Poc = poc;
            _empresaCredito = empresaCredito;

            await EjecutarConCargaAsync(async () =>
            {
                var tiposConsumo = await Task.WhenAll(
                    _conceptoRepositorio.ObtenerPorTipoAsync(3),
                    _conceptoRepositorio.ObtenerPorTipoAsync(5)
                );

                var todosLosTiposConsumo = tiposConsumo.SelectMany(x => x).ToList();
                _view.CargarTiposConsumo(todosLosTiposConsumo , poc.NumeroPoc);
            });
        }

        public async Task CalcularTotal(decimal litros)
        {
            Concepto concepto = await _conceptoRepositorio.ObtenerPorIdAsync(_view.TipoConsumoSeleccionado);
            if (concepto == null)
            {
                _view.MostrarMensaje("Debe seleccionar un tipo de consumo.");
                return;
            }

            if (concepto.IdConsumoTipo == 3)
            {
                _view.MostrarTotalCalculado(litros * concepto.PrecioActual);
            }
        }

        public async Task GuardarConsumoAsync()
        {
            await EjecutarConCargaAsync(async () =>
            {
                if (!ValidarDatos()) return;

                Concepto concepto = await _conceptoRepositorio.ObtenerPorIdAsync(_view.TipoConsumoSeleccionado);

                decimal nuevoPrecioTotal = 0;

               

                if (concepto.IdConsumoTipo == 3)
                {
                    nuevoPrecioTotal = _view.Cantidad.Value * concepto.PrecioActual;
                }
                else
                {
                    if (!_view.PrecioManual.HasValue || _view.PrecioManual < 0)
                    {
                        _view.MostrarMensaje("Debe ingresar un valor para el precio.");
                        return;
                    }

                    nuevoPrecioTotal = _view.PrecioManual.Value * _view.Cantidad.Value;
                }

               
                if (_idConsumo == null) // Nuevo consumo
                {
                    if (VerificarCreditoInsuficiente(nuevoPrecioTotal)) return;

                    var nuevoConsumo = new ConsumoOtros
                    {
                        IdPOC = _Poc.IdPoc,
                        IdConsumo = concepto.IdConsumo,
                        NumeroVale = _view.RemitoExterno,
                        Cantidad = _view.Cantidad.Value,
                        ImporteTotal = nuevoPrecioTotal,
                        Aclaracion = _view.Aclaraciones,
                        FechaRemito = _view.FechaRemito,
                        Dolar = _view.Dolar,
                        Activo = true
                    };

                    await _consumoOtrosRepositorio.AgregarConsumoAsync(nuevoConsumo);
                    ActualizarCredito(nuevoPrecioTotal, 0); // Agregar el nuevo consumo
                }
                else // Edición de consumo existente
                {
                    var consumoAnterior = await _consumoOtrosRepositorio.ObtenerPorIdAsync(_idConsumo.Value);
                    if (consumoAnterior == null)
                    {
                        _view.MostrarMensaje("No se pudo cargar el consumo anterior.");
                        return;
                    }

                    if (VerificarCreditoInsuficiente(nuevoPrecioTotal - consumoAnterior.ImporteTotal)) return;

                    ActualizarCredito(nuevoPrecioTotal, consumoAnterior.ImporteTotal); // Ajustar crédito

                    consumoAnterior.IdConsumo = concepto.IdConsumo;
                    consumoAnterior.NumeroVale = _view.RemitoExterno;
                    consumoAnterior.Cantidad = _view.Cantidad.Value;
                    consumoAnterior.ImporteTotal = nuevoPrecioTotal;
                    consumoAnterior.Aclaracion = _view.Aclaraciones;
                    consumoAnterior.FechaRemito = _view.FechaRemito;
                    consumoAnterior.Dolar = _view.Dolar;

                    await _consumoOtrosRepositorio.ActualizarConsumoAsync(consumoAnterior);
                }

                _view.MostrarMensaje("Consumo guardado correctamente.");
                _view.Cerrar();
            });
        }

        private bool ValidarDatos()
        {
            _Poc.FechaCreacion = _Poc.FechaCreacion.Date;

            if (_view.FechaRemito < _Poc.FechaCreacion)
            {
                _view.MostrarMensaje("La fecha de remito no puede ser menor a la de la POC");
                return false;
            }

            if (_view.TipoConsumoSeleccionado < 0)
            {
                _view.MostrarMensaje("Debe seleccionar un tipo de consumo.");
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
            if (precioTotal > _empresaCredito.CreditoDisponible)
            {
                _view.MostrarMensaje("El crédito disponible no es suficiente para este consumo.");
                return true;
            }
            return false;
        }

        private async void ActualizarCredito(decimal nuevoImporte, decimal importeAnterior)
        {
            decimal diferencia = nuevoImporte - importeAnterior;

            _empresaCredito.CreditoConsumido += diferencia;
            _empresaCredito.CreditoDisponible -= diferencia;

            await _empresaCreditoRepositorio.ActualizarCreditoAsync(_empresaCredito);
        }

        public async Task CargarDatosParaEditarAsync(POC poc, int idConsumo, EmpresaCredito empresaCredito)
        {
            _Poc = poc;
            _empresaCredito = empresaCredito;

            await EjecutarConCargaAsync(async () =>
            {
                var consumo = await _consumoOtrosRepositorio.ObtenerPorIdAsync(idConsumo);
                if (consumo == null)
                {
                    _view.MostrarMensaje("No se encontró el consumo seleccionado.");
                    return;
                }

                var tiposConsumo = await Task.WhenAll(
                    _conceptoRepositorio.ObtenerPorTipoAsync(3),
                    _conceptoRepositorio.ObtenerPorTipoAsync(5)
                );

              
                var todosLosTiposConsumo = tiposConsumo.SelectMany(x => x).ToList();
                _idConsumo = idConsumo;
                _view.CargarTiposConsumo(todosLosTiposConsumo, poc.NumeroPoc);
                
                _view.InicializarParaEdicion(consumo);
            });
        }
    }
}