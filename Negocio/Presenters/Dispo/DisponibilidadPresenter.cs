using Core.Base;
using Core.Repositories;
using Core.Services;
using GestionFlota.Views;
using Shared.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DevExpress.XtraEditors;

namespace GestionFlota.Presenters
{
    public class DisponibilidadPresenter : BasePresenter<IDisponibilidadView>
    {
        private readonly IDisponibilidadRepositorio _disponibilidadRepositorio;
        private readonly INominaRepositorio _nominaRepositorio;
        private readonly IAlertaRepositorio _alertaRepositorio;
        public DisponibilidadPresenter(
            IDisponibilidadRepositorio disponibilidadRepositorio,
            ISesionService sesionService,
            INavigationService navigationService
            , INominaRepositorio nominaRepositorio
            , IAlertaRepositorio alertaRepositorio
        ) : base(sesionService, navigationService)
        {
            _disponibilidadRepositorio = disponibilidadRepositorio;
            _nominaRepositorio = nominaRepositorio;
            _alertaRepositorio = alertaRepositorio;
        }

        public async Task InicializarAsync()
        {
            _view.ConfigurarControles();
            await BuscarDisponibilidadesAsync();
        }

        public async Task BuscarDisponibilidadesAsync()
        {
           DateTime fecha = _view.FechaSeleccionada;
           List<Disponibilidad> disponibilidades = await _disponibilidadRepositorio.BuscarDisponiblesPorFechaAsync(fecha);
           _view.CargarDisponibilidades(disponibilidades);
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


            _view.MostrarVencimientos(vencimientos.OrderBy(v => v.FechaVencimiento).ToList());
            _view.MostrarAlertas(alertas);
        }

        public void MostrarDetalleAlerta(AlertaDto alerta)
        {
            string mensaje = $"Alerta: {alerta.Descripcion}\nFecha: {alerta.Fecha:dd/MM/yyyy} - Si ya fue resuelta eliminar.";
            _view.MostrarMensaje(mensaje);
        }

        public async Task IntentarEditarDisponibilidadAsync(Disponibilidad dispo)
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
            List<VencimientosDto> vencidos = vencimientos.Where(v => v.FechaVencimiento.Date < _view.FechaSeleccionada).ToList();
            if (vencidos.Any())
            {
                foreach (var venc in vencidos)
                {
                    string msg = $"Vencimiento: {venc.Descripcion}\nVenció el: {venc.FechaVencimiento:dd/MM/yyyy}\n- Actualice vencimiento del documental.";
                    _view.MostrarMensaje(msg);
                }
                // Avisar que no se puede abrir el form
                _view.MostrarMensaje("No puede editar la disponibilidad hasta actualizar todos los vencimientos vencidos.");
                //return; // IMPORTANTEEEEEE
            }

            // 4. Si todo OK, abrir el form
            await AbrirFormularioAsync<AgregarEditarDisponibleForm>(async f =>
            {
                await f._presenter.InicializarAsync(dispo, _view.FechaSeleccionada); // Asumiendo que este presenter espera el dispo a editar
            });
            await BuscarDisponibilidadesAsync(); // Refrescar lista después de editar
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
                await CambiarEstadoDeBajaAsync(disponible, control.MotivoSeleccionado.Value);
        }

        public async Task CambiarEstadoDeBajaAsync(Disponible disponible, int? idMotivo)
        {
            disponible.IdDisponibleEstado = idMotivo;
            await _disponibilidadRepositorio.ActualizarDisponibleAsync(disponible);
            await BuscarDisponibilidadesAsync(); // Refrescar lista después de cambiar estado
        }
    }
}
