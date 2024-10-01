using SacNew.Interfaces;
using SacNew.Models;
using SacNew.Repositories;
using SacNew.Services;
using SacNew.Views.GestionFlota.Postas.ConceptoConsumos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
            var idUsuario = _sesionService.IdUsuario;

            if (_conceptoActual == null)
            {
                // Agregar nuevo concepto
                var nuevoConcepto = new Concepto
                {
                    Codigo = _view.Codigo,
                    Descripcion = _view.Descripcion,
                    IdTipoConsumo = _view.IdTipoConsumo,
                    PrecioActual = _view.PrecioActual,
                    Vigencia = _view.Vigencia,
                    PrecioAnterior = _view.PrecioAnterior,
                    Activo = true,  // Por defecto está activo
                    IdUsuario = idUsuario,  // Usuario desde sesión
                    FechaModificacion = DateTime.Now
                };

                _conceptoRepositorio.AgregarConcepto(nuevoConcepto);

                // Agregar los registros a la tabla de conceptoPostaProveedor
                var idProveedorBahia = _view.IdProveedorBahiaBlanca;
                var idProveedorPlaza = _view.IdProveedorPlazaHuincul;

               // _conceptoPostaProveedorRepositorio.AgregarConceptoPostaProveedor(nuevoConcepto.IdConsumo, 2, idProveedorBahia);  // Posta 2 (Bahía Blanca)
               // _conceptoPostaProveedorRepositorio.AgregarConceptoPostaProveedor(nuevoConcepto.IdConsumo, 3, idProveedorPlaza);  // Posta 3 (Plaza Huincul)

                _view.MostrarMensaje("Concepto agregado exitosamente.");
            }
            else
            {
                // Actualizar concepto existente
                _conceptoActual.IdConsumo = _view.Id;  // Mantener el Id del concepto
                _conceptoActual.Descripcion = _view.Descripcion;
                _conceptoActual.IdTipoConsumo = _view.IdTipoConsumo;
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
    }
}
