using GestionFlota.Presenters;
using SacNew.Views.GestionFlota.Postas.IngresaConsumos.ImportConsumos;
using Shared.Models;

namespace SacNew.Views.GestionFlota.Postas.IngresaConsumos
{
    public partial class ImportaDatosMv : Form, ImportaDatosMvView
    {
        private readonly ImportDatosMvPresenter _presenter;

        public ImportaDatosMv(ImportDatosMvPresenter presenter)
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

        public void MostrarConsumos(List<ImportMercadoVictoria> consumos)
        {
            dgvConsumos.DataSource = consumos;
            dgvConsumos.Columns["IdImportMercadoVictoria"].Visible = false; // Ocultar columna ID
        }

        public List<ImportMercadoVictoria> ObtenerConsumos()
        {
            return dgvConsumos.DataSource as List<ImportMercadoVictoria> ?? new List<ImportMercadoVictoria>();
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

        private async void ImportaDatosMv_Load(object sender, EventArgs e)
        {
            await _presenter.CargarPeriodosAsync();
        }
    }
}