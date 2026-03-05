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

        public DateTime PeriodoSeleccionado => dtpPeriodo.Value;

        public int QuincenaSeleccionada =>
        Convert.ToInt32(cmbQuincena.SelectedItem ?? "1");


        public void MostrarDatos(List<ImportVolvoConnectDto> datos)
        {
            gridControlDatos.DataSource = datos;

            // Ocultamos columnas no deseadas desde el GridView
            var view = dgvDatos;
            view.Columns["IdImportVolvoConnect"].Visible = false;
            view.Columns["IdUnidad"].Visible = false;
            view.Columns["IdPeriodo"].Visible = false;


            view.BestFitColumns(); // Ajusta automáticamente las columnas al contenido
        }

        public List<ImportVolvoConnectDto> ObtenerDatos()
        {
            return dgvDatos.DataSource as List<ImportVolvoConnectDto> ?? new List<ImportVolvoConnectDto>();
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
            dtpPeriodo.Value = DateTime.Now;
            cmbQuincena.Items.Clear(); // 🔹 Limpia todos los elementos anteriores
            cmbQuincena.Items.AddRange(new[] { "1", "2" });
            cmbQuincena.SelectedIndex = 0;
            gridControlDatos.DataSource = null;
            gridControlDatos.RefreshDataSource();
        }

        private async void bBuscarDatos_Click(object sender, EventArgs e)
        {
            if (PeriodoSeleccionado == DateTime.MinValue)
            {
                MostrarMensaje("Seleccione un período antes de buscar consumos.");
                return;
            }
            await _presenter.BuscarConsumosPorPeriodo();
        }
    }
}