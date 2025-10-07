using Core.Base;
using Core.Repositories;
using Core.Services;
using Servicios.Views.Mantenimiento;
using Shared.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Servicios.Presenters.Mantenimiento
{
    public class AgregarEditarArticuloProveedorPresenter : BasePresenter<IAgregarEditarArticuloProveedorView>
    {
        private readonly IArticuloProveedorRepositorio _articuloRepositorio;
        private readonly IMedidaRepositorio _medidaRepositorio;

        public ArticuloProveedor? ProveedorActual { get; private set; }

        public AgregarEditarArticuloProveedorPresenter(
            IArticuloProveedorRepositorio articuloRepositorio,
            IMedidaRepositorio medidaRepositorio,
            ISesionService sesionService,
            INavigationService navigationService
        ) : base(sesionService, navigationService)
        {
            _articuloRepositorio = articuloRepositorio;
            _medidaRepositorio = medidaRepositorio;
        }

        public async Task InicializarAsync(int? idArticuloProveedor = null)
        {
            await EjecutarConCargaAsync(async () =>
            {
                if (idArticuloProveedor.HasValue)
                {
                    ProveedorActual = await _articuloRepositorio.ObtenerPorIdAsync(idArticuloProveedor.Value);
                    if (ProveedorActual != null)
                    {
                        _view.MostrarDatosProveedor(ProveedorActual);
                    }
                }
            });
        }

        public async Task GuardarAsync()
        {
            var proveedor = new ArticuloProveedor
            {
                IdProveedor = ProveedorActual?.IdProveedor ?? 0,
                RazonSocial = _view.RazonSocial,
                CUIT = _view.CUIT,
                Direccion = _view.Direccion ?? "Indefinido",
                Telefono = _view.Telefono ?? "Indefinido",
                Email = _view.Email ?? "Indefinido",
                Activo = true
            };

            await EjecutarConCargaAsync(async () =>
            {
                if (ProveedorActual == null)
                {
                    var id = await _articuloRepositorio.AgregarAsync(proveedor);
                    _view.MostrarMensaje($"Proveedor agregado correctamente. ID: {id}");
                }
                else
                {
                    await _articuloRepositorio.ActualizarAsync(proveedor);
                    _view.MostrarMensaje("Proveedor actualizado correctamente.");
                }

                _view.Cerrar();
            });
        }
    }
}
