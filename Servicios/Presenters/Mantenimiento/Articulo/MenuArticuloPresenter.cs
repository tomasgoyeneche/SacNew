using Core.Base;
using Core.Repositories;
using Core.Services;
using GestionFlota.Views.Alertas;
using Servicios.Views.Mantenimiento;
using Shared.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Servicios.Presenters
{
    public class MenuArticuloPresenter : BasePresenter<IMenuArticuloView>
    {
        private readonly IUnidadRepositorio _unidadRepositorio;
        private readonly IChoferRepositorio _choferRepositorio;
        private readonly IArticuloRepositorio _articuloRepositorio;

        public MenuArticuloPresenter(
            IChoferRepositorio choferRepositorio,
            IUnidadRepositorio unidadRepositorio,
            IArticuloRepositorio articuloRepositorio,
            ISesionService sesionService,
            INavigationService navigationService)
        : base(sesionService, navigationService)  // Aquí pasamos las dependencias a la clase base
        {
            _unidadRepositorio = unidadRepositorio;
            _choferRepositorio = choferRepositorio;
            _articuloRepositorio = articuloRepositorio;
        }

        public async Task CargarArticulosAsync()
        {
            List<Articulo> novedades = await _articuloRepositorio.ObtenerArticulosActivosAsync();
            _view.MostrarArticulos(novedades);
        }

        public async Task EliminarArticulosAsync(int idArticulo)
        {
            await EjecutarConCargaAsync(async () =>
            {
                var confirmacion = _view.ConfirmarEliminacion("¿Está seguro que desea eliminar este Articulo?");
                if (confirmacion != DialogResult.Yes) return;

                await _articuloRepositorio.EliminarArticuloAsync(idArticulo);

                _view.MostrarMensaje("Alerta eliminada correctamente.");
            }, async () => await CargarArticulosAsync());
        }

        public async Task EditarArticulosAsync(int idArticulo)
        {
            await EjecutarConCargaAsync(async () =>
            {
                await AbrirFormularioAsync<AgregarEditarArticuloForm>(async form =>
                {
                    await form._presenter.InicializarAsync(idArticulo);
                });
            }, async () => await CargarArticulosAsync());
        }

        public async Task AgregarArticulosAsync()
        {
            await EjecutarConCargaAsync(async () =>
            {
                await AbrirFormularioAsync<AgregarEditarArticuloForm>(async form =>
                {
                    await form._presenter.InicializarAsync(0);
                });
            }, async () => await CargarArticulosAsync());
        }
    }
}
