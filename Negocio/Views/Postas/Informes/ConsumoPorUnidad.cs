using GestionFlota.Presenters;
using Shared.Models;

namespace SacNew.Views.GestionFlota.Postas.Informes
{
    public partial class ConsumoPorUnidad : Form, IConsumoUnidadView
    {
        private readonly ConsumoPorUnidadPresenter _presenter;

        public ConsumoPorUnidad(ConsumoPorUnidadPresenter presenter)
        {
            InitializeComponent();
            _presenter = presenter;
            _presenter.SetView(this);
        }

        private async void ConsumoPorUnidad_Load(object sender, EventArgs e)
        {
            await _presenter.CargarPeriodosAsync();
        }

        public int IdPeriodoSeleccionado => (int)cmbPeriodos.SelectedValue;

        public void CargarPeriodos(List<Periodo> periodos)
        {
            cmbPeriodos.DataSource = periodos;
            cmbPeriodos.DisplayMember = "NombrePeriodo";
            cmbPeriodos.ValueMember = "IdPeriodo";
        }

        public void MostrarConsumos(List<InformeConsumoUnidad> consumos)
        {
            dgvConsumos.DataSource = consumos;
            dgvConsumos.Columns["idPeriodo"].Visible = false; // Oculta ID si no es necesario
        }

        public List<InformeConsumoUnidad> ObtenerConsumos()
        {
            return (List<InformeConsumoUnidad>)dgvConsumos.DataSource;
        }

        public void MostrarMensaje(string mensaje)
        {
            MessageBox.Show(mensaje, "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private async void btnBuscar_Click(object sender, EventArgs e)
        {
            await _presenter.BuscarConsumosAsync();
        }

        private async void btnGuardar_Click(object sender, EventArgs e)
        {
            await _presenter.GuardarConsumosAsync();
        }

        private async void btnExportarExcel_Click(object sender, EventArgs e)
        {
            using (var saveFileDialog = new SaveFileDialog { Filter = "Excel Files|*.xlsx" })
            {
                if (saveFileDialog.ShowDialog() == DialogResult.OK)
                {
                    await _presenter.ExportarConsumosAExcelAsync(saveFileDialog.FileName);
                }
            }
        }
    }
}