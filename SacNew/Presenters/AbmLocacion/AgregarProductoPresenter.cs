using SacNew.Interfaces;
using SacNew.Models;
using SacNew.Repositories;
using SacNew.Services;

namespace SacNew.Presenters
{
    public class AgregarProductoPresenter : BasePresenter<IAgregarProductoView>
    {
        private readonly IProductoRepositorio _productoRepositorio;
        private readonly ILocacionProductoRepositorio _locacionProductoRepositorio;
        private int _idLocacion;

        public AgregarProductoPresenter(
            IProductoRepositorio productoRepositorio,
            ILocacionProductoRepositorio locacionProductoRepositorio,
            ISesionService sesionService,
            IServiceProvider serviceProvider
        ) : base(sesionService, serviceProvider)
        {
            _productoRepositorio = productoRepositorio;
            _locacionProductoRepositorio = locacionProductoRepositorio;
        }

        public async Task InicializarAsync(int idLocacion)
        {
            _idLocacion = idLocacion;
            await EjecutarConCargaAsync(async () =>
            {
                var productos = await _productoRepositorio.ObtenerTodosAsync();
                _view.CargarProductos(productos);
            });
        }

        public async Task GuardarProductoAsync()
        {
            await EjecutarConCargaAsync(async () =>
            {
                var locacionProducto = new LocacionProducto
                {
                    IdLocacion = _idLocacion,
                    IdProducto = _view.ProductoSeleccionado
                };

                await _locacionProductoRepositorio.AgregarAsync(locacionProducto);
                _view.MostrarMensaje("Producto agregado exitosamente.");
            });
        }
    }
}