using Core.Base;
using Core.Repositories;
using Core.Services;
using Servicios.Views.Mantenimiento;
using Shared.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Servicios.Presenters
{
    public class AgregarEditarComprobantePresenter
     : BasePresenter<IAgregarEditarComprobanteView>
    {
        private readonly IMovimientoComprobanteRepositorio _comprobanteRepositorio;
        private readonly IArticuloProveedorRepositorio _proveedorRepositorio;


        public string _Tipo;
        public MovimientoComprobante? ComprobanteActual { get; private set; }
        public OrdenTrabajoComprobante? OrdenTrabajoComprobanteActual { get; private set; }



        public AgregarEditarComprobantePresenter(
            IMovimientoComprobanteRepositorio comprobanteRepositorio,
            IArticuloProveedorRepositorio proveedorRepositorio,
            ISesionService sesionService,
            INavigationService navigationService
        ) : base(sesionService, navigationService)
        {
            _comprobanteRepositorio = comprobanteRepositorio;
            _proveedorRepositorio = proveedorRepositorio;
        }

        public async Task InicializarAsync(int idMovimientoStock, string Tipo, MovimientoComprobante? comprobante = null, OrdenTrabajoComprobante? ordenTrabajoComprobante = null)
        {
            await EjecutarConCargaAsync(async () =>
            {
                _Tipo = Tipo;
                var tipos = await _comprobanteRepositorio.ObtenerTiposComprobantes();
                _view.CargarTiposComprobante(tipos);


                if (_Tipo == "MovimientoStock")
                {
                    _view.MostrarProveedores(true);
                    List<ArticuloProveedor> proveedores = await _proveedorRepositorio.ObtenerTodosAsync();
                    _view.CargarProveedores(proveedores);


                    _view.Id = idMovimientoStock;

                    if (comprobante != null)
                    {
                        ComprobanteActual = comprobante;
                        _view.Nombre = comprobante.Nombre;
                        _view.IdTipoComprobante = comprobante.IdTipoComprobante;
                        _view.NroComprobante = comprobante.NroComprobante;
                        _view.IdProveedor = comprobante.IdProveedor;
                        _view.RutaComprobante = comprobante.RutaComprobante;
                    }
                }
                else{
                    _view.MostrarProveedores(false);

                    _view.Id = idMovimientoStock;

                    if (ordenTrabajoComprobante != null)
                    {
                        OrdenTrabajoComprobanteActual = ordenTrabajoComprobante;
                        _view.Nombre = ordenTrabajoComprobante.Nombre;
                        _view.IdTipoComprobante = ordenTrabajoComprobante.IdTipoComprobante ?? 0;
                        _view.NroComprobante = ordenTrabajoComprobante.NroComprobante ?? string.Empty;
                        _view.RutaComprobante = ordenTrabajoComprobante.RutaComprobante;
                    }
                }
            });
        }

        public void CargarPdf(string origen)
        {
            var idMovimiento = _view.Id;
            string carpeta;
            if(_Tipo == "MovimientoStock")
            {
                carpeta = Path.Combine(@"S:\StockComprobantes", idMovimiento.ToString());
            }
            else {                 
                carpeta = Path.Combine(@"S:\OrdenesTrabajoComprobantes", idMovimiento.ToString());
            }

            if (!Directory.Exists(carpeta))
                Directory.CreateDirectory(carpeta);

            // Generar nombre dinámico ComprobanteX.pdf
            string destino;
            do
            {
                destino = Path.Combine(carpeta, $"{_view.Nombre}.pdf");
            }
            while (File.Exists(destino));

            File.Copy(origen, destino);

            _view.RutaComprobante = destino;
            _view.MostrarMensaje($"Archivo cargado: {destino}");
        }

        public async Task GuardarAsync()
        {
            if (_Tipo == "MovimientoStock")
            {
                var comprobante = new MovimientoComprobante
                {
                    IdMovimientoComprobante = ComprobanteActual?.IdMovimientoComprobante ?? 0,
                    IdMovimientoStock = _view.Id,
                    IdTipoComprobante = _view.IdTipoComprobante.Value,
                    NroComprobante = _view.NroComprobante,
                    IdProveedor = _view.IdProveedor,
                    RutaComprobante = _view.RutaComprobante,
                    Activo = true
                };

                await EjecutarConCargaAsync(async () =>
                {
                    if (ComprobanteActual == null)
                    {
                        var id = await _comprobanteRepositorio.AgregarAsync(comprobante);
                        comprobante.IdMovimientoComprobante = id;
                        _view.MostrarMensaje("Comprobante agregado correctamente.");
                    }
                    else
                    {
                        await _comprobanteRepositorio.ActualizarAsync(comprobante);
                        _view.MostrarMensaje("Comprobante actualizado correctamente.");
                    }
                    _view.Cerrar();
                });
            }
            else
            {
                var ordenTrabajocomprobante = new OrdenTrabajoComprobante
                {
                    Nombre = _view.Nombre,  
                    IdOrdenTrabajoComprobante = ComprobanteActual?.IdMovimientoComprobante ?? 0,
                    IdOrdenTrabajo = _view.Id,
                    IdTipoComprobante = _view.IdTipoComprobante ?? null,
                    NroComprobante = _view.NroComprobante ?? null,
                    RutaComprobante = _view.RutaComprobante,
                    Activo = true
                };

                await EjecutarConCargaAsync(async () =>
                {
                    if (ComprobanteActual == null)
                    {
                        var id = await _ordenTrabajoComprobanteRepositorio.AgregarAsync(ordenTrabajocomprobante);
                        ordenTrabajocomprobante.IdOrdenTrabajoComprobante = id;
                        _view.MostrarMensaje("Comprobante agregado correctamente.");
                    }
                    else
                    {
                        await _ordenTrabajoComprobanteRepositorio.ActualizarAsync(ordenTrabajocomprobante);
                        _view.MostrarMensaje("Comprobante actualizado correctamente.");
                    }
                    _view.Cerrar();
                });
            }
          
        }

        public async Task AgregarProveedorAsync()
        {
            await EjecutarConCargaAsync(async () =>
            {
                await AbrirFormularioAsync<AgregarEditarArticuloProveedorForm>(async form =>
                {
                    await form._presenter.InicializarAsync(0);
                });
            }, async () => await InicializarAsync(_view.Id, _Tipo, ComprobanteActual, null));
        }
    }
}
