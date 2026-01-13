using DevExpress.XtraEditors;
using DevExpress.XtraGrid.Columns;
using GestionFlota.Presenters;
using SacNew.Views.GestionFlota.Postas.YpfIngresaConsumos.ImportarConsumos;
using Shared.Models;

namespace GestionFlota.Views.Postas.YpfIngresaConsumos.ImportarConsumos
{
    public partial class ImportarConsumosYpfForm : DevExpress.XtraEditors.XtraForm, IImportarConsumosYpfView
    {
        private readonly ImportarConsumosYpfPresenter _presenter;

        public ImportarConsumosYpfForm(ImportarConsumosYpfPresenter presenter)
        {
            InitializeComponent();
            _presenter = presenter;
            _presenter.SetView(this);
        }

        public DateTime PeriodoSeleccionado => dtpPeriodo.Value;

        public int QuincenaSeleccionada =>
        Convert.ToInt32(cmbQuincena.SelectedItem ?? "1");

        public void MostrarMensaje(string mensaje)
        {
            XtraMessageBox.Show(this, mensaje, "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        public void MostrarConsumos(List<ImportConsumoYpfEnRutaDto> consumos)
        {
            gridControlDatos.DataSource = consumos;
            var view = dgvDatos;
            foreach (GridColumn col in view.Columns)
            {
                if (col.FieldName.StartsWith("Id", StringComparison.OrdinalIgnoreCase))
                    col.Visible = false;
            }
            view.BestFitColumns();
        }

        public List<ImportConsumoYpfEnRutaDto> ObtenerConsumos()
        {
            return dgvDatos.DataSource as List<ImportConsumoYpfEnRutaDto> ?? new List<ImportConsumoYpfEnRutaDto>();
        }

        private async void btnExportarExcel_Click(object sender, EventArgs e)
        {
            using (var saveFileDialog = new SaveFileDialog { Filter = "Archivos Excel|*.xlsx" })
            {
                if (saveFileDialog.ShowDialog() == DialogResult.OK)
                {
                    await _presenter.ExportarConsumosAExcelAsync(saveFileDialog.FileName);
                }
            }
        }

        private async void btnImportar_Click(object sender, EventArgs e)
        {
            using (var openFileDialog = new OpenFileDialog { Filter = "Excel Files|*.xlsx;*.xls" })
            {
                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    await _presenter.ImportarConsumosAsync(openFileDialog.FileName);
                }
            }
        }

        private async void btnGuardar_Click(object sender, EventArgs e)
        {
            await _presenter.GuardarConsumosAsync();
            this.Dispose();
        }

        private async void guna2Button1_Click(object sender, EventArgs e)
        {
            if (PeriodoSeleccionado == DateTime.MinValue)
            {
                MostrarMensaje("Seleccione un período antes de buscar consumos.");
                return;
            }
            await _presenter.BuscarConsumosPorPeriodo();
        }

        private void ImportarConsumosYpfForm_Load(object sender, EventArgs e)
        {
            dtpPeriodo.Value = DateTime.Now;
            cmbQuincena.Items.Clear(); // 🔹 Limpia todos los elementos anteriores
            cmbQuincena.Items.AddRange(new[] { "1", "2" });
            cmbQuincena.SelectedIndex = 0;
        }
    }
}