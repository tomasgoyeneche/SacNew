using Core.Base;
using Core.Repositories;
using Core.Services;
using GestionFlota.Views;
using Shared;
using Shared.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestionFlota.Presenters
{
    public class AsignarCargaPresenter : BasePresenter<IAsignarCargaView>
    {
        private readonly ILocacionRepositorio _locacionRepositorio;
        private readonly IProductoRepositorio _productoRepositorio;
        private readonly INominaRepositorio _nominaRepositorio;

        private readonly IProgramaRepositorio _programaRepositorio;
        private Cupeo _cupeoActual;

        public AsignarCargaPresenter(
            ISesionService sesionService,
            INavigationService navigationService,
            ILocacionRepositorio locacionRepositorio,
            IProductoRepositorio productoRepositorio,
            INominaRepositorio nominaRepositorio,
            IProgramaRepositorio programaRepositorio
            )
            : base(sesionService, navigationService)
        {
            _locacionRepositorio = locacionRepositorio;
            _productoRepositorio = productoRepositorio;
            _programaRepositorio = programaRepositorio;
            _nominaRepositorio = nominaRepositorio;
        }

        public async Task InicializarAsync(Cupeo cupeo)
        {
            _cupeoActual = cupeo;

            var origenes = await _locacionRepositorio.ObtenerTodasAsync();
            var destinos = await _locacionRepositorio.ObtenerTodasAsync();
            var productos = await _productoRepositorio.ObtenerTodosAsync();

            _view.CargarOrigenes(origenes, cupeo.IdOrigen);
            _view.CargarDestinos(destinos, cupeo.IdDestino);
            _view.CargarProductos(productos, cupeo.IdProducto);
        }

        public async Task ConfirmarAsignacionAsync()
        {
            // Si el viaje ya está confirmado
            if (_cupeoActual.Confirmado?.ToUpper() == "OK")
            {
                // Actualizar Programa
                await _programaRepositorio.ActualizarProgramaOrigenProductoAsync(
                    _cupeoActual.IdPrograma.Value,  // Id del programa actual
                    _view.IdOrigenSeleccionado ?? 0,
                    _view.IdProductoSeleccionado ?? 0);

                // Actualizar ProgramaTramo
                await _programaRepositorio.ActualizarProgramaTramoDestinoAsync(
                    _cupeoActual.IdPrograma.Value,
                    _view.IdDestinoSeleccionado ?? 0);

                _view.MostrarMensaje("Datos actualizados correctamente.");
                _view.Cerrar();
                return;
            }

            // Mapeo a Programa
            var programa = new Programa
            {
                IdDisponible = _cupeoActual.IdDisponible ?? 0,
                IdPedido = _cupeoActual.IdPedido ?? 0,
                IdOrigen = _view.IdOrigenSeleccionado ?? 0,
                IdProducto = _view.IdProductoSeleccionado ?? 0,
                Cupo = Convert.ToInt32(_cupeoActual.Cupo),
                AlbaranDespacho = Convert.ToInt32(_cupeoActual.AlbaranDespacho),
                PedidoOr = Convert.ToInt32(_cupeoActual.PedidoOr),
                Observaciones = _cupeoActual.Observaciones,
                IdProgramaEstado = 1, // Estado "Asignado"
                FechaCarga = _cupeoActual.FechaCarga,
                FechaEntrega = _cupeoActual.FechaEntrega
            };

            int idPrograma = await _programaRepositorio.InsertarProgramaRetornandoIdAsync(programa);

            var tramo = new ProgramaTramo
            {
                IdPrograma = idPrograma,
                IdNomina = _cupeoActual.IdNomina,
                IdDestino = _view.IdDestinoSeleccionado ?? 0,
                FechaInicio = _cupeoActual.FechaCarga,
                FechaFin = null // vacío
            };

            await _programaRepositorio.InsertarProgramaTramoAsync(tramo);

            _view.MostrarMensaje("Carga asignada correctamente.");
            _view.Cerrar();
        }

        public async Task RegistrarComentarioAsync(string comentario)
        {
            await _nominaRepositorio.RegistrarNominaAsync(
                _cupeoActual.IdNomina, // o el disponible.IdNomina según contexto
                "Comentario",
                comentario,
                _sesionService.IdUsuario
            );
            _view.MostrarMensaje("Comentario registrado correctamente.");
        }
    }
}
