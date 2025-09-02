using Core.Base;
using Core.Repositories;
using Core.Services;
using DevExpress.XtraEditors;
using GestionFlota.Views;
using Shared.Models;
using System.IO;

namespace GestionFlota.Presenters
{
    public class DisponibilidadPresenter : BasePresenter<IDisponibilidadView>
    {
        private readonly IDisponibilidadRepositorio _disponibilidadRepositorio;
        private readonly INominaRepositorio _nominaRepositorio;
        private readonly IAlertaRepositorio _alertaRepositorio;
        private readonly IExcelService _excelService;

        public DisponibilidadPresenter(
            IDisponibilidadRepositorio disponibilidadRepositorio,
            ISesionService sesionService,
            INavigationService navigationService
            , IExcelService excelService
            , INominaRepositorio nominaRepositorio
            , IAlertaRepositorio alertaRepositorio
        ) : base(sesionService, navigationService)
        {
            _disponibilidadRepositorio = disponibilidadRepositorio;
            _nominaRepositorio = nominaRepositorio;
            _alertaRepositorio = alertaRepositorio;
            _excelService = excelService;
        }

        public async Task InicializarAsync()
        {
            _view.ConfigurarControles();
            await BuscarDisponibilidadesAsync();
        }

        public async Task BuscarDisponibilidadesAsync(int? idNominaSeleccionada = null)
        {
            try
            {
                _view.SetEstadoCargaDisponibles(true); // Bloquea y limpia

                DateTime fecha = _view.FechaSeleccionada;
                List<Disponibilidad> disponibilidades =
                    await _disponibilidadRepositorio.BuscarDisponiblesPorFechaAsync(fecha);

                _view.CargarDisponibilidades(disponibilidades);
            }
            finally
            {
                _view.SetEstadoCargaDisponibles(false); // Desbloquea

                if (idNominaSeleccionada.HasValue)
                {
                    _view.SeleccionarDispoPorNomina(idNominaSeleccionada.Value);
                }
            }
        }

        public async Task CargarVencimientosYAlertasAsync(Disponibilidad dispo)
        {
            List<VencimientosDto> vencimientos = new();
            List<AlertaDto> alertas = new();
            DateTime fechaLimite = _view.FechaSeleccionada.AddDays(20);

            Nomina? nomina = await _nominaRepositorio.ObtenerPorIdAsync(dispo.IdNomina);
            List<VencimientosDto> vencNomina = await _nominaRepositorio.ObtenerVencimientosPorNominaAsync(nomina);
            vencimientos.AddRange(vencNomina.Where(v => v.FechaVencimiento <= fechaLimite));

            List<AlertaDto> alertasNomina = await _alertaRepositorio.ObtenerAlertasPorIdNominaAsync(dispo.IdNomina);
            alertas.AddRange(alertasNomina);

            var historial = await _nominaRepositorio.ObtenerHistorialPorNomina(dispo.IdNomina);

            _view.MostrarHistorial(historial);
            _view.MostrarVencimientos(vencimientos.OrderBy(v => v.FechaVencimiento).ToList());
            _view.MostrarAlertas(alertas);
        }

        public void MostrarDetalleAlerta(AlertaDto alerta)
        {
            string mensaje = $"Alerta: {alerta.Descripcion}\nFecha: {alerta.Fecha:dd/MM/yyyy} - Si ya fue resuelta eliminar.";
            _view.MostrarMensaje(mensaje);
        }

        public async Task IntentarEditarDisponibilidadAsync(Disponibilidad dispo, int? idNominaSeleccionada = null)
        {
            // 1. Cargar alertas y vencimientos
            List<AlertaDto> alertas = await _alertaRepositorio.ObtenerAlertasPorIdNominaAsync(dispo.IdNomina);
            List<VencimientosDto> vencimientos = await _nominaRepositorio.ObtenerVencimientosPorNominaAsync(
                await _nominaRepositorio.ObtenerPorIdAsync(dispo.IdNomina)
            );

            // 2. Mostrar alertas pendientes
            foreach (var alerta in alertas)
            {
                string msg = $"Alerta: {alerta.Descripcion}\nFecha: {alerta.Fecha:dd/MM/yyyy}\n¿Ya fue resuelta? Si es así, elimínela.";
                _view.MostrarMensaje(msg);
                // (Opcional: podrías ofrecer botones Sí/No si querés marcar como resuelta desde aquí)
            }

            // 3. Si hay vencimientos vencidos, mostrar y abortar apertura del form
            List<VencimientosDto> vencidos = vencimientos
            .Where(v => v.FechaVencimiento.Date < _view.FechaSeleccionada)
            .ToList();

            bool bloquear = false;

            foreach (var venc in vencidos)
            {
                // Mensaje siempre
                string msg = $"Vencimiento: {venc.Descripcion}\n" +
                             $"Venció el: {venc.FechaVencimiento:dd/MM/yyyy}\n" +
                             "- Actualice vencimiento del documental.";
                _view.MostrarMensaje(msg);

                if (venc.Descripcion.Equals("CheckList", StringComparison.OrdinalIgnoreCase))
                {
                    // calcular cuántos días pasaron desde el vencimiento hasta hoy
                    var diasVencidos = (DateTime.Now.Date - venc.FechaVencimiento.Date).TotalDays;

                    if (diasVencidos > 7)
                    {
                        bloquear = true; // demasiado vencido
                    }
                    // si es ≤ 7 días → no bloquea, solo avisa
                }
                else
                {
                    bloquear = true; // cualquier otro documental bloquea
                }
            }

            if (bloquear)
            {
                _view.MostrarMensaje("No puede editar la disponibilidad hasta actualizar todos los vencimientos vencidos.");
                return; // IMPORTANTEEEEEE
            }

            // 4. Si todo OK, abrir el form
            await AbrirFormularioAsync<AgregarEditarDisponibleForm>(async f =>
            {
                await f._presenter.InicializarAsync(dispo, _view.FechaSeleccionada); // Asumiendo que este presenter espera el dispo a editar
            });
            await BuscarDisponibilidadesAsync(idNominaSeleccionada); // Refrescar lista después de editar
        }

