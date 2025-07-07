using DevExpress.XtraEditors;
using GestionOperativa.Presenters;
using Shared.Models;

namespace GestionOperativa.Views
{
    public partial class EliminarEventoForm : DevExpress.XtraEditors.XtraForm, IEliminarEventoView
    {
        public readonly EliminarEventoPresenter _presenter;

        public EliminarEventoForm(EliminarEventoPresenter presenter)
        {
            InitializeComponent();
            _presenter = presenter;
            _presenter.SetView(this);
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

            gridViewHistorico.BestFitColumns();
        }

        public void MostrarMensaje(string mensaje)
        {
            XtraMessageBox.Show(this, mensaje, "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private async void gridViewHistorial_DoubleClick(object sender, EventArgs e)
        {
            if (gridViewHistorico.GetFocusedRow() is GuardiaHistorialDto registro)
            {
                var confirm = MessageBox.Show("¿Eliminar este evento?", "Confirmar", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (confirm == DialogResult.Yes)
                {
                    await _presenter.EliminarRegistroAsync(registro);
                }
            }
        }

        public void Close()
        {
            Dispose();
        }
    }
}