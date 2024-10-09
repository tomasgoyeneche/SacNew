using SacNew.Interfaces;
using SacNew.Models;
using SacNew.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SacNew.Presenters
{
    public class AgregarEditarLocacionPresenter
    {
        private IAgregarEditarLocacionView _view;
        private readonly ILocacionRepositorio _locacionRepositorio;
        private readonly ILocacionProductoRepositorio _locacionProductoRepositorio;
        private readonly ILocacionKilometrosEntreRepositorio _locacionKilometrosEntreRepositorio;
        private Locacion _locacionActual;

        public AgregarEditarLocacionPresenter(
            ILocacionRepositorio locacionRepositorio,
            ILocacionProductoRepositorio locacionProductoRepositorio,
            ILocacionKilometrosEntreRepositorio locacionKilometrosEntreRepositorio)
        {
            _locacionRepositorio = locacionRepositorio;
            _locacionProductoRepositorio = locacionProductoRepositorio;
            _locacionKilometrosEntreRepositorio = locacionKilometrosEntreRepositorio;
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
                var productosCarga = await _locacionProductoRepositorio.ObtenerPorLocacionIdAsync(idLocacion.Value);
                _view.CargarProductos(productosCarga);

                // Cargar distancias entre locaciones
                var kilometrosEntre = await _locacionKilometrosEntreRepositorio.ObtenerPorLocacionIdAsync(idLocacion.Value);
                _view.CargarKilometros(kilometrosEntre);
            }
            else
            {
                // Nueva locación
                _locacionActual = new Locacion();
            }
        }

        public async Task GuardarLocacionAsync()
        {
            _locacionActual.Nombre = _view.Nombre;
            _locacionActual.Carga = _view.Carga;
            _locacionActual.Descarga = _view.Descarga;
            _locacionActual.Activo = true;

            if (_locacionActual.IdLocacion == 0)
            {
                // Agregar nueva locación
                await _locacionRepositorio.AgregarAsync(_locacionActual);
            }
            else
            {
                // Actualizar locación existente
                await _locacionRepositorio.ActualizarAsync(_locacionActual);
            }

            _view.MostrarMensaje("Locación guardada correctamente.");
        }

        public async Task EliminarProductoAsync(int idLocacionProducto)
        {
            await _locacionProductoRepositorio.EliminarAsync(idLocacionProducto);

            // Refrescar lista de productos
            var productosCarga = await _locacionProductoRepositorio.ObtenerPorLocacionIdAsync(_locacionActual.IdLocacion);
            _view.CargarProductos(productosCarga);
        }

        public async Task EliminarKilometrosAsync(int idKilometros)
        {
            await _locacionKilometrosEntreRepositorio.EliminarAsync(idKilometros);

            // Refrescar lista de distancias
            var kilometrosEntre = await _locacionKilometrosEntreRepositorio.ObtenerPorLocacionIdAsync(_locacionActual.IdLocacion);
            _view.CargarKilometros(kilometrosEntre);
        }
    }
}