        public async Task MostrarSelectorDeMotivoBajaAsync(Disponibilidad disponibilidad)
        {
            var disponible = await _disponibilidadRepositorio.ObtenerDisponiblePorNominaYFechaAsync(disponibilidad.IdNomina, disponibilidad.DispoFecha);

            if (disponible == null)
            {
                _view.MostrarMensaje("No se encontró ningún disponible para la fecha seleccionada.");
                return;
            }

            var motivos = await _disponibilidadRepositorio.ObtenerEstadosDeBajaAsync();
            var control = new MotivoBajaSelectorControl(motivos);
            var dialogResult = XtraDialog.Show(control, "Seleccione motivo de baja", MessageBoxButtons.OKCancel);

            if (dialogResult == DialogResult.OK && control.MotivoSeleccionado.HasValue)
                await CambiarEstadoDeBajaAsync(disponible, control.MotivoSeleccionado.Value, control.Observacion);
        }

        public async Task CambiarEstadoDeBajaAsync(Disponible disponible, int idMotivo, string observacion)
        {
            disponible.IdDisponibleEstado = idMotivo;
            disponible.Observaciones = observacion;
            await _disponibilidadRepositorio.ActualizarDisponibleAsync(disponible);

            // Obtener motivo para descripción
            DisponibleEstado? motivo = await _disponibilidadRepositorio.ObtenerEstadoDeBajaPorIdAsync(idMotivo);

            await _nominaRepositorio.RegistrarNominaAsync(
                disponible.IdNomina,
                "Cancela Disponible",
                 $"{motivo?.Descripcion ?? "Sin motivo"} - {observacion}".Trim(),
                _sesionService.IdUsuario
            );

            await BuscarDisponibilidadesAsync(); // Refrescar lista después de cambiar estado
        }

        public async Task MostrarSelectorFechasYPFAsync()
        {
            var fechas = await _disponibilidadRepositorio.ObtenerProximasFechasDisponiblesAsync(DateTime.Today.AddDays(1), 5);
            var control = new DispoYPFSelectorControl();
            control.CargarFechas(fechas);

            control.FechaSeleccionada += async (s, fechaSeleccionada) =>
            {
                await ExportarDisponibilidadYPF(fechaSeleccionada);
                // Cerrar ventana:
                ((control.ParentForm) as Form)?.Close();
            };

            // Mostralo modal
            XtraDialog.Show(control, "Seleccione una fecha de disponibilidad YPF", MessageBoxButtons.OK);
        }

        private async Task ExportarDisponibilidadYPF(DateTime dispoFecha)
        {
            List<DisponibilidadYPF> lista = await _disponibilidadRepositorio.ObtenerDisponibilidadYPFPorFechaAsync(dispoFecha);
            if (lista == null || !lista.Any())
            {
                _view.MostrarMensaje("No hay datos para exportar para la fecha seleccionada.");
                return;
            }

            string carpeta = @"C:\Compartida\Exportaciones";
            if (!Directory.Exists(carpeta))
                Directory.CreateDirectory(carpeta);

            string fileName = Path.Combine(carpeta, $"DispoYPF-{dispoFecha:dd-MM-yy}.xlsx");

            await _excelService.ExportarAExcelAsync(lista, fileName, "DispoYPF");

            // Abrir el archivo con el programa por defecto
            System.Diagnostics.Process.Start(new System.Diagnostics.ProcessStartInfo
            {
                FileName = fileName,
                UseShellExecute = true
            });
        }

        public async Task EliminarAlertaAsync(AlertaDto alerta, int idNominaSeleccionada)
        {

            // Eliminar en repositorio
            await _alertaRepositorio.EliminarAlertaAsync(alerta.IdAlerta);

            _view.MostrarMensaje("La alerta se eliminó correctamente.");

            // Refrescar vencimientos y alertas del ruteo actualmente seleccionado
            await BuscarDisponibilidadesAsync(idNominaSeleccionada);
        }

    }
}