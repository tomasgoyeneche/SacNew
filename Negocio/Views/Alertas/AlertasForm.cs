using DevExpress.XtraEditors;
using GestionFlota.Presenters;
using Shared.Models;

namespace GestionFlota.Views.Alertas
{
    public partial class AlertasForm : DevExpress.XtraEditors.XtraForm, IAlertasView
    {
        public readonly AlertasPresenter _presenter;

        public AlertasForm(AlertasPresenter presenter)
        {
            InitializeComponent();
            _presenter = presenter;
            _presenter.SetView(this);
        }

        public void MostrarAlertas(List<AlertaDto> alertas)
        {
            gridControlAlertas.DataSource = alertas;

            // Ocultamos columnas no deseadas desde el GridView
            var view = gridViewAlertas;

            foreach (var col in new[] { "IdAlerta", "IdNomina", "Activo" })
            {
                if (view.Columns[col] != null)
                    view.Columns[col].Visible = false;
            }

            view.BestFitColumns(); // Ajusta automáticamente las columnas al contenido

            gridViewAlertas.OptionsView.EnableAppearanceEvenRow = true;
            gridViewAlertas.OptionsView.EnableAppearanceOddRow = true;
            gridViewAlertas.Appearance.Row.Font = new Font("Segoe UI", 9);
            gridViewAlertas.Appearance.HeaderPanel.Font = new Font("Segoe UI Semibold", 9.75f);
            gridViewAlertas.Appearance.HeaderPanel.Options.UseFont = true;
            gridViewAlertas.Appearance.Row.Options.UseFont = true;
        }

        public DialogResult ConfirmarEliminacion(string mensaje)
        {
            return MessageBox.Show(mensaje, "Confirmar eliminación", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
        }

        public void MostrarMensaje(string mensaje)
        {
            XtraMessageBox.Show(this, mensaje, "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private async void btnEliminar_Click(object sender, EventArgs e)
        {
            AlertaDto row = gridViewAlertas.GetFocusedRow() as AlertaDto;
            if (row != null)
            {
                await _presenter.EliminarAlertaAsync(row.IdAlerta);
            }
            else
            {
                MostrarMensaje("Seleccione una Novedad para eliminar.");
            }
        }

        private async void btnAgregarNovedad_Click(object sender, EventArgs e)
        {
            await _presenter.AgregarAlertaAsync();
        }

        private async void btnEditarNovedad_Click(object sender, EventArgs e)
        {
            AlertaDto row = gridViewAlertas.GetFocusedRow() as AlertaDto;
            if (row != null)
            {
                await _presenter.EditarAlertaAsync(row.IdAlerta);
            }
            else
            {
                MostrarMensaje("Seleccione una Novedad para editar.");
            }
        }
    }
}