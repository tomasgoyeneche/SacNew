using Newtonsoft.Json;
using SacNew.Interfaces;
using SacNew.Models;
using SacNew.Repositories;
using SacNew.Services;

namespace SacNew.Presenters
{
    public class AgregarEditarLocacionPresenter
    {
        private IAgregarEditarLocacionView _view;
        private readonly ILocacionRepositorio _locacionRepositorio;
        private readonly ILocacionProductoRepositorio _locacionProductoRepositorio;
        private readonly ILocacionKilometrosEntreRepositorio _locacionKilometrosEntreRepositorio;
        private readonly IAuditoriaService _auditoriaService;
        private Locacion _locacionActual;

        public AgregarEditarLocacionPresenter(
            ILocacionRepositorio locacionRepositorio,
            ILocacionProductoRepositorio locacionProductoRepositorio,
            ILocacionKilometrosEntreRepositorio locacionKilometrosEntreRepositorio,
            IAuditoriaService auditoriaService
            )
        {
            _locacionRepositorio = locacionRepositorio;
            _locacionProductoRepositorio = locacionProductoRepositorio;
            _locacionKilometrosEntreRepositorio = locacionKilometrosEntreRepositorio;
            _auditoriaService = auditoriaService;
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
            string valoresAnteriores = null;

            if (_locacionActual.IdLocacion != 0)
            {
                // Si es una edición, convertimos los valores anteriores a JSON para la auditoría
                valoresAnteriores = JsonConvert.SerializeObject(_locacionActual);
            }

            // Guardar la locación actual
            _locacionActual.Nombre = _view.Nombre;
            _locacionActual.Carga = _view.Carga;
            _locacionActual.Descarga = _view.Descarga;
            _locacionActual.Activo = true;

            string accion;
            string valoresNuevos = JsonConvert.SerializeObject(_locacionActual); // Convertir los valores nuevos a JSON

            if (_locacionActual.IdLocacion == 0)
            {
                // Agregar nueva locación
                await _locacionRepositorio.AgregarAsync(_locacionActual);
                accion = "Agregar";
            }
            else
            {
                // Actualizar locación existente
                await _locacionRepositorio.ActualizarAsync(_locacionActual);
                accion = "Editar";
            }

            // Registrar auditoría después de agregar o editar
            await _auditoriaService.RegistrarAuditoriaAsync(
                "Locacion",
                accion,
                _locacionActual.IdLocacion,
                valoresAnteriores,
                valoresNuevos
            );

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