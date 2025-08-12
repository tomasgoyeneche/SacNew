using DevExpress.XtraEditors;
using DevExpress.XtraGrid.Views.Grid;
using GestionFlota.Presenters;
using Shared.Models;

namespace GestionFlota.Views
{
    public partial class DisponibilidadForm : DevExpress.XtraEditors.XtraForm, IDisponibilidadView
    {
        public readonly DisponibilidadPresenter _presenter;

        public DisponibilidadForm(DisponibilidadPresenter presenter)
        {
            InitializeComponent();
            _presenter = presenter;
            _presenter.SetView(this);
        }

        public void ConfigurarControles()
        {
            dateEditFecha.EditValue = DateTime.Today.AddDays(1);
        }

        public DateTime FechaSeleccionada => dateEditFecha.EditValue is DateTime fecha ? fecha : DateTime.Today.AddDays(1);

        public void CargarDisponibilidades(List<Disponibilidad> disponibilidades)
        {
            gridControlDisponibles.DataSource = disponibilidades;
            var view = gridViewDisponibles;
            //view.BestFitColumns();

            int countCheck = disponibilidades.Count(d => d.DisOrigen != null);
            int countNoCheck = disponibilidades.Count - countCheck;

            lblCheck.Text = $"Check: {countCheck}";
            lblNoCheck.Text = $"No Check: {countNoCheck}";
        }

        public void MostrarMensaje(string mensaje)
        {
            XtraMessageBox.Show(mensaje, "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        public void Close()
        {
            this.Close();
        }

        private void btnCerrar_Click(object sender, EventArgs e)
        {
            Close();
        }

        private async void bBuscarDisponibles_Click(object sender, EventArgs e)
        {
            await _presenter.BuscarDisponibilidadesAsync();
        }

        public void MostrarVencimientos(List<VencimientosDto> vencimientos)
        {
            gridControlVencimientos.DataSource = vencimientos;

            var view = gridViewVencimientos;
            foreach (var col in new[] { "Entidad" })
            {
                if (view.Columns[col] != null)
                    view.Columns[col].Visible = false;
            }

            gridViewVencimientos.BestFitColumns();
        }

        public void MostrarAlertas(List<AlertaDto> alertas)
        {
            gridControlAlertas.DataSource = alertas;

            var view = gridViewAlertas;
            foreach (var col in new[] { "IdAlerta", "IdNomina", "Activo", "PatenteSemi" })
            {
                if (view.Columns[col] != null)
                    view.Columns[col].Visible = false;
            }

            gridViewAlertas.BestFitColumns();
        }

        public void MostrarHistorial(List<HistorialGeneralDto> historial)
        {
            gridControlHistorico.DataSource = historial;

            var view = gridViewHistorico;

            //if (view.Columns["Fecha"] != null)
            //{
            //    view.Columns["Fecha"].DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            //    view.Columns["Fecha"].DisplayFormat.FormatString = "dd/MM/yyyy HH:mm";
            //}

            view.BestFitColumns();
        }

        private void gridViewVencimientos_RowCellStyle(object sender, DevExpress.XtraGrid.Views.Grid.RowCellStyleEventArgs e)
        {
            if (e.RowHandle < 0) return;

            var view = sender as GridView;
            var vencimiento = view.GetRow(e.RowHandle) as VencimientosDto;

            if (vencimiento == null || e.Column.FieldName != "FechaVencimiento")
                return;

            var fecha = vencimiento.FechaVencimiento.Date;
            var hoy = DateTime.Now.Date;

            if (fecha < hoy)
            {
                e.Appearance.BackColor = Color.LightCoral; // rojo
            }
            else if ((fecha - hoy).TotalDays <= 7)
            {
                e.Appearance.BackColor = Color.Khaki; // amarillo
            }
            else
            {
                e.Appearance.BackColor = Color.LightGreen; // verde
            }
        }

        private async void gridViewDisponibles_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            if (gridViewDisponibles.GetFocusedRow() is Disponibilidad dispo)
            {
                await _presenter.CargarVencimientosYAlertasAsync(dispo);
            }
        }

        private void gridViewAlertas_DoubleClick(object sender, EventArgs e)
        {
            if (gridViewAlertas.GetFocusedRow() is AlertaDto alerta)
            {
                _presenter.MostrarDetalleAlerta(alerta);
            }
        }

        private async void bAgregarDispo_Click(object sender, EventArgs e)
        {
            if (gridViewDisponibles.GetFocusedRow() is Disponibilidad dispo)
            {
                int idAgregarDispo = dispo.IdNomina;
                await _presenter.IntentarEditarDisponibilidadAsync(dispo, idAgregarDispo);
            }
        }

        public void SeleccionarDispoPorNomina(int IdNomina)
        {
            var view = gridViewDisponibles;
            for (int i = 0; i < view.RowCount; i++)
            {
                var row = view.GetRow(i) as Disponibilidad;
                if (row != null && row.IdNomina == IdNomina)
                {
                    view.FocusedRowHandle = i;
                    view.SelectRow(i); // Opcional: resalta la fila
                    break;
                }
            }
        }

        private async void bCancelarDisponible_Click(object sender, EventArgs e)
        {
            if (gridViewDisponibles.GetFocusedRow() is Disponibilidad dispo)
            {
                await _presenter.MostrarSelectorDeMotivoBajaAsync(dispo);
            }
        }

        private async void gridViewDisponibles_RowCellClick(object sender, RowCellClickEventArgs e)
        {
            if (e.Clicks == 2 && e.RowHandle >= 0 && gridViewDisponibles.GetFocusedRow() is Disponibilidad dispo) // Es doble click sobre una celda válida
            {
                int idAgregarDispo = dispo.IdNomina;
                await _presenter.IntentarEditarDisponibilidadAsync(dispo, idAgregarDispo);
            }
        }

        private async void bDisponibilidadYpf_Click(object sender, EventArgs e)
        {
            await _presenter.MostrarSelectorFechasYPFAsync();
        }

        private void gridControlHistorico_DoubleClick(object sender, EventArgs e)
        {
            if (gridViewHistorico.GetFocusedRow() is HistorialGeneralDto historico)
            {
                MostrarMensaje($"Mensaje: {historico.Descripcion}");
            }
        }

        private async void dateEditFecha_DateChanged(object sender, EventArgs e)
        {
            await _presenter.BuscarDisponibilidadesAsync();
        }
    }
}