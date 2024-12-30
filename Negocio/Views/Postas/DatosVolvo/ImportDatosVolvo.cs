using GestionFlota.Presenters;
using Shared.Models;

namespace SacNew.Views.GestionFlota.Postas.DatosVolvo
{
    public partial class ImportDatosVolvo : Form, IImportarVolvoConnectView
    {
        private readonly ImportarVolvoConnectPresenter _presenter;

        public ImportDatosVolvo(ImportarVolvoConnectPresenter presenter)
        {
            InitializeComponent();
            _presenter = presenter;
            _presenter.SetView(this);
        }

        public Periodo? PeriodoSeleccionado => cmbPeriodos.SelectedItem as Periodo;

        public void CargarPeriodos(IEnumerable<Periodo> periodos)
        {
            cmbPeriodos.DataSource = periodos.ToList();
            cmbPeriodos.DisplayMember = "NombrePeriodo";
            cmbPeriodos.ValueMember = "IdPeriodo";
        }

        public void MostrarDatos(List<ImportVolvoConnect> datos)
        {
            dgvDatos.DataSource = datos;
            dgvDatos.Columns["IdImportVolvoConnect"].Visible = false;
        }

        public List<ImportVolvoConnect> ObtenerDatos()
        {
            return dgvDatos.DataSource as List<ImportVolvoConnect> ?? new List<ImportVolvoConnect>();
        }

        public void MostrarMensaje(string mensaje)
        {
            MessageBox.Show(mensaje, "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private async void btnImportar_Click(object sender, EventArgs e)
        {
            using var openFileDialog = new OpenFileDialog { Filter = "Archivos Excel|*.xlsx" };
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                await _presenter.ImportarVolvoAsync(openFileDialog.FileName);
            }
        }

        private async void btnGuardar_Click(object sender, EventArgs e)
        {
            await _presenter.GuardarConsumosAsync();
            this.Dispose();
        }

        private async void btnExportar_Click(object sender, EventArgs e)
        {
            using var saveFileDialog = new SaveFileDialog { Filter = "Archivos Excel|*.xlsx" };
            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                await _presenter.ExportarConsumosAExcelAsync(saveFileDialog.FileName);
            }
        }

        private async void ImportDatosVolvo_Load(object sender, EventArgs e)
        {
            await _presenter.CargarPeriodosAsync();
        }
    }
}