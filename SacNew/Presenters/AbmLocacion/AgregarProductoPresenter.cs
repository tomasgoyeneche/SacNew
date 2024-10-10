using SacNew.Interfaces;
using SacNew.Models;
using SacNew.Repositories;

namespace SacNew.Presenters
{
    public class AgregarProductoPresenter
    {
        private IAgregarProductoView _view;
        private readonly IProductoRepositorio _productoRepositorio;
        private readonly ILocacionProductoRepositorio _locacionProductoRepositorio;
        private int _idLocacion;

        public AgregarProductoPresenter(IProductoRepositorio productoRepositorio, ILocacionProductoRepositorio locacionProductoRepositorio)
        {
            _productoRepositorio = productoRepositorio;
            _locacionProductoRepositorio = locacionProductoRepositorio;
        }

        public void SetView(IAgregarProductoView view)
        {
            _view = view;
        }

        public async Task InicializarAsync(int idLocacion)
        {
            var productos = await _productoRepositorio.ObtenerTodosAsync();
            _idLocacion = idLocacion;
            _view.CargarProductos(productos);
        }

        public async void GuardarProducto()
        {
            try
            {
                var locacionProducto = new LocacionProducto
                {
                    IdLocacion = _idLocacion,
                    IdProducto = _view.ProductoSeleccionado
                };

                await _locacionProductoRepositorio.AgregarAsync(locacionProducto);
                _view.MostrarMensaje("Producto agregado exitosamente.");
            }
            catch (Exception ex)
            {
                _view.MostrarMensaje($"Error al agregar producto: {ex.Message}");
            }
        }
    }
}