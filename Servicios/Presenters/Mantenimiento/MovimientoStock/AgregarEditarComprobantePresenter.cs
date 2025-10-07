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

        public MovimientoComprobante? ComprobanteActual { get; private set; }

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

        public async Task InicializarAsync(int idMovimientoStock, MovimientoComprobante? comprobante = null)
        {
            await EjecutarConCargaAsync(async () =>
            {
                var tipos = await _comprobanteRepositorio.ObtenerTiposComprobantes();
                var proveedores = await _proveedorRepositorio.ObtenerTodosAsync();

                _view.CargarTiposComprobante(tipos);
                _view.CargarProveedores(proveedores);

                _view.IdMovimientoStock = idMovimientoStock;

                if (comprobante != null)
                {
                    ComprobanteActual = comprobante;
                    _view.IdTipoComprobante = comprobante.IdTipoComprobante;
                    _view.NroComprobante = comprobante.NroComprobante;
                    _view.IdProveedor = comprobante.IdProveedor;
                    _view.RutaComprobante = comprobante.RutaComprobante;
                }
            });
        }

        public void CargarPdf(string origen)
        {
            var idMovimiento = _view.IdMovimientoStock;
            var carpeta = Path.Combine(@"S:\StockComprobantes", idMovimiento.ToString());

            if (!Directory.Exists(carpeta))
                Directory.CreateDirectory(carpeta);

            // Generar nombre dinámico ComprobanteX.pdf
            int contador = 1;
            string destino;
            do
            {
                destino = Path.Combine(carpeta, $"Comprobante{contador}.pdf");
                contador++;
            }
            while (File.Exists(destino));

            File.Copy(origen, destino);

            _view.RutaComprobante = destino;
            _view.MostrarMensaje($"Archivo cargado: {destino}");
        }

        public async Task GuardarAsync()
        {
            var comprobante = new MovimientoComprobante
            {
                IdMovimientoComprobante = ComprobanteActual?.IdMovimientoComprobante ?? 0,
                IdMovimientoStock = _view.IdMovimientoStock,
                IdTipoComprobante = _view.IdTipoComprobante,
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

        public async Task AgregarProveedorAsync()
        {
            await EjecutarConCargaAsync(async () =>
            {
                await AbrirFormularioAsync<AgregarEditarArticuloProveedorForm>(async form =>
                {
                    await form._presenter.InicializarAsync(0);
                });
            }, async () => await InicializarAsync(_view.IdMovimientoStock, ComprobanteActual));
        }
    }
}
