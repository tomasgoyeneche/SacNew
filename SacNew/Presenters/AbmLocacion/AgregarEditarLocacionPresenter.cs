using SacNew.Models;
using SacNew.Repositories;
using SacNew.Services;
using SacNew.Views.Configuraciones.AbmLocaciones;

namespace SacNew.Presenters
{
    public class AgregarEditarLocacionPresenter : BasePresenter<IAgregarEditarLocacionView>
    {
        private readonly ILocacionRepositorio _locacionRepositorio;
        private readonly ILocacionProductoRepositorio _locacionProductoRepositorio;
        private readonly ILocacionKilometrosEntreRepositorio _locacionKilometrosEntreRepositorio;

        private Locacion? _locacionActual;

        public AgregarEditarLocacionPresenter(
            ILocacionRepositorio locacionRepositorio,
            ILocacionProductoRepositorio locacionProductoRepositorio,
            ILocacionKilometrosEntreRepositorio locacionKilometrosEntreRepositorio,
            ISesionService sesionService,
            INavigationService navigationService
        ) : base(sesionService, navigationService)
        {
            _locacionRepositorio = locacionRepositorio;
            _locacionProductoRepositorio = locacionProductoRepositorio;
            _locacionKilometrosEntreRepositorio = locacionKilometrosEntreRepositorio;
        }

        public async Task InicializarAsync(int? idLocacion)
        {
            await EjecutarConCargaAsync(async () =>
            {
                if (idLocacion.HasValue)
                {
                    _locacionActual = await _locacionRepositorio.ObtenerPorIdAsync(idLocacion.Value);
                    _view.MostrarDatosLocacion(_locacionActual);
                    await CargarProductosAsync(idLocacion.Value);
                    await CargarKilometrosAsync(idLocacion.Value);
                    _view.EstablecerModoEdicion(true);
                }
                else
                {
                    _locacionActual = new Locacion();
                    _view.EstablecerModoEdicion(false);
                }
            });
        }

        public async Task GuardarLocacionAsync()
        {
            await EjecutarConCargaAsync(async () =>
            {
                _locacionActual.Nombre = _view.Nombre;
                _locacionActual.Direccion = _view.Direccion;
                _locacionActual.Carga = _view.Carga;
                _locacionActual.Descarga = _view.Descarga;
                _locacionActual.Activo = true;

                if (!await ValidarAsync(_locacionActual))
                    return;

                if (_locacionActual.IdLocacion == 0)
                {
                    await _locacionRepositorio.AgregarAsync(_locacionActual);
                    _view.MostrarMensaje("Locación agregada correctamente.");
                }
                else
                {
                    await _locacionRepositorio.ActualizarAsync(_locacionActual);
                    _view.MostrarMensaje("Locación actualizada correctamente.");
                }
            });
        }

        public async Task EliminarProductoAsync(int idLocacionProducto)
        {
            await EjecutarConCargaAsync(async () =>
            {
                var confirmacion = _view.ConfirmarEliminacion("¿Está seguro que desea eliminar este producto asociado?");
                if (confirmacion == DialogResult.Yes)
                {
                    await _locacionProductoRepositorio.EliminarAsync(idLocacionProducto);
                    await CargarProductosAsync(_locacionActual.IdLocacion);
                }
            });
        }

        public async Task EliminarKilometrosAsync(int idKilometros)
        {
            await EjecutarConCargaAsync(async () =>
            {
                var confirmacion = _view.ConfirmarEliminacion("¿Está seguro que desea eliminar esta locación-destino?");
                if (confirmacion == DialogResult.Yes)
                {
                    await _locacionKilometrosEntreRepositorio.EliminarAsync(idKilometros);
                    await CargarKilometrosAsync(_locacionActual.IdLocacion);
                }
            });
        }

        private async Task CargarProductosAsync(int idLocacion)
        {
            var productos = await _locacionProductoRepositorio.ObtenerPorLocacionIdAsync(idLocacion);
            _view.CargarProductos(productos);
        }

        private async Task CargarKilometrosAsync(int idLocacion)
        {
            var kilometros = await _locacionKilometrosEntreRepositorio.ObtenerPorLocacionIdAsync(idLocacion);
            _view.CargarKilometros(kilometros);
        }

        public async Task AgregarProducto()
        {
            await AbrirFormularioAsync<AgregarProductoForm>(async form =>
            {
                await form._presenter.InicializarAsync(_locacionActual.IdLocacion);
            });

            await CargarProductosAsync(_locacionActual.IdLocacion);
        }

        public async Task AgregarKilometro()
        {
            await AbrirFormularioAsync<AgregarKilometrosLocaciones>(async form =>
            {
                await form._presenter.InicializarAsync(_locacionActual.IdLocacion);
            });

            await CargarKilometrosAsync(_locacionActual.IdLocacion);
        }
    }
}