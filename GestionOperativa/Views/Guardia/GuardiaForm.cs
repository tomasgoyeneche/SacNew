using DevExpress.XtraGrid.Views.Grid;
using GestionOperativa.Presenters;
using Shared.Models;
using System.Data;

namespace GestionOperativa
{
    public partial class GuardiaForm : DevExpress.XtraEditors.XtraForm, IGuardiaView
    {
        public readonly GuardiaPresenter _presenter;

        public string PatenteIngresada
        {
            get => txtPatente.Text;
            set => txtPatente.Text = value;
        }

        public DateTime? FechaManual =>
            dtpIngreso.EditValue is DateTime fecha ? fecha : null;

        public DateTime? FechaSalidaManual =>
            dtpSalida.EditValue is DateTime fecha ? fecha : null;

        public GuardiaForm(GuardiaPresenter presenter)
        {
            InitializeComponent();
            _presenter = presenter;
            _presenter.SetView(this);
        }

        public void MostrarGuardia(List<GuardiaDto> guardias)
        {
            gridControlGuardia.DataSource = guardias;

            var view = gridViewGuardia;
            foreach (var col in new[] { "IdEntidad", "IdGuardiaIngreso", "TipoIngreso", "IdPosta", "IdEstadoEvento" })
            {
                if (view.Columns[col] != null)
                    view.Columns[col].Visible = false;
            }

            view.BestFitColumns();
        }

        public void MostrarResumenParador(int unidades, int tractores, int semis, int choferes)
        {
            txtUtIngresados.Text = unidades.ToString();
            txtTractoresIngresados.Text = tractores.ToString();
            txtSemis.Text = semis.ToString();
            txtChoferes.Text = choferes.ToString();
        }

        public void MostrarResumen(List<(string Descripcion, int Cantidad)> resumen)
        {
            gridControlResumen.DataSource = resumen
                .Select(r => new { r.Descripcion, r.Cantidad })
                .ToList();

            gridViewResumen.BestFitColumns();
        }

        public void MostrarHistorial(List<GuardiaHistorialDto> historial)
        {
            gridControlHistorico.DataSource = historial;

            var view = gridViewHistorico;
            foreach (var col in new[] { "IdGuardiaIngreso", "IdGuardiaEstado", "IdGuardiaRegistro" })
            {
                if (view.Columns[col] != null)
                    view.Columns[col].Visible = false;
            }

            if (view.Columns["FechaGuardia"] != null)
            {
                view.Columns["FechaGuardia"].DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
                view.Columns["FechaGuardia"].DisplayFormat.FormatString = "dd/MM/yyyy HH:mm";
            }

            view.BestFitColumns();
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

        private async void gridViewGuardia_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            if (gridViewGuardia.GetFocusedRow() is GuardiaDto guardia)
            {
                await _presenter.MostrarHistorialAsync(guardia.IdGuardiaIngreso);
                await _presenter.CargarVencimientosYAlertasAsync(guardia);
            }
        }

        public void MostrarMensaje(string mensaje)
        {
            MessageBox.Show(mensaje);
        }

        private async void dtpIngreso_EditValueChanged(object sender, EventArgs e)
        {
            await _presenter.RegistrarIngresoAsync(true); // Ingreso manual con fecha elegida
        }

        private async void bIngresar_Click(object sender, EventArgs e)
        {
            await _presenter.RegistrarIngresoAsync(false); // Ingreso normal (ahora)
        }

        private async void bIngresoOtros_Click(object sender, EventArgs e)
        {
            await _presenter.RegistrarOtrosAsync(); // Ingreso normal (ahora)
        }

        private async void bSalida_Click(object sender, EventArgs e)
        {
            if (gridViewGuardia.GetFocusedRow() is GuardiaDto guardia)
            {
                await _presenter.RegistrarSalidaAsync(guardia, false); // salida automática
            }
            else
            {
                MostrarMensaje("Debe seleccionar un registro para registrar la salida.");
            }
        }

        private async void dtpSalida_EditValueChanged(object sender, EventArgs e)
        {
            if (gridViewGuardia.GetFocusedRow() is GuardiaDto guardia)
            {
                await _presenter.RegistrarSalidaAsync(guardia, true); // salida manual
            }
            else
            {
                MostrarMensaje("Debe seleccionar un registro para registrar la salida.");
            }
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

        private async void gridViewGuardia_DoubleClick(object sender, EventArgs e)
        {
            if (gridViewGuardia.GetFocusedRow() is GuardiaDto guardia)
            {
                await _presenter.AbrirCambioEstadoAsync(guardia);
            }
        }

        private async void bNominaActual_Click(object sender, EventArgs e)
        {
            await _presenter.MostrarNominaActualAsync();
        }
    }
}