using DevExpress.Mvvm.Native;
using DevExpress.Utils;
using DevExpress.Utils.Extensions;
using DevExpress.XtraEditors;
using DevExpress.XtraPrinting;
using DevExpress.XtraScheduler;
using DevExpress.XtraScheduler;
using DevExpress.XtraScheduler.Drawing;
using GestionDocumental.Presenters;
using Shared.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GestionDocumental.Views.Novedades
{
    public partial class NovedadesChoferesCalendarioForm : DevExpress.XtraEditors.XtraForm, INovedadesChoferesCalendarioView
    {
        public readonly NovedadesChoferesCalendarioPresenter _presenter;

        public NovedadesChoferesCalendarioForm(NovedadesChoferesCalendarioPresenter presenter)
        {
            InitializeComponent();

            _presenter = presenter;
            _presenter.SetView(this);

            btnMesAnterior.Click += async (_, __) => await _presenter.MesAnteriorAsync();
            btnMesSiguiente.Click += async (_, __) => await _presenter.MesSiguienteAsync();
            btnRefrescar.Click += async (_, __) => await _presenter.CambiarMesAsync(MesSeleccionado);

            dateMes.EditValueChanged += DateMes_EditValueChanged;
        }

        public DateTime MesSeleccionado =>
            (dateMes.EditValue as DateTime?)?.Date
            ?? new DateTime(DateTime.Today.Year, DateTime.Today.Month, 1);
        public int PromedioAusenciasChofer
        {
            set
            {
                _promedioAusenciasChofer = value;
            }
        }
        private int _promedioAusenciasChofer;


        public void SetMesSeleccionado(DateTime fechaDelMes)
        {
            // Evita loop de evento
            dateMes.EditValueChanged -= DateMes_EditValueChanged;
            dateMes.EditValue = fechaDelMes;
            dateMes.EditValueChanged += DateMes_EditValueChanged;
        }

        private async void DateMes_EditValueChanged(object sender, EventArgs e)
        {
            if (dateMes.EditValue is DateTime dt)
                await _presenter.CambiarMesAsync(dt);
        }

        public void ConfigurarScheduler()
        {
            schedulerControlAusencias.DataStorage = schedulerStorageAusencias;
            schedulerControlAusencias.ActiveViewType = SchedulerViewType.Timeline;
            schedulerControlAusencias.GroupType = SchedulerGroupType.Resource;

            schedulerControlAusencias.OptionsCustomization.AllowDisplayAppointmentFlyout = false;
            schedulerControlAusencias.OptionsView.ToolTipVisibility = ToolTipVisibility.Always;

            var tv = schedulerControlAusencias.TimelineView;
            tv.Scales.Clear();
            tv.Scales.Add(new TimeScaleDay());
            tv.ShowMoreButtons = false;
            tv.TimelineScrollBarVisible = true;

            tv.GetBaseTimeScale().Width = 55;
        
            var c = schedulerControlAusencias.OptionsCustomization;
            c.AllowAppointmentCreate = UsedAppointmentType.None;
            c.AllowAppointmentEdit = UsedAppointmentType.None;
            c.AllowAppointmentDelete = UsedAppointmentType.None;
            c.AllowAppointmentDrag = UsedAppointmentType.None;

            schedulerControlAusencias.InitAppointmentDisplayText += (_, e) =>
                e.Text = e.Appointment.Subject;

            schedulerControlAusencias.AppointmentViewInfoCustomizing += Scheduler_AppointmentViewInfoCustomizing;
        }

        public void BindearScheduler(
            List<UnidadChoferResourceDto> choferes,
            List<UnidadChoferSchedulerDto> ausencias,
            DateTime desde,
            DateTime hasta)
        {
            var storage = schedulerStorageAusencias;

            storage.Resources.DataSource = choferes;
            storage.Resources.Mappings.Id = nameof(UnidadChoferResourceDto.IdEntidad);
            storage.Resources.Mappings.Caption = nameof(UnidadChoferResourceDto.Descripcion);

            storage.Appointments.DataSource = ausencias;
            storage.Appointments.Mappings.AppointmentId = nameof(UnidadChoferSchedulerDto.IdEstadoChoferUnidad);
            storage.Appointments.Mappings.ResourceId = nameof(UnidadChoferSchedulerDto.IdChoferUnidad);
            storage.Appointments.Mappings.Start = nameof(UnidadChoferSchedulerDto.Inicio);
            storage.Appointments.Mappings.End = nameof(UnidadChoferSchedulerDto.FinExclusivo);
            storage.Appointments.Mappings.Subject = nameof(UnidadChoferSchedulerDto.DescripcionEstado);
            storage.Appointments.Mappings.Description = nameof(UnidadChoferSchedulerDto.Observaciones);

            storage.Appointments.CustomFieldMappings.Clear();
            storage.Appointments.CustomFieldMappings.Add(
                new AppointmentCustomFieldMapping("IdEstado", nameof(UnidadChoferSchedulerDto.IdEstado))
            );
            AplicarRangoVisible(desde, hasta);
        }

        public void AplicarRangoVisible(DateTime desde, DateTime hasta)
        {
            var intervalo = new TimeInterval(desde, hasta);
            // 1) Restringe navegación/operaciones fuera del mes
            schedulerControlAusencias.LimitInterval = intervalo;
            // 2) En Timeline: evita “scroll infinito” (si no, el scroll no respeta bien el rango)
            schedulerControlAusencias.TimelineView.EnableInfiniteScrolling = false;
            // 3) Fuerza qué intervalo debe mostrarse (lo visible)
            schedulerControlAusencias.TimelineView.SetVisibleIntervals(
                new TimeIntervalCollection { intervalo }
            );
            // 4) Alinea el scroll al inicio del mes (opcional pero queda bien)
            schedulerControlAusencias.Start = desde;
            // 5) Refresca layout (en Timeline a veces hace falta)
            schedulerControlAusencias.TimelineView.LayoutChanged();
        }

        private const int ResourceTotalTopId = int.MaxValue - 1;
        private const int ResourceTotalBottomId = int.MaxValue;

        private void Scheduler_AppointmentViewInfoCustomizing(object sender, AppointmentViewInfoCustomizingEventArgs e)
        {
            var apt = e.ViewInfo.Appointment;

            // Tooltip prolijo (vale para todos)
            var obs = string.IsNullOrWhiteSpace(apt.Description) ? "—" : apt.Description.Trim();
            e.ViewInfo.ToolTipText =
                $"{apt.Subject}\n" +
                "────────────────────\n" +
                $"🕒 Inicio: {apt.Start:dd/MM/yyyy HH:mm}\n" +
                $"🏁 Final:  {apt.End:dd/MM/yyyy HH:mm}\n" +
                $"📝 Obs:    {obs}";

            var resourceId = apt.ResourceId is int r ? r : 0;

            // =========================
            // 🔵 FILA TOTAL (colores dinámicos)
            // =========================
            if ((resourceId == ResourceTotalTopId || resourceId == ResourceTotalBottomId) && (_presenter.TipoAusenciaMan == "CHOFER"))
            {
                int.TryParse(apt.Subject, out var total);

                var color = ObtenerColorPorTotal(total);
                e.ViewInfo.Appearance.BackColor = color;
                e.ViewInfo.Appearance.BorderColor = Oscurecer(color, 40);
                e.ViewInfo.Appearance.ForeColor = total >= (_promedioAusenciasChofer * 1.3) ? Color.White : Color.Black;
                e.ViewInfo.Appearance.Font = new Font(e.ViewInfo.Appearance.Font, FontStyle.Bold);

                e.ViewInfo.Appearance.TextOptions.VAlignment = VertAlignment.Top;
                e.ViewInfo.Appearance.TextOptions.HAlignment = HorzAlignment.Center;
                return;
            }


            // =========================
            // 🎨 CHOFERES NORMALES
            // =========================
            var idEstado = apt.CustomFields["IdEstado"] as int? ?? 0;
            var colorEstado = ObtenerColorPorEstado(idEstado);

            e.ViewInfo.Appearance.BackColor = colorEstado;
            e.ViewInfo.Appearance.BorderColor = Oscurecer(colorEstado, 40);
            e.ViewInfo.Appearance.ForeColor = Color.Black;
            e.ViewInfo.Appearance.Font = new Font(e.ViewInfo.Appearance.Font, FontStyle.Regular);
            e.ViewInfo.Appearance.TextOptions.VAlignment = VertAlignment.Top;

        }

        private Color ObtenerColorPorTotal(int total)
        {
            if (_promedioAusenciasChofer <= 0)
                return Color.White;

            var margen = (int)Math.Round(_promedioAusenciasChofer * 0.30, MidpointRounding.AwayFromZero);

            var limiteSuperior = _promedioAusenciasChofer + margen;
            var limiteInferior = _promedioAusenciasChofer - margen;

            if (total >= limiteSuperior)
                return Color.FromArgb(192, 0, 0); // 🔴 Rojo

            if (total <= limiteInferior)
                return Color.FromArgb(173, 216, 230); // 🔵 Azul

            return Color.White; // ⚪ Normal
        }

        private static Color Oscurecer(Color c, int delta)
            => Color.FromArgb(
                Math.Max(0, c.R - delta),
                Math.Max(0, c.G - delta),
                Math.Max(0, c.B - delta));

        private static Color ObtenerColorPorEstado(int idEstado) => idEstado switch
        {
            1 => Color.FromArgb(173, 216, 230), // Licencia - Azul claro
            2 => Color.FromArgb(255, 235, 156), // Franco/Vacaciones - Amarillo
            3 => Color.FromArgb(144, 238, 144), // Disponible - Verde claro
            4 => Color.FromArgb(255, 182, 193), // Enfermo - Rosa claro
            5 => Color.FromArgb(255, 160, 122), // Suspendido - Naranja claro

            6 => Color.FromArgb(221, 160, 221), // Violeta claro
            7 => Color.FromArgb(176, 224, 230), // Celeste pálido
            8 => Color.FromArgb(240, 230, 140), // Khaki claro
            9 => Color.FromArgb(175, 238, 238),
            10 => Color.FromArgb(255, 204, 153), // Durazno claro
            11 => Color.FromArgb(200, 200, 255), // Lavanda suave

            _ => Color.FromArgb(211, 211, 211)  // Gris neutro
        };

        public void MostrarMensaje(string mensaje)
        {
            XtraMessageBox.Show(this, mensaje, "Calendario",
                MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

    }
}