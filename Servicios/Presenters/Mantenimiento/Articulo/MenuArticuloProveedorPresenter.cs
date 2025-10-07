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
    public class MenuArticuloProveedorPresenter : BasePresenter<IMenuArticuloProveedorView>
    {
        private readonly IUnidadRepositorio _unidadRepositorio;
        private readonly IChoferRepositorio _choferRepositorio;
        private readonly IArticuloProveedorRepositorio _articuloRepositorio;

        public MenuArticuloProveedorPresenter(
            IChoferRepositorio choferRepositorio,
            IUnidadRepositorio unidadRepositorio,
            IArticuloProveedorRepositorio articuloRepositorio,
            ISesionService sesionService,
            INavigationService navigationService)
        : base(sesionService, navigationService)  // Aquí pasamos las dependencias a la clase base
        {
            _unidadRepositorio = unidadRepositorio;
            _choferRepositorio = choferRepositorio;
            _articuloRepositorio = articuloRepositorio;
        }

        public async Task CargarProveedoresAsync()
        {
            List<ArticuloProveedor> novedades = await _articuloRepositorio.ObtenerTodosAsync();
            _view.MostrarProveedores(novedades);
        }

        public async Task EliminarArticulosAsync(int idProveedor)
        {
            await EjecutarConCargaAsync(async () =>
            {
                var confirmacion = _view.ConfirmarEliminacion("¿Está seguro que desea eliminar este Proveedor?");
                if (confirmacion != DialogResult.Yes) return;

                await _articuloRepositorio.EliminarAsync(idProveedor);

                _view.MostrarMensaje("Alerta eliminada correctamente.");
            }, async () => await CargarProveedoresAsync());
        }

        public async Task EditarArticulosAsync(int idProveedor)
        {
            await EjecutarConCargaAsync(async () =>
            {
                await AbrirFormularioAsync<AgregarEditarArticuloProveedorForm>(async form =>
                {
                    await form._presenter.InicializarAsync(idProveedor);
                });
            }, async () => await CargarProveedoresAsync());
        }

        public async Task AgregarArticulosAsync()
        {
            await EjecutarConCargaAsync(async () =>
            {
                await AbrirFormularioAsync<AgregarEditarArticuloProveedorForm>(async form =>
                {
                    await form._presenter.InicializarAsync(0);
                });
            }, async () => await CargarProveedoresAsync());
        }
    }
}
