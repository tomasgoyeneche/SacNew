using DevExpress.XtraEditors;
using GestionFlota.Presenters;
using SacNew.Views.GestionFlota.Postas.DatosVolvo;
using Shared.Models;

namespace GestionFlota.Views.Postas.DatosVolvo
{
    public partial class ImportarDatosVolvoForm : DevExpress.XtraEditors.XtraForm, IImportarVolvoConnectView
    {
        private readonly ImportarVolvoConnectPresenter _presenter;

        public ImportarDatosVolvoForm(ImportarVolvoConnectPresenter presenter)
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
            gridControlDatos.DataSource = datos;

            // Ocultamos columnas no deseadas desde el GridView
            var view = dgvDatos;
            view.Columns["IdImportVolvoConnect"].Visible = false;

            view.BestFitColumns(); // Ajusta automáticamente las columnas al contenido
        }

        public List<ImportVolvoConnect> ObtenerDatos()
        {
            return dgvDatos.DataSource as List<ImportVolvoConnect> ?? new List<ImportVolvoConnect>();
        }

        public void MostrarMensaje(string mensaje)
        {
            XtraMessageBox.Show(this, mensaje, "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
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