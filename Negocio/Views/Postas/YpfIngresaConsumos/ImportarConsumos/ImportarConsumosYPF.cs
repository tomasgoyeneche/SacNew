using GestionFlota.Presenters;
using Shared.Models;

namespace SacNew.Views.GestionFlota.Postas.YpfIngresaConsumos.ImportarConsumos
{
    public partial class ImportarConsumosYPF : Form, IImportarConsumosYpfView
    {
        private readonly ImportarConsumosYpfPresenter _presenter;

        public ImportarConsumosYPF(ImportarConsumosYpfPresenter presenter)
        {
            InitializeComponent();
            _presenter = presenter;
            _presenter.SetView(this);
        }

        public Periodo? PeriodoSeleccionado => cmbPeriodos.SelectedItem as Periodo;

        public void CargarPeriodos(List<Periodo> periodos)
        {
            cmbPeriodos.DataSource = periodos;
            cmbPeriodos.DisplayMember = "NombrePeriodo";
            cmbPeriodos.ValueMember = "IdPeriodo";
        }

        public void MostrarMensaje(string mensaje)
        {
            MessageBox.Show(mensaje, "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        public void MostrarConsumos(List<ImportConsumoYpfEnRuta> consumos)
        {
            dgvConsumos.DataSource = consumos;

            dgvConsumos.Columns["idImportConsumoYPF"].Visible = false; // Ocultar columna ID
            dgvConsumos.Columns["Chequeado"].Visible = false; // Ocultar columna ID
        }

        public List<ImportConsumoYpfEnRuta> ObtenerConsumos()
        {
            return dgvConsumos.DataSource as List<ImportConsumoYpfEnRuta> ?? new List<ImportConsumoYpfEnRuta>();
        }

        private async void ImportarConsumosYPF_Load(object sender, EventArgs e)
        {
            await _presenter.CargarPeriodosAsync();
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

        private void guna2ControlBox4_Click(object sender, EventArgs e)
        {
            dgvConsumos.DataSource = null;
        }
    }
}