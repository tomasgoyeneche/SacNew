using SacNew.Models;
using SacNew.Repositories;
using SacNew.Services;
using SacNew.Views.GestionFlota.Postas.ConceptoConsumos;

namespace SacNew.Presenters
{
    public class AgregarEditarConceptoPresenter
    {
        private IAgregarEditarConceptoView _view;

        private readonly IConceptoRepositorio _conceptoRepositorio;
        private readonly IConceptoTipoRepositorio _conceptoTipoRepositorio;
        private readonly IConceptoProveedorRepositorio _conceptoProveedorRepositorio;
        private readonly IConceptoPostaProveedorRepositorio _conceptoPostaProveedorRepositorio;
        private readonly ISesionService _sesionService;
        private Concepto _conceptoActual;

        public AgregarEditarConceptoPresenter(
                                              IConceptoRepositorio conceptoRepositorio,
                                              IConceptoTipoRepositorio conceptoTipoRepositorio,
                                              IConceptoProveedorRepositorio conceptoProveedorRepositorio,
                                              IConceptoPostaProveedorRepositorio conceptoPostaProveedorRepositorio,
                                              ISesionService sesionService)
        {
            _conceptoRepositorio = conceptoRepositorio;
            _conceptoTipoRepositorio = conceptoTipoRepositorio;
            _conceptoProveedorRepositorio = conceptoProveedorRepositorio;
            _conceptoPostaProveedorRepositorio = conceptoPostaProveedorRepositorio;
            _sesionService = sesionService;
        }

        public void SetView(IAgregarEditarConceptoView view)
        {
            _view = view;
        }

        public void Inicializar()
        {
            //CargarTiposDeConsumo();
            CargarProveedores();
        }

        private void CargarTiposDeConsumo()
        {
            var tiposDeConsumo = _conceptoTipoRepositorio.ObtenerTodosLosTipos();
            _view.CargarTiposDeConsumo(tiposDeConsumo);
        }

        private void CargarProveedores()
        {
            var proveedores = _conceptoProveedorRepositorio.ObtenerTodosLosProveedores();
            _view.CargarProveedoresBahiaBlanca(proveedores);
            _view.CargarProveedoresPlazaHuincul(proveedores);
        }

        public void CargarDatosParaEditar(Concepto concepto)
        {
            _conceptoActual = concepto;
            _view.MostrarDatosConcepto(_conceptoActual);
        }

        public void GuardarConcepto()
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

                _conceptoRepositorio.AgregarConcepto(nuevoConcepto);

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
                _conceptoRepositorio.ActualizarConcepto(_conceptoActual);

                // Actualizar los registros de conceptoPostaProveedor
                var idProveedorBahia = _view.IdProveedorBahiaBlanca;
                var idProveedorPlaza = _view.IdProveedorPlazaHuincul;

                _conceptoPostaProveedorRepositorio.ActualizarConceptoPostaProveedor(_conceptoActual.IdConsumo, 2, idProveedorBahia);
                _conceptoPostaProveedorRepositorio.ActualizarConceptoPostaProveedor(_conceptoActual.IdConsumo, 3, idProveedorPlaza);

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