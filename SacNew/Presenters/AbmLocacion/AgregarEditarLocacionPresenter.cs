using Microsoft.Extensions.DependencyInjection;
using SacNew.Interfaces;
using SacNew.Models;
using SacNew.Repositories;
using SacNew.Views.Configuraciones.AbmLocaciones;

namespace SacNew.Presenters
{
    public class AgregarEditarLocacionPresenter
    {
        private IAgregarEditarLocacionView _view;
        private readonly ILocacionRepositorio _locacionRepositorio;
        private readonly ILocacionProductoRepositorio _locacionProductoRepositorio;
        private readonly ILocacionKilometrosEntreRepositorio _locacionKilometrosEntreRepositorio;
        private Locacion _locacionActual;
        private readonly IServiceProvider _serviceProvider;

        public AgregarEditarLocacionPresenter(
            ILocacionRepositorio locacionRepositorio,
            ILocacionProductoRepositorio locacionProductoRepositorio,
            ILocacionKilometrosEntreRepositorio locacionKilometrosEntreRepositorio
          , IServiceProvider serviceProvider)
        {
            _locacionRepositorio = locacionRepositorio;
            _locacionProductoRepositorio = locacionProductoRepositorio;
            _locacionKilometrosEntreRepositorio = locacionKilometrosEntreRepositorio;
            _serviceProvider = serviceProvider;
        }

        public void SetView(IAgregarEditarLocacionView view)
        {
            _view = view;
        }

        public async Task InicializarAsync(int? idLocacion)
        {
            if (idLocacion.HasValue)
            {
                // Editar locación existente
                _locacionActual = await _locacionRepositorio.ObtenerPorIdAsync(idLocacion.Value);
                _view.MostrarDatosLocacion(_locacionActual);

                // Cargar productos asociados a la locación
                await CargarProductosAsync(idLocacion.Value);

                // Cargar distancias entre locaciones
                await CargarKilometrosAsync(idLocacion.Value);
                _view.EstablecerModoEdicion(true);
            }
            else
            {
                // Nueva locación
                _locacionActual = new Locacion();
                _view.EstablecerModoEdicion(false);
            }
        }

        public async Task GuardarLocacionAsync()
        {
            _locacionActual.Nombre = _view.Nombre;
            _locacionActual.Direccion = _view.Direccion;
            _locacionActual.Carga = _view.Carga;
            _locacionActual.Descarga = _view.Descarga;
            _locacionActual.Activo = true;

            if (_locacionActual.IdLocacion == 0)
            {
                // Agregar nueva locación
                await _locacionRepositorio.AgregarAsync(_locacionActual);
                _view.MostrarMensaje("Locación agregada correctamente.");
            }
            else
            {
                // Actualizar locación existente
                await _locacionRepositorio.ActualizarAsync(_locacionActual);
                _view.MostrarMensaje("Locación actualizada correctamente.");
            }
        }

        public async Task EliminarProductoAsync(int idLocacionProducto)
        {
            var confirmacion = _view.ConfirmarEliminacion("¿Está seguro que desea eliminar este producto asociado?");
            if (confirmacion == DialogResult.Yes)
            {
                await _locacionProductoRepositorio.EliminarAsync(idLocacionProducto);

                // Refrescar lista de productos
                var productosCarga = await _locacionProductoRepositorio.ObtenerPorLocacionIdAsync(_locacionActual.IdLocacion);
                _view.CargarProductos(productosCarga);
            }
        }

        public async Task EliminarKilometrosAsync(int idKilometros)
        {
            var confirmacion = _view.ConfirmarEliminacion("¿Está seguro que desea eliminar esta locación-destino?");
            if (confirmacion == DialogResult.Yes)
            {
                await _locacionKilometrosEntreRepositorio.EliminarAsync(idKilometros);

                // Refrescar lista de distancias
                var kilometrosEntre = await _locacionKilometrosEntreRepositorio.ObtenerPorLocacionIdAsync(_locacionActual.IdLocacion);
                _view.CargarKilometros(kilometrosEntre);
            }
        }

        private async Task CargarProductosAsync(int idLocacion)
        {
            var productosCarga = await _locacionProductoRepositorio.ObtenerPorLocacionIdAsync(idLocacion);
            _view.CargarProductos(productosCarga);
        }

        private async Task CargarKilometrosAsync(int idLocacion)
        {
            var kilometrosEntre = await _locacionKilometrosEntreRepositorio.ObtenerPorLocacionIdAsync(idLocacion);
            _view.CargarKilometros(kilometrosEntre);
        }

        public void AgregarProducto()
        {
            var agregarProductoForm = _serviceProvider.GetService<AgregarProductoForm>();
            agregarProductoForm._presenter.InicializarAsync(_locacionActual.IdLocacion);

            agregarProductoForm.ShowDialog();
            CargarProductosAsync(_locacionActual.IdLocacion);
        }

        public void AgregarKilometro()
        {
            var agregarKilometroForm = _serviceProvider.GetService<AgregarKilometrosLocaciones>();
            agregarKilometroForm._presenter.InicializarAsync(_locacionActual.IdLocacion);

            agregarKilometroForm.ShowDialog();
            CargarKilometrosAsync(_locacionActual.IdLocacion);
        }
    }
}