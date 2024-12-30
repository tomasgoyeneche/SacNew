using Core.Base;
using Core.Repositories;
using Core.Services;
using SacNew.Views.GestionFlota.Postas.ConceptoConsumos;
using Shared.Models;

namespace GestionFlota.Presenters
{
    public class AgregarEditarConceptoPresenter : BasePresenter<IAgregarEditarConceptoView>
    {
        private readonly IConceptoRepositorio _conceptoRepositorio;
        private readonly IConceptoTipoRepositorio _conceptoTipoRepositorio;
        private readonly IConceptoProveedorRepositorio _conceptoProveedorRepositorio;
        private readonly IConceptoPostaProveedorRepositorio _conceptoPostaProveedorRepositorio;
        private Concepto? _conceptoActual;

        public AgregarEditarConceptoPresenter(
            IConceptoRepositorio conceptoRepositorio,
            IConceptoTipoRepositorio conceptoTipoRepositorio,
            IConceptoProveedorRepositorio conceptoProveedorRepositorio,
            IConceptoPostaProveedorRepositorio conceptoPostaProveedorRepositorio,
            ISesionService sesionService,
            INavigationService navigationService)
            : base(sesionService, navigationService)
        {
            _conceptoRepositorio = conceptoRepositorio;
            _conceptoTipoRepositorio = conceptoTipoRepositorio;
            _conceptoProveedorRepositorio = conceptoProveedorRepositorio;
            _conceptoPostaProveedorRepositorio = conceptoPostaProveedorRepositorio;
        }

        public async Task InicializarAsync(Concepto conceptoSeleccionado)
        {
            await CargarTiposDeConsumoAsync();
            await CargarProveedoresAsync();
            if (conceptoSeleccionado != null)
            {
                CargarDatosParaEditar(conceptoSeleccionado);
            }
        }

        private async Task CargarTiposDeConsumoAsync()
        {
            var tiposDeConsumo = await _conceptoTipoRepositorio.ObtenerTodosLosTiposAsync();
            await _view.CargarTiposDeConsumoAsync(tiposDeConsumo);
        }

        private async Task CargarProveedoresAsync()
        {
            var proveedores = await _conceptoProveedorRepositorio.ObtenerTodosLosProveedoresAsync();
            await _view.CargarProveedoresBahiaBlancaAsync(proveedores);
            await _view.CargarProveedoresPlazaHuinculAsync(proveedores);
        }

        public void CargarDatosParaEditar(Concepto concepto)
        {
            _conceptoActual = concepto;
            _view.MostrarDatosConcepto(_conceptoActual);
        }

        public async Task GuardarConceptoAsync()
        {
            if (!ValidarDatos())
            {
                return;
            }

            var idUsuario = _sesionService.IdUsuario;

            if (_conceptoActual == null)
            {
                // Agregar nuevo concepto
                var nuevoConcepto = new Concepto
                {
                    Codigo = _view.Codigo,
                    Descripcion = _view.Descripcion,
                    IdConsumoTipo = _view.IdTipoConsumo,
                    PrecioActual = _view.PrecioActual,
                    Vigencia = _view.Vigencia,
                    PrecioAnterior = _view.PrecioAnterior,
                    Activo = true,  // Por defecto está activo
                    IdUsuario = idUsuario,  // Usuario desde sesión
                    FechaModificacion = DateTime.Now
                };

                await _conceptoRepositorio.AgregarConceptoAsync(nuevoConcepto);
                _view.MostrarMensaje("Concepto agregado exitosamente.");
            }
            else
            {
                // Actualizar concepto existente
                _conceptoActual.IdConsumo = _view.Id;  // Mantener el Id del concepto
                _conceptoActual.Descripcion = _view.Descripcion;
                _conceptoActual.IdConsumoTipo = _view.IdTipoConsumo;
                _conceptoActual.PrecioActual = _view.PrecioActual;
                _conceptoActual.Vigencia = _view.Vigencia;
                _conceptoActual.PrecioAnterior = _view.PrecioAnterior;
                _conceptoActual.IdUsuario = idUsuario;
                _conceptoActual.FechaModificacion = DateTime.Now;

                await _conceptoRepositorio.ActualizarConceptoAsync(_conceptoActual);

                // Actualizar los registros de conceptoPostaProveedor
                var idProveedorBahia = _view.IdProveedorBahiaBlanca;
                var idProveedorPlaza = _view.IdProveedorPlazaHuincul;

                await _conceptoPostaProveedorRepositorio.ActualizarConceptoPostaProveedorAsync(_conceptoActual.IdConsumo, 2, idProveedorBahia);
                await _conceptoPostaProveedorRepositorio.ActualizarConceptoPostaProveedorAsync(_conceptoActual.IdConsumo, 3, idProveedorPlaza);

                _view.MostrarMensaje("Concepto actualizado exitosamente.");
            }
        }

        private bool ValidarDatos()
        {
            if (string.IsNullOrWhiteSpace(_view.Codigo))
            {
                _view.MostrarMensaje("El código no puede estar vacío.");
                return false;
            }

            if (string.IsNullOrWhiteSpace(_view.Descripcion))
            {
                _view.MostrarMensaje("La descripción no puede estar vacía.");
                return false;
            }

            if (_view.IdTipoConsumo <= 0)
            {
                _view.MostrarMensaje("Debe seleccionar un tipo de consumo válido.");
                return false;
            }

            if (_view.PrecioActual < 0)
            {
                _view.MostrarMensaje("El precio actual debe ser mayor o igual a 0.");
                return false;
            }

            if (_view.PrecioAnterior < 0)
            {
                _view.MostrarMensaje("El precio anterior no puede ser negativo.");
                return false;
            }

            if (_view.Vigencia == DateTime.MinValue)
            {
                _view.MostrarMensaje("Debe seleccionar una fecha de vigencia válida.");
                return false;
            }

            if (_view.IdProveedorBahiaBlanca <= 0)
            {
                _view.MostrarMensaje("Debe seleccionar un proveedor válido para Bahía Blanca.");
                return false;
            }

            if (_view.IdProveedorPlazaHuincul <= 0)
            {
                _view.MostrarMensaje("Debe seleccionar un proveedor válido para Plaza Huincul.");
                return false;
            }

            return true;
        }
    }
}