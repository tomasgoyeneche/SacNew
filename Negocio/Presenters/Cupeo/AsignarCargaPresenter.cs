using Configuraciones.Views;
using Core.Base;
using Core.Reports;
using Core.Repositories;
using Core.Services;
using DevExpress.Office.Utils;
using DevExpress.XtraEditors;
using GestionFlota.Views;
using GestionFlota.Views.Cupeo;
using GestionOperativa.Reports;
using Shared;
using Shared.Models;
using Shared.Models.DTOs;
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
        private readonly IDisponibilidadRepositorio _disponibilidadRepositorio;

        private readonly IProgramaRepositorio _programaRepositorio;
        private Cupeo _cupeoActual;

        public AsignarCargaPresenter(
            ISesionService sesionService,
            INavigationService navigationService,
            ILocacionRepositorio locacionRepositorio,
            IProductoRepositorio productoRepositorio,
            INominaRepositorio nominaRepositorio,
            IDisponibilidadRepositorio disponibilidadRepositorio,
            IProgramaRepositorio programaRepositorio
            )
            : base(sesionService, navigationService)
        {
            _locacionRepositorio = locacionRepositorio;
            _productoRepositorio = productoRepositorio;
            _programaRepositorio = programaRepositorio;
            _nominaRepositorio = nominaRepositorio;
            _disponibilidadRepositorio = disponibilidadRepositorio;
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

            var origenes = await _locacionRepositorio.ObtenerTodasAsync();
            var productos = await _productoRepositorio.ObtenerTodosAsync();
            var destinos = await _locacionRepositorio.ObtenerTodasAsync();

            var nombreOrigen = origenes.FirstOrDefault(o => o.IdLocacion == _view.IdOrigenSeleccionado)?.Nombre ?? "";
            var nombreDestino = destinos.FirstOrDefault(d => d.IdLocacion == _view.IdDestinoSeleccionado)?.Nombre ?? "";
            var nombreProducto = productos.FirstOrDefault(p => p.IdProducto == _view.IdProductoSeleccionado)?.Nombre ?? "";

            string motivo = "Asignado";
            string descripcion = $"{nombreOrigen} => {nombreDestino} | {nombreProducto} {_cupeoActual.AlbaranDespacho}";

            await _nominaRepositorio.RegistrarNominaAsync(
                _cupeoActual.IdNomina,
                motivo,
                descripcion,
                _sesionService.IdUsuario
            );

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

        public async Task AbrirOrdenCarga()
        {
            await EjecutarConCargaAsync(async () =>
            {
                // Obtener los datos desde el repositorio

                // Crear una instancia del nuevo reporte DevExpress
                var reporte = new ReporteOrdenCarga();
                reporte.DataSource = new List<Cupeo> { _cupeoActual };
                reporte.DataMember = "";

                await AbrirFormularioAsync<VisualizadorReportesDevForm>(form =>
                {
                    form.MostrarReporteDevExpress(reporte);
                    return Task.CompletedTask;
                });
            });
        }

        public async Task CancelarAsignacionAsync()
        {
            // Si está confirmado (ya existe programa)
            if (_cupeoActual.Confirmado?.ToUpper() == "OK")
            {
                var programa = await _programaRepositorio.ObtenerPorIdAsync(_cupeoActual.IdPrograma.Value);
                if (programa == null)
                {
                    _view.MostrarMensaje("No se encontró ningún Programa para la fecha seleccionada.");
                    return;
                }

                // Seleccionar motivo de baja para programa
                var motivos = await _programaRepositorio.ObtenerEstadosDeBajaAsync();
                var control = new MotivoBajaAsignadoControl(motivos);
                if (XtraDialog.Show(control, "Seleccione motivo de baja", MessageBoxButtons.OKCancel) == DialogResult.OK
                    && control.MotivoSeleccionado.HasValue)
                {
                    await CambiarEstadoDeBajaProgramaAsync(programa, control.MotivoSeleccionado.Value);
                    _view.Cerrar();
                }
                return;
            }

            // Si no está confirmado: cancelar disponible
            var disponible = await _disponibilidadRepositorio.ObtenerPorIdAsync(_cupeoActual.IdDisponible.Value);
            if (disponible == null)
            {
                _view.MostrarMensaje("No se encontró ningún disponible para la fecha seleccionada.");
                return;
            }

            var motivosDisp = await _disponibilidadRepositorio.ObtenerEstadosDeBajaAsync();
            var controlDisp = new MotivoBajaSelectorControl(motivosDisp);
            if (XtraDialog.Show(controlDisp, "Seleccione motivo de baja", MessageBoxButtons.OKCancel) == DialogResult.OK
                && controlDisp.MotivoSeleccionado.HasValue)
            {
                await CambiarEstadoDeBajaDisponibleAsync(disponible, controlDisp.MotivoSeleccionado.Value);
                _view.Cerrar();
            }
        }

        private async Task CambiarEstadoDeBajaDisponibleAsync(Disponible disponible, int idMotivo)
        {
            disponible.IdDisponibleEstado = idMotivo;
            await _disponibilidadRepositorio.ActualizarDisponibleAsync(disponible);

            // Log en NominaRegistro
            var motivo = await _disponibilidadRepositorio.ObtenerEstadoDeBajaPorIdAsync(idMotivo);
            await _nominaRepositorio.RegistrarNominaAsync(
                disponible.IdNomina,
                "Cancela Disponible",
                motivo?.Descripcion ?? "Sin motivo",
                _sesionService.IdUsuario
            );
        }

        private async Task CambiarEstadoDeBajaProgramaAsync(Programa programa, int idMotivo)
        {
            programa.IdProgramaEstado = idMotivo;
            await _programaRepositorio.ActualizarProgramaAsync(programa);

            await _programaRepositorio.CerrarTramosActivosPorProgramaAsync(programa.IdPrograma);

            // Log en ProgramaRegistro
            var motivo = await _programaRepositorio.ObtenerEstadoDeBajaPorIdAsync(idMotivo);
            await _programaRepositorio.RegistrarProgramaAsync(
                programa.IdPrograma,
                "Cancela Asignado",
                motivo?.Descripcion ?? "Sin motivo",
                _sesionService.IdUsuario
            );
        }

    }
}
